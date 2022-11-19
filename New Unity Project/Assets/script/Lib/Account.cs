using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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

    [Serializable]
    public class PlayerClient
    {
        public int account_id;
        public string name;
        public int level;
        public int exp;
        public int fans;
        public int speed;
        public int jump;
        public int shotForce;
        public int point;

        public object this[string key]
        {
            get
            {
                switch (key)
                {
                    case "account_id":
                        return account_id;
                    case "name":
                        return name;
                    case "level":
                        return level;
                    case "exp":
                        return exp;
                    case "fans":
                        return fans;
                    case "speed":
                        return speed;
                    case "jump":
                        return jump;
                    case "shotForce":
                        return shotForce;
                    case "point":
                        return point;
                    default:
                        return null;
                }
            }
            
            set
            {
                switch (key)
                {
                    case "account_id":
                        account_id = (int)value;
                        break;
                    case "name":
                        name = (string) value;
                        break;
                    case "level":
                        level = (int)value;
                        break;
                    case "exp":
                        exp = (int)value;
                        break;
                    case "fans":
                        fans = (int)value;
                        break;
                    case "speed":
                        speed = (int)value;
                        break;
                    case "jump":
                        jump = (int)value;
                        break;
                    case "shotForce":
                        shotForce = (int)value;
                        break;
                    case "point":
                        point = (int)value;
                        break;
                }
            }
        }
    }
}
