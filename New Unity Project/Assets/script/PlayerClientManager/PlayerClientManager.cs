using Assets.script.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerClientManager : MonoBehaviour
{
    private Account account;
    private PlayerClient playerClient;
    private void Awake()
    {
        Event.register(Events.loggedIn, logedIn);
        Event.register(Events.setAttribule, setAttribule);
    }

    public void logedIn(object context)
    {
        account = (Account)context;
        PlayerPrefs.SetString("_token", account._token);
        PlayerPrefs.Save();
        Global.account = account;        
        StartCoroutine(checkPlayer());
    }

    IEnumerator checkPlayer()
    {
        WWWForm form = new WWWForm();    
        form.AddField("account_id", account.id);
        
        using (UnityWebRequest www = UnityWebRequest.Post(URL.player_checkPlayer,form))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
                Event.emit(Events.createPlayer, null);
            }
            else
            {
                string json = www.downloadHandler.text;
                setAttribule(json);
                Event.emit(Events.goToLobby, null);
            }
        }
    }

    private void setAttribule(object json)
    {
        playerClient = new PlayerClient();
        JsonUtility.FromJsonOverwrite((string)json, playerClient);
        Global.playerClient = playerClient;
        Event.emit(Events.canConnect, null);
    }

    //IEnumerator createRoomRequest(string roomid)
    //{
    //    WWWForm form = new WWWForm();
    //    form.AddField("_token", account._token);
    //    form.AddField("roomID", roomid);

    //    using (UnityWebRequest www = UnityWebRequest.Post(URL.game_createRoom, form))
    //    {
    //        yield return www.SendWebRequest();

    //        if (www.result != UnityWebRequest.Result.Success)
    //        {
    //            Debug.Log(www.error);
    //            Event.emit(Events.goBack, null);
    //        }            
    //    }
    //}

    //private void onJoinRoom(object roomid)
    //{
    //    StartCoroutine(joinRoomRequest((string)roomid));
    //}

    //IEnumerator joinRoomRequest(string roomid)
    //{
    //    WWWForm form = new WWWForm();
    //    form.AddField("_token", account._token);
    //    form.AddField("roomID", roomid);

    //    using (UnityWebRequest www = UnityWebRequest.Post(URL.game_joinRoom, form))
    //    {
    //        yield return www.SendWebRequest();

    //        if (www.result != UnityWebRequest.Result.Success)
    //        {
    //            Debug.Log(www.error);
    //            Event.emit(Events.goBack, null);
    //        }
    //    }
    //}
}
