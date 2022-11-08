using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddTime : MonoBehaviour
{
    private int eventID;
    private void Awake()
    {
        eventID = Event.register(Events.gameRaw, addTime);
    }

    private void addTime(object context)
    {
        Event.emit(Events.addTime, Define.TimeOver);
    }

    private void OnDisable()
    {
        Event.unRegister(Events.gameRaw, eventID);
    }
}
