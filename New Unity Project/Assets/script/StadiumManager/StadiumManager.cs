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
        Event.emit(Events.goToGame, null);
        Global.state = State.waitForGame;
        sendUserID();
    }

    private void sendUserID()
    {
        photonView.RPC("sendUserID", RpcTarget.MasterClient, PhotonNetwork.LocalPlayer.UserId ,Global.playerClient.account_id);
    }
}
