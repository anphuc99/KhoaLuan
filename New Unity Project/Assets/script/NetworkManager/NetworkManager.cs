using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System;
using Photon.Realtime;

public class NetworkManager : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update
    private void Awake()
    {
        Event.register(Events.onStart, onStart);
        Event.register(Events.goBack, goBack);
    }

    private void onStart(object context)
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    private void goBack(object context)
    {
        PhotonNetwork.Disconnect();
    }

    public override void OnConnectedToMaster()
    {
        Event.emit(Events.onJoinLobby, null);
        PhotonNetwork.JoinLobby();
    }

    private void CreateRoom()
    {
        string roomid = Guid.NewGuid().ToString();
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.IsVisible = true;
        roomOptions.MaxPlayers = Define.MaxPlayers;
        PhotonNetwork.CreateRoom(roomid, roomOptions);
        Event.emit(Events.createRoom, roomid);
    }

    public override void OnJoinedLobby()
    {
        PhotonNetwork.JoinRandomRoom();
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        CreateRoom();
    }

    public override void OnJoinedRoom()
    {
        Event.emit(Events.onJoinRoom, null);
    }

    public override void OnLeftRoom()
    {
        Event.emit(Events.onLeaveRoom, null);
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Event.emit(Events.playerJoinRoom, newPlayer);
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        Event.emit(Events.playerLeaveRoom, otherPlayer);
    }
}