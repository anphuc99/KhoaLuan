using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingDefine : MonoBehaviour
{
    public int MaxPlayers = 6;
    public int WaitForGame = 15;
    public int TimeGame = 300;
    public int TimeOver = 30;
    public int scoreWin = 5;
    public bool local = false;
    public Vector3[,] positionTeam = new Vector3[2, 3]
    {
        {
            new Vector3(0,0,-55),
            new Vector3(-3,0,-55),
            new Vector3(3,0,-55),
        },
        {
            new Vector3(0,0,-45),
            new Vector3(-3,0,-45),
            new Vector3(3,0,-45)
        },
    };

    public Quaternion[] quaternionsTeam = new Quaternion[2]
    {
        Quaternion.identity,
        new Quaternion(0,1,0,0),
    };

    private void Awake()
    {
        Define.MaxPlayers = MaxPlayers;
        Define.WaitForGame = WaitForGame;
        Define.positionTeam = positionTeam;
        Define.quaternionsTeam = quaternionsTeam;
        Define.TimeGame = TimeGame;
        Define.TimeOver = TimeOver;
        Define.scoreWin = scoreWin;
        if (local)
        {
            Define.Website = "http://192.168.1.145:8000/";
            Define.WebsocetURL = "ws://192.168.1.145:3000";
        }
        else
        {
            Define.Website = "https://api.soccerlegend.devmini.com/";
            Define.WebsocetURL = "wss://devmini.com";
        }
    }
}
