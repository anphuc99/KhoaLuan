using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class MasterClientStadiumManager : MonoBehaviourPunCallbacks
{
    private void Awake()
    {
        //Event.register(Events.register)
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Start()
    {
        StartCoroutine(senTeamToClient());
    }

    IEnumerator senTeamToClient()
    {
        while (true)
        {
            if (!PhotonNetwork.IsMasterClient) break;
            Dictionary<int, GameObject> playerInstantiations = Global.playerInstantiation;
            if (playerInstantiations.Count != Define.MaxPlayers)
            {
                yield return new WaitForFixedUpdate();
                continue;
            }
            Json.PlayerTeam[] playerTeams = new Json.PlayerTeam[playerInstantiations.Count];
            Debug.Log("co cocoocococ" + playerInstantiations.Count);
            int i = 0;
            foreach (KeyValuePair<int, GameObject> pair in playerInstantiations)
            {
                playerTeams[i] = new Json.PlayerTeam();
                if (i < Define.MaxPlayers / 2)
                {
                    playerTeams[i].viewID = pair.Key;
                    playerTeams[i].team = 1;
                }
                else
                {
                    playerTeams[i].viewID = pair.Key;
                    playerTeams[i].team = 2;
                }
                playerTeams[i].position = i;
                i++;
            }

            Event.emit(Events.senTeamToClient, playerTeams);
            break;
        }
    }
}
