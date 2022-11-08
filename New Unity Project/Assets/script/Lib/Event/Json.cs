﻿using System;
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
        public string playerTeams;
        public int redScore;
        public int blueScore;
    }
}
