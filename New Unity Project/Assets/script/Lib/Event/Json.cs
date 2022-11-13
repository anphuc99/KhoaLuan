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
}
