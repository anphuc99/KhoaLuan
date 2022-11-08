using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class SetTeamWin : MonoBehaviourPunCallbacks
{
    private bool isGameRaw = false;
    private int eventID;
    private int eventID2;
    private int eventID3;
    private void Awake()
    {
        eventID = Event.register(Events.enoughScore, setTeamWin);
        eventID2 = Event.register(Events.timeOut, setTeamWin);
        eventID3 = Event.register(Events.numberOfPlayersChange, numberOfPlayersChange);
    }

    private void setTeamWin(object context)
    {
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
        StartCoroutine(WaitGoToLobby());
    }

    private void numberOfPlayersChange(object num)
    {
        if (!PhotonNetwork.IsMasterClient) return;
        int number = (int)num;
        if(number == 1)
        {
            Event.emit(Events.endGame, null);
        }
    }

    IEnumerator WaitGoToLobby()
    {
        yield return new WaitForSeconds(5);
        photonView.RPC(nameof(gameEnd), RpcTarget.All);
    }

    [PunRPC]
    public void resultGame(int team)
    {
        if (PhotonNetwork.IsMasterClient)
        {
            addTodb();
        }
        Event.emit(Events.resultsTeamWin, team);
    }

    [PunRPC]
    public void gameEnd()
    {
        if(!PhotonNetwork.IsMasterClient)
            Event.emit(Events.endGame, null);
    }

    private void OnDisable()
    {
        Event.unRegister(Events.enoughScore, eventID);
        Event.unRegister(Events.timeOut, eventID2);
        Event.unRegister(Events.numberOfPlayersChange, eventID3);
    }
}
