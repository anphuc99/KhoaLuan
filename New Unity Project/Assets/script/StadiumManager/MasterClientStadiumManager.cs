using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using System.Linq;

public class MasterClientStadiumManager : MonoBehaviourPunCallbacks
{
    private int eventID;
    private int eventID2;
    private void Awake()
    {
        eventID = Event.register(Events.onBeginGameStart, onGameStart);
        eventID2 = Event.register(Events.playerLeaveRoom, playerLeaveRoom);
    }
    private void onGameStart(object context)
    {
        if (!PhotonNetwork.IsMasterClient) return;
        Dictionary<string, GameObject> playerInstantiations = Global.playerInstantiation;
        Json.PlayerTeam[] playerTeams = new Json.PlayerTeam[playerInstantiations.Count];
        int i = 0;
        foreach (KeyValuePair<string, GameObject> pair in playerInstantiations)
        {
            playerTeams[i] = new Json.PlayerTeam();
            if (i < Define.MaxPlayers / 2)
            {
                playerTeams[i].UserID = pair.Key;
                playerTeams[i].team = 0;
                playerTeams[i].position = i;
            }
            else
            {
                playerTeams[i].UserID = pair.Key;
                playerTeams[i].team = 1;
                playerTeams[i].position = i - Define.MaxPlayers / 2;
            }
            i++;
        }
        Event.emit(Events.senTeamToClient, playerTeams);
        string json = JsonHelper.ToJson<Json.PlayerTeam>(playerTeams);
        SetGlobal.setValue(Value.listPlayerTeam, json);
    }    

    private void playerLeaveRoom(object _player)
    {
        if (!PhotonNetwork.IsMasterClient) return;
        Player player = (Player)_player;
        string json = (string)SetGlobal.getValue(Value.listPlayerTeam);
        List<Json.PlayerTeam> playerTeams = JsonHelper.FromJson<Json.PlayerTeam>(json).ToList();
        Json.PlayerTeam playerItem = playerTeams.Find(x => x.UserID == player.UserId);
        playerTeams.Remove(playerItem);
    }

    private void OnDisable()
    {
        Event.unRegister(Events.onGameStart, eventID);
    }
}
