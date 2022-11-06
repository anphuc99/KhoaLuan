using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReceivePacket : MonoBehaviourPunCallbacks
{
    [PunRPC]
    public void playerAccept(Player player)
    {
        Event.emit(Events.playerAccept, player);
    }

    [PunRPC]
    public void sendTeamToClient(string json)
    {
        Debug.Log(json);
        Json.PlayerTeam[] playerTeams = JsonHelper.FromJson<Json.PlayerTeam>(json);
        Event.emit(Events.receiveTeamFromMasterClient, playerTeams);
    }
}
