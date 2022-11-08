using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class TimeGame : MonoBehaviour
{
    private int eventID;
    private int eventID2;
    private int eventID3;

    private bool gameEnd = false;

    private void Awake()
    {
        eventID = Event.register(Events.onGameStart, onGameStart);
        eventID2 = Event.register(Events.addTime, addTime);
    }

    private void onGameStart(object context)
    {
        StartCoroutine(timeGame());        
    }

    IEnumerator timeGame()
    {
        while (true)
        {
            if (gameEnd) break;
            if (!PhotonNetwork.IsMasterClient)
            {
                yield return new WaitForFixedUpdate();
                continue;
            }

            int time = (int?)SetGlobal.getValue(Value.Time) ?? Define.TimeGame;
            time--;
            if(time == 0)
            {
                Event.emit(Events.timeOut, null);
            }
            else if(time > 0)
            {
                SetGlobal.setValue(Value.Time, time);
            }
            yield return new WaitForSeconds(1);
        }
    }

    private void addTime(object increTime)
    {
        if (PhotonNetwork.IsMasterClient)
        {
            int time = (int)SetGlobal.getValue(Value.Time);
            time = (int)increTime;
            SetGlobal.setValue(Value.Time, time);
        }
    }

    private void OnDisable()
    {
        Event.unRegister(Events.onGameStart, eventID);
        Event.unRegister(Events.addTime, eventID2);
    }

}