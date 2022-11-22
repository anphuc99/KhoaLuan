using Assets.script.Player;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Json
{
    [Serializable]
    public class PlayerTeam
    {
        public string UserID;
        public int team;
        public int position;
        public int account_id = 0;
    }

    [Serializable]
    public class Resutls
    {
        public int redScore;
        public int blueScore;
    }

    [Serializable]
    public class SocketType<T>
    {
        public string type;
        public T data;
    }

    [Serializable]
    public class SocketResponse
    {
        public string type;
        public string data;
    }

    [Serializable]
    public class Room
    {
        public string _token;
        public string roomID;
        public string clientID;
    }

    [Serializable]
    public class HistoryInfo
    {
        public int gameID;
        public int playerID;
        public int team;
        public int redScore;
        public int blueScore;
        public string date;
        public int result;
    }

    [Serializable]
    public class Game
    {
        public int id;
        public int redScore;
        public int blueScore;
        public int master;
        public string date;
    }

    [Serializable]
    public class GameInfo
    {
        public int id;
        public int gameID;
        public int playerID;
        public int team;
        public string name;
        public string level;
    }

    [Serializable]
    public class GetGameInfo
    {
        public Game game;
        public GameInfo[] gameInfo;
    }

    [Serializable]
    public class EndGame
    {
        public int gameID;
        public string player;
    }

    [Serializable]
    public class Msg
    {
        public string msg;
    }
}
