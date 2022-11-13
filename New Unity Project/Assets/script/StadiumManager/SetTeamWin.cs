using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.Networking;

public class SetTeamWin : MonoBehaviourPunCallbacks
{
    private bool isGameRaw = false;
    private int eventID;
    private int eventID2;
    private void Awake()
    {
        eventID = Event.register(Events.enoughScore, setTeamWin);
        eventID2 = Event.register(Events.timeOut, setTeamWin);        
    }

    private void setTeamWin(object context)
    {
        if (Global.state == State.gameEnd)
        {
            addTodb();
            return;
        }
        int redScore = (int?)SetGlobal.getValue(Value.redScore) ?? 0;
        int blueScore = (int?)SetGlobal.getValue(Value.blueScore) ?? 0;
        if (redScore > blueScore)
        {
            photonView.RPC(nameof(resultGame), RpcTarget.All, 0);
        }
        else if (redScore < blueScore)
        {
            photonView.RPC(nameof(resultGame), RpcTarget.All, 1);
        }
        else
        {
            if (!isGameRaw)
            {
                Event.emit(Events.gameRaw, null);            
                isGameRaw = true;
            }
            else
            {
                photonView.RPC(nameof(resultGame), RpcTarget.All, -1);
            }
        }
    }

    private void addTodb()
    {
        StartCoroutine(requestAddTodb());
    }

    IEnumerator requestAddTodb()
    {
        yield return new WaitForSeconds(5);
        if (!PhotonNetwork.IsMasterClient) yield return null;
        Json.Resutls resutls = new Json.Resutls();        
        resutls.redScore = (int?) SetGlobal.getValue(Value.redScore) ?? 0;
        resutls.blueScore = (int?) SetGlobal.getValue(Value.blueScore) ?? 0;
        Json.SocketType<Json.Resutls> socketType = new Json.SocketType<Json.Resutls>()
        {
            type = "sendResult",
            data = resutls
        };

        Event.emit(Events.socketSendMessage, socketType);
    }

    [PunRPC]
    public void resultGame(int team)
    {
        if (PhotonNetwork.IsMasterClient)
        {
            addTodb();
        }
        Global.state = State.gameEnd;
        Event.emit(Events.resultsTeamWin, team);
    }

    private void OnDisable()
    {
        Event.unRegister(Events.enoughScore, eventID);
        Event.unRegister(Events.timeOut, eventID2);
    }
}
