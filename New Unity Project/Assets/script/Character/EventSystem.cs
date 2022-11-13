using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System.Linq;

public class EventSystem : MonoBehaviourPunCallbacks
{
    public string materialName;
    public Texture team_red;
    public Texture team_blue;

    private int eventID;
    private int eventID2;
    private int team;
    private int pos;

    private void Awake()
    {
        eventID = Event.register(Events.receiveTeamFromSever, receiveTeamFromSever);
        eventID2 = Event.register(Events.onGameRestart, onGameRestart);
    }

    private void receiveTeamFromSever(object playerTeams)
    {
        Debug.Log((string)playerTeams);
        Json.PlayerTeam[] playerTeams1 = JsonHelper.FromJson<Json.PlayerTeam>((string)playerTeams);
        foreach (Json.PlayerTeam playerTeam in playerTeams1)
        {
            if (photonView.Owner.UserId == playerTeam.UserID)
            {
                GameObject body = transform.Find("body").gameObject;
                List<Material> myMaterials = body.GetComponent<Renderer>().materials.ToList();
                Material matBody = null;
                foreach (Material mat in myMaterials)
                {
                    if(mat.name == materialName)
                    {
                        matBody = mat;
                    }
                }
                if (playerTeam.team == 0)
                {
                    matBody.mainTexture = team_red;
                }
                else
                {
                    matBody.mainTexture = team_blue;
                }
                team = playerTeam.team;
                pos = playerTeam.position;
                transform.position = Define.positionTeam[team, pos];
                transform.rotation = Define.quaternionsTeam[team];
                if (photonView.IsMine)
                {
                    Global.myTeam = team;
                }
            }
        }
    }

    private void onGameRestart(object context)
    {
        transform.position = Define.positionTeam[team, pos];
        transform.rotation = Define.quaternionsTeam[team];
    }

    private void OnDisable()
    {
        Event.unRegister(Events.receiveTeamFromSever, eventID);
        Event.unRegister(Events.onGameRestart, eventID2);
    }
}
