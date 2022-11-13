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
    private List<Json.PlayerTeam> teamList = new List<Json.PlayerTeam>();
    private void Awake()
    {
        eventID = Event.register(Events.onBeginGameStart, onGameStart);
        eventID2 = Event.register(Events.playerLeaveRoom, playerLeaveRoom);
        PhotonNetwork.CurrentRoom.IsVisible = false;
    }
    private void onGameStart(object context)
    {
        if (!PhotonNetwork.IsMasterClient) return;
        if (teamList.Count == 0)
        {
            string Json = SetGlobal.getValue(Value.listPlayerTeam).ToString();
            teamList = JsonHelper.FromJson<Json.PlayerTeam>(Json).ToList();
        }
        int i = 0;
        foreach (Json.PlayerTeam team in teamList)
        {
            if (i < Define.MaxPlayers / 2)
            {
                team.team = 0;
                team.position = i;
            }
            else
            {
                team.team = 1;
                team.position = i - Define.MaxPlayers / 2;
            }
            i++;
        }
        Event.emit(Events.senTeamToClient, teamList.ToArray());
        string json = JsonHelper.ToJson<Json.PlayerTeam>(teamList.ToArray());
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
        json = JsonHelper.ToJson<Json.PlayerTeam>(playerTeams.ToArray());
        teamList = playerTeams;
        SetGlobal.setValue(Value.listPlayerTeam, json);
    }

    private void OnDisable()
    {
        Event.unRegister(Events.onGameStart, eventID);
    }

    [PunRPC]
    public void sendUserID(string UserID, int account_id)
    {
        teamList.Add(new Json.PlayerTeam() { account_id = account_id, UserID = UserID });
        Debug.Log("sendUserID " + teamList.Count + account_id);
        if(teamList.Count == PhotonNetwork.CurrentRoom.PlayerCount)
        {
            string json = JsonHelper.ToJson<Json.PlayerTeam>(teamList.ToArray());
            SetGlobal.setValue(Value.listPlayerTeam, json);
            Debug.Log("sendUserID if" + teamList.Count);
            Debug.Log(json);
        }
    }
}
