using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class ConnectSocket : MonoBehaviour
{
    private void Awake()
    {
        Event.register(Events.canConnect, onLogin);
        Event.register(Events.endGame, endGame);
        Event.register(Events.onCreateRoom, onCreateRoom);
        Event.register(Events.onJoinRoom, onJoinRoom);
        Event.register(Events.login, onLogout);
        Event.register(Events.onServerCreateRoom, onServerCreateRoom);
        Event.register(Events.onServerJoinRoom, onServerJoinRoom);
        Event.register(Events.setNewMaster, setNewMaster);
    }

    private void Start()
    {
        StartCoroutine(stayConnected());
    }

    private void onLogin(object context)
    {
        Event.emit(Events.connected, null);
        string _token = Global.account._token;
        Json.SocketType<string> socketType = new Json.SocketType<string>()
        {
            type = "login",
            data = _token,
        };
        Event.emit(Events.socketSendMessage, socketType);
    }

    IEnumerator stayConnected()
    {
        while (true)
        {
            if (PhotonNetwork.IsConnected && PhotonNetwork.IsMasterClient)
            {
                Json.SocketType<string> socketType = new Json.SocketType<string>()
                {
                    type = "connectWithMaster",
                    data = Global.account._token,
                };
                Event.emit(Events.socketSendMessage, socketType);
            }
            yield return new WaitForSeconds(1);
        }
    }

    private void setNewMaster(object _newMaster)
    {
        Debug.Log("setNewMaster");
        string newMaster = (string)_newMaster;
        Dictionary<int, Player> playerList = PhotonNetwork.CurrentRoom.Players;        
        foreach (var player in playerList)
        {
            if (player.Value.UserId == newMaster)
            {
                PhotonNetwork.SetMasterClient(player.Value);
            }
        }
    }

    private void onLogout(object context)
    {
        Event.emit(Events.closeSocket, null);
    }

    private void endGame(object context)
    {
        if(PhotonNetwork.IsConnected)
        {
            PhotonNetwork.Disconnect();
        }            
        Json.EndGame endGame = JsonUtility.FromJson<Json.EndGame>((string)context);
        Global.gameID = endGame.gameID;
        Event.emit(Events.showResult, null);
        Event.emit(Events.setAttribule, endGame.player);
    }

    private void onCreateRoom(object roomid)
    {
        Json.Room room = new Json.Room()
        {
            roomID = (string)roomid,
            _token = Global.account._token,
            clientID = PhotonNetwork.LocalPlayer.UserId
        };
        Json.SocketType<Json.Room> socketType = new Json.SocketType<Json.Room>()
        {
            type = "createRoom",
            data = room,
        };

        Event.emit(Events.socketSendMessage, socketType);
    }

    private void onServerCreateRoom(object context)
    {
        string rs = (string)context;
        Debug.Log("onServerCreateRoom: " + rs);
        if (rs != "Success")
        {
            Event.emit(Events.goBack, null);
        }
    }

    private void onJoinRoom(object roomid)
    {
        Json.Room room = new Json.Room()
        {
            roomID = (string)roomid,
            _token = Global.account._token,
            clientID = PhotonNetwork.LocalPlayer.UserId
        };
        Json.SocketType<Json.Room> socketType = new Json.SocketType<Json.Room>()
        {
            type = "joinRoom",
            data = room,
        };

        Event.emit(Events.socketSendMessage, socketType);
    }

    private void onServerJoinRoom(object context)
    {
        string rs = (string)context;
        Debug.Log("onServerJoinRoom: " + rs);
        if (rs != "Success")
        {
            Event.emit(Events.goBack, null);
        }
    }
}
