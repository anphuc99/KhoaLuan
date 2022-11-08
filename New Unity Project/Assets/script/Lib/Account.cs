using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.script.Player
{
    public class Account
    {
        public int id;
        public string name;
        public string email;
        public string username;
        public string _token;
    }

    public class PlayerClient
    {
        public string name;
        public int account_id;
        public int score;
    }
}
