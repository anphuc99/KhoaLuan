using Assets.script.Player;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public enum State
{
    appStart,
    waitForGame,
    gameStart,
    gamePause,
    gameEnd,
}

public static class Global
{
    public static State state = State.appStart;
    public static Dictionary<string, GameObject> playerInstantiation = new Dictionary<string, GameObject>();
    public static Dictionary<string, object> GlobalSync = new Dictionary<string, object>();
    public static int myTeam;    
    public static Account account;
    public static PlayerClient playerClient;
    public static string curMasterClient;
    public static bool IsMasterCertificate;
    public static string clientLang;
    public static int gameID;
}

public class SetGlobal
{
    public string key;
    public object value;

    public SetGlobal(string key, object value)
    {
        this.key = key; 
        this.value = value;
    }

    public static void setValue(string key, object value)
    {
        SetGlobal setGlobal = new SetGlobal(key, value);
        Event.emit(Events.setGlobalValue, setGlobal);
    }

    public static object getValue(string key)
    {
        if (Global.GlobalSync.ContainsKey(key))
            return Global.GlobalSync[key];
        else 
            return null;
    }

    public static void deleteAll()
    {
        Global.GlobalSync = new Dictionary<string, object>();
    }
}
