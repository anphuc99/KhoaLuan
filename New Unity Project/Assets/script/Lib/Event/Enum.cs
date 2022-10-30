using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public enum Events
{
    login,
    loggedIn,
    register,
    goBack,
    goToLobby,
    createPlayer,
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
}
public struct SceneName
{
    public const string Login = "Login";
    public const string ChooseCharater = "ChooseCharater";
    public const string Lobby = "Lobby";
    public const string Wait4Player = "WaitForPlayer";
    public const string Register = "Register";
    public const string LoadStadium = "LoadStadium";
}

public struct Define
{
    public const int MaxPlayers = 2;
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
