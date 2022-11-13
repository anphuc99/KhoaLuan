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
    private int eventID3;
    private List<Json.PlayerTeam> teamList = new List<Json.PlayerTeam>();
    private void Awake()
    {
        eventID = Event.register(Events.onBeginGameStart, onBeginGameStart);
        eventID2 = Event.register(Events.onGameStart, onGameStart);
        eventID3 = Event.register(Events.playerLeaveRoom, playerLeaveRoom);
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

    private void playerLeaveRoom(object _player)
    {
        if (!PhotonNetwork.IsMasterClient) return;
        StartCoroutine(playerOut(_player));
    }

    IEnumerator playerOut(object _player)
    {
        yield return new WaitForSeconds(2);
        Player player = (Player)_player;
        Json.Room room = new Json.Room()
        {
            roomID = PhotonNetwork.CurrentRoom.Name,
            clientID = player.UserId,
            _token = Global.account._token
        };

        Json.SocketType<Json.Room> socketType = new Json.SocketType<Json.Room>()
        {
            type = "playerOut",
            data = room
        };

        Event.emit(Events.socketSendMessage, socketType);
    }

    private void OnDisable()
    {
        Event.unRegister(Events.onBeginGameStart, eventID);
        Event.unRegister(Events.onGameStart, eventID2);
        Event.unRegister(Events.playerLeaveRoom, eventID3);
    }
}
