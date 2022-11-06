using Assets.script.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class TokenManager : MonoBehaviour
{
    private void Awake()
    {
        Event.register(Events.gameOnStart, gameOnStart);
    }
    public void gameOnStart(object context)
    {
        StartCoroutine(token());
    }

    IEnumerator token()
    {
        WWWForm form = new WWWForm();
        string _token = PlayerPrefs.GetString("_token");
        Debug.Log(_token);
        form.AddField("_token", _token);

        using (UnityWebRequest www = UnityWebRequest.Post(URL.account_token, form))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
                Event.emit(Events.login, null);
            }
            else
            {
                string json = www.downloadHandler.text;
                Account account = JsonUtility.FromJson<Account>(json);
                Event.emit(Events.loggedIn, account);

            }
        }
    }
}
