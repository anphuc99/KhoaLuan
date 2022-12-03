using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public static class Events
{
    public static string
        login = "login",
        loggedIn = "loggedIn",

        canConnect = "canConnect",
        register = "register",
        goBack = "goBack",
        goToLobby = "goToLobby",
        createPlayer = "createPlayer",
        setAttribule = "setAttribule",
        onStart = "onStart",
        onJoinLobby = "onJoinLobby",
        onCreateRoom = "onCreateRoom",
        onJoinRoom = "onJoinRoom",
        onLeaveRoom = "onLeaveRoom",
        playerJoinRoom = "playerJoinRoom",
        playerLeaveRoom = "playerLeaveRoom",
        numberOfPlayersChange = "numberOfPlayersChange",
        onAccept = "onAccept",
        playerAccept = "playerAccept",
        enoughPlayers = "enoughPlayers",
        goToGame = "goToGame",
        gameOnStart = "gameOnStart",
        sendTokenToMasterClient = "sendTokenToMasterClient",
        receiveTokenFromClient = "receiveTokenFromClient",
        senTeamToClient = "senTeamToClient",
        receiveTeamFromSever = "receiveTeamFromSever",
        setGlobalValue = "setGlobalValue",
        onSetValueGlobal = "onSetValueGlobal",
        onBeginGameStart = "onBeginGameStart",
        onGameStart = "onGameStart",
        onGameRestart = "onGameRestart",
        enoughScore = "enoughScore",
        timeOut = "timeOut",
        resultsTeamWin = "resultsTeamWin",
        gameRaw = "gameRaw",
        addTime = "addTime",
        endGame = "endGame",
        sendUserID = "sendUserID",
        receiveUserID = "receiveUserID",
        connected = "connected",
        closeSocket = "closeSocket",
        socketSendMessage = "socketSendMessage",
        onServerCreateRoom = "onServerCreateRoom",
        onServerJoinRoom = "onServerJoinRoom",
        masterCertificate = "masterCertificate",
        setNewMaster = "setNewMaster",
        showLeaderboard = "showLeaderboard",
        showSetting = "showSetting",
        showProfile = "showProfile",
        showHistory = "showHistory",
        showResult = "showResult",
        setLanguage = "setLanguage",
        setMultiplier = "setMultiplier",
        setVolumeMusic = "setVolumeMusic",
        stopSound = "stopSound",
        setVolumeSound = "setVolumeSound",
        playSound = "playSound",
        horizotalSpeed = "horizotalSpeed",
        verticalSpeed = "verticalSpeed",
        jumpSpeed = "jumpSpeed";

}
public struct SceneName
{
    public const string Test = "Test";
    public const string Login = "Login";
    public const string ChooseCharater = "ChooseCharater";
    public const string Lobby = "Lobby";
    public const string Wait4Player = "WaitForPlayer";
    public const string Register = "Register";
    public const string LoadStadium = "LoadStadium";
    public const string Stadium = "Stadium";
    public const string Leaderboard = "Leaderboard";
    public const string Setting = "Setting";
    public const string Profile = "Profile";
    public const string History = "History";
    public const string Result = "Result";
    public const string LoadGame = "LoadGame";
}

public class Define
{
    public static int MaxPlayers = 2;
    public static int WaitForGame = 15;
    public static int TimeGame = 300;
    public static int TimeOver = 30;
    public static int scoreWin = 5;
    public static string WebsocetURL = "";
    public static string Website = "";
    public static Vector3[,] positionTeam = new Vector3[2, 3]
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

    public static Quaternion[] quaternionsTeam = new Quaternion[2] 
    {
        Quaternion.identity,
        new Quaternion(0,1,0,0),
    };
}

public class URL
{
    //http://127.0.0.1:8000/
    //https://api.soccerlegend.devmini.com/
    public static string account_register { get { return Define.Website + "account/register"; } }
    public static string account_login { get { return Define.Website + "account/login"; } }
    public static string account_token { get { return Define.Website + "account/token"; } }
    public static string player_chooseCharacter { get { return Define.Website + "player/choose-character"; } }
    public static string player_checkPlayer { get { return Define.Website + "player/check-player"; } }
    public static string player_setMultiplier { get { return Define.Website + "player/set-multiplier"; } }
    public static string game_getTopRank { get { return Define.Website + "game/get-top-rank"; } }
    public static string game_getMyRank { get { return Define.Website + "game/get-my-rank"; } }
    public static string game_getHistory { get { return Define.Website + "game/get-history"; } }
    public static string game_getGameInfo { get { return Define.Website + "game/get-game-info"; } }
}

public struct Value
{
    public const string TimeWait = "TimeWait";
    public const string Time = "Time";
    public const string redScore = "redScore";
    public const string blueScore = "blueScore";
    public const string listPlayerTeam = "listPlayerTeam";
}
