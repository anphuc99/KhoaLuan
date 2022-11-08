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
        Event.register(Events.endGame, endGame);
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
        Debug.Log(URL.account_register + form.ToString());
        
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
    }

    private void endGame(object context)
    {
        Event.emit(Events.goBack, null);
    }
}
