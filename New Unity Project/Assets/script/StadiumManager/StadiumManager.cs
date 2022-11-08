using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class StadiumManager : MonoBehaviourPunCallbacks
{
    private string playerName = "Player";
    public GameObject prefap;
    private Dictionary<string, GameObject> characterList = new Dictionary<string, GameObject>();
    private int eventID;
    private void OnEnable()
    {
        Vector3 position = new Vector3(0, 0, -50);
        PhotonNetwork.Instantiate(playerName, position, Quaternion.identity);
        eventID = Event.register(Events.resultsTeamWin, resultGame);
    }
    // Update is called once per frame
    //void Update()
    //{
    //    PhotonNetwork.Instantiate(this.playerName, new Vector3(0, 0, -50), Quaternion.identity);
    //}

    private void resultGame(object context)
    {
        Destroy(gameObject);
    }
}
