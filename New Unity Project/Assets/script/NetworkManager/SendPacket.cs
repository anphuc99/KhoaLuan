using Photon.Pun;
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
    }

    private void onAccept(object o)
    {
        photonView.RPC("playerAccept", RpcTarget.All, PhotonNetwork.LocalPlayer);
    }
}
