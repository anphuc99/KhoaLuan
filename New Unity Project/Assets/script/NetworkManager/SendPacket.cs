using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SendPacket : MonoBehaviourPunCallbacks
{
    private PhotonView photonView;
    private void Awake()
    {
        photonView = PhotonView.Get(this);
        Event.register(Events.onAccept, onAccept);
        Event.register(Events.senTeamToClient, sendTeamToClient);
    }
    private void sendTeamToClient(object players)
    {
        Json.PlayerTeam[] playerTeams = players as Json.PlayerTeam[];
        string json = JsonHelper.ToJson<Json.PlayerTeam>(playerTeams);
        photonView.RPC("sendTeamToClient", RpcTarget.All, json);
    }

    private void onAccept(object o)
    {
        photonView.RPC("playerAccept", RpcTarget.All, PhotonNetwork.LocalPlayer);
    }

}
