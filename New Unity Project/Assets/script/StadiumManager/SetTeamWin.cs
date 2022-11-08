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
        Json.Resutls resutls = new Json.Resutls();        
        resutls.playerTeams = (string)SetGlobal.getValue(Value.listPlayerTeam);
        resutls.redScore = (int?) SetGlobal.getValue(Value.redScore) ?? 0;
        resutls.blueScore = (int?) SetGlobal.getValue(Value.blueScore) ?? 0;
        WWWForm form = new WWWForm();
        form.AddField("playerTeams", resutls.playerTeams);
        form.AddField("redScore", resutls.redScore);
        form.AddField("blueScore", resutls.blueScore);

        using (UnityWebRequest www = UnityWebRequest.Post(URL.game_sendGameResutls, form))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
            }
            else
            {
                photonView.RPC(nameof(gameEnd), RpcTarget.All);
            }
        }

    }

    IEnumerator WaitGoToLobby()
    {
        yield return new WaitForSeconds(5);
        Event.emit(Events.endGame, null);
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

    [PunRPC]
    public void gameEnd()
    {
        StartCoroutine(WaitGoToLobby());
    }

    private void OnDisable()
    {
        Event.unRegister(Events.enoughScore, eventID);
        Event.unRegister(Events.timeOut, eventID2);
    }
}
