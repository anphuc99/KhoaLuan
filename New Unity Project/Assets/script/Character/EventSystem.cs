using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class EventSystem : MonoBehaviour
{
    public string materialName;
    public Texture team_red;
    public Texture team_blue;

    private int eventID;
    private int eventID2;
    private int team;
    private int pos;

    private void Awake()
    {
        eventID = Event.register(Events.receiveTeamFromSever, receiveTeamFromSever);
        eventID2 = Event.register(Events.onGameRestart, onGameRestart);
    }

    private void receiveTeamFromSever(object playerTeams)
    {
        
    }

    private void onGameRestart(object context)
    {
        transform.position = Define.positionTeam[team, pos];
        transform.rotation = Define.quaternionsTeam[team];
    }

    private void OnDisable()
    {
        Event.unRegister(Events.receiveTeamFromSever, eventID);
        Event.unRegister(Events.onGameRestart, eventID2);
    }
}
