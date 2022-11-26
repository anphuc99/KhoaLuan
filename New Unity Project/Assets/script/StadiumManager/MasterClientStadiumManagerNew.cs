using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using System.Linq;

public class MasterClientStadiumManagerNew : MonoBehaviourPunCallbacks
{
    private int eventID;
    private int eventID2;
    private List<Json.PlayerTeam> teamList = new List<Json.PlayerTeam>();
    private void Awake()
    {
        eventID = Event.register(Events.onBeginGameStart, onBeginGameStart);
        eventID2 = Event.register(Events.onGameStart, onGameStart);
        PhotonNetwork.CurrentRoom.IsVisible = false;
    }    

    private void onBeginGameStart(object context)
    {
        if (!PhotonNetwork.IsMasterClient) return;
        Json.SocketType<string> socketType = new Json.SocketType<string>()
        {
            type = "onGameBeginStart",
            data = ""
        };

        Event.emit(Events.socketSendMessage, socketType);
    }
    private void onGameStart(object context)
    {
        if (!PhotonNetwork.IsMasterClient) return;
        
    }

    private void OnDisable()
    {
        Event.unRegister(Events.onBeginGameStart, eventID);
        Event.unRegister(Events.onGameStart, eventID2);
    }
}
