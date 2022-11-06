using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public enum Events
{
    login,
    loggedIn,
    register,
    goBack,
    goToLobby,
    createPlayer,
    setAttribule,
    onStart,
    onJoinLobby,
    createRoom,
    onJoinRoom,
    onLeaveRoom,
    playerJoinRoom,
    playerLeaveRoom,
    numberOfPlayersChange,
    onAccept,
    playerAccept,
    enoughPlayers,
    goToGame,
    gameOnStart,
    sendTokenToMasterClient,
    receiveTokenFromClient,
    senTeamToClient,
    receiveTeamFromMasterClient,
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
}

public class Define
{
    public const int MaxPlayers = 2;
    public static Vector3[] positionTeam = new Vector3[6] 
    {
        new Vector3(0,0,-53), 
        new Vector3(-3,0,-53), 
        new Vector3(3,0,-53),
        new Vector3(0,0,-47),
        new Vector3(-3,0,-47),
        new Vector3(3,0,-47)
    };
}

public struct URL
{
    public const string root = "http://127.0.0.1:8000/";
    public const string account_register = root + "account/register";
    public const string account_login = root + "account/login";
    public const string account_token = root + "account/token";
    public const string player_chooseCharacter = root + "player/choose-character";
    public const string player_checkPlayer = root + "player/check-player";
}
