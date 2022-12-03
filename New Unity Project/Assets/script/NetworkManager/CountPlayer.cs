using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountPlayer : MonoBehaviourPunCallbacks
{
    private int oldCount = 0;
    private bool isJoinRoom = false;

    private void Awake()
    {
        //Event.register(Events.goBack, goBack);
    }
    public override void OnJoinedRoom()
    {
        oldCount = 0;
        isJoinRoom = true;
    }

    public override void OnLeftRoom()
    {
        isJoinRoom = false;
    }

    // Update is called once per frame
    void Update()
    {
        try
        {
            if (!isJoinRoom) return;
            int playerInRooms = PhotonNetwork.CurrentRoom.Players.Count;
            if (oldCount != playerInRooms)
            {
                StartCoroutine(emitCount(playerInRooms));
                oldCount = playerInRooms;
            }
        }
        catch(Exception e)
        {
            Debug.LogException(e);
        }
    }

    IEnumerator emitCount(int num)
    {
        yield return new WaitForSeconds(0.5f);
        Event.emit(Events.numberOfPlayersChange, num);
    }
}
