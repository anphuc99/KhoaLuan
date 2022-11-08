using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class EventSystem : MonoBehaviourPunCallbacks
{

    public Texture team_red;
    public Texture team_blue;

    private int eventID;
    private int eventID2;
    private int team;
    private int pos;

    private void Awake()
    {
        eventID = Event.register(Events.receiveTeamFromMasterClient, receiveTeamFromMasterClient);
        eventID2 = Event.register(Events.onGameRestart, onGameRestart);
    }

    private void receiveTeamFromMasterClient(object playerTeams)
    {
        Json.PlayerTeam[] playerTeams1 = (Json.PlayerTeam[])playerTeams;
        foreach (Json.PlayerTeam playerTeam in playerTeams1)
        {
            if (photonView.Owner.UserId == playerTeam.UserID)
            {
                GameObject body = transform.Find("body").gameObject;
                if (playerTeam.team == 0)
                {
                    SkinnedMeshRenderer m_Renderer = body.GetComponent<SkinnedMeshRenderer>();
                    m_Renderer.material.mainTexture = team_red;
                }
                else
                {
                    SkinnedMeshRenderer m_Renderer = body.GetComponent<SkinnedMeshRenderer>();
                    m_Renderer.material.mainTexture = team_blue;
                }
                team = playerTeam.team;
                pos = playerTeam.position;
                transform.position = Define.positionTeam[team, pos];
                transform.rotation = Define.quaternionsTeam[team];
                if (photonView.IsMine)
                {
                    Global.myTeam = team;
                }
            }
        }
    }

    private void onGameRestart(object context)
    {
        transform.position = Define.positionTeam[team, pos];
        transform.rotation = Define.quaternionsTeam[team];
    }

    private void OnDisable()
    {
        Event.unRegister(Events.receiveTeamFromMasterClient, eventID);
        Event.unRegister(Events.onGameRestart, eventID2);
    }
}
