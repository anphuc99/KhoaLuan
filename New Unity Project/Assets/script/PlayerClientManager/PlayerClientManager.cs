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
    }

    public void logedIn(object context)
    {
        Debug.Log("cjh");
        account = (Account) context;
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
                JsonUtility.FromJsonOverwrite(json, this);
                Event.emit(Events.goToLobby, null);
            }
        }
    }
}
