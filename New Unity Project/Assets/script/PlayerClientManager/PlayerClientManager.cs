using Assets.script.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerClientManager : MonoBehaviour
{
    private Account account;
    public int id;
    public string name;
    public int account_id;
    public int score;
    private void Awake()
    {
        Event.register(Events.loggedIn, logedIn);
        Event.register(Events.setAttribule, setAttribule);
        Event.register(Events.endGame, endGame);
    }

    public void logedIn(object context)
    {
        account = (Account)context;
        Account account1 = (Account)context;
        PlayerPrefs.SetString("_token", account1._token);
        PlayerPrefs.Save();
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
        JsonUtility.FromJsonOverwrite((string)json, this);
    }

    private void endGame(object context)
    {
        Event.emit(Events.goBack, null);
    }
}
