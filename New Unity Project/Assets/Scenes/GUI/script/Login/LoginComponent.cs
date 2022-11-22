using Assets.script.Lib;
using Assets.script.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using TMPro;

public class LoginComponent : MonoBehaviour
{
    private string username;
    private string password;
    public void btnLogin_Click()
    {
        Transform tbUsername = transform.Find("tbUsserName").Find("Text");
        Transform tbPassword = transform.Find("tbPassword").Find("Text");
        username = tbUsername.GetComponent<Text>().text;
        password = tbPassword.GetComponent<Text>().text;
        StartCoroutine(login());
    }

    public void btnReigner_Click()
    {
        Event.emit(Events.register, null);
    }

    IEnumerator login()
    {
        WWWForm form = new WWWForm();
        form.AddField("username", username);
        form.AddField("password", password);

        using (UnityWebRequest www = UnityWebRequest.Post(URL.account_login, form))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
                Json.Msg msg = JsonUtility.FromJson<Json.Msg>(www.downloadHandler.text);
                setMsgError(msg.msg);
            }
            else
            {
                string json = www.downloadHandler.text;
                Account account = JsonUtility.FromJson<Account>(json);
                Event.emit(Events.loggedIn, account);

            }
        }
    }

    private void setMsgError(string msg)
    {
        transform.Find("msg").GetComponent<TextMeshProUGUI>().text = Lang.toText(msg);
    }
}
