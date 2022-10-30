using Assets.script.Player;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using UnityEngine.Windows;

public class Reigner : MonoBehaviour
{
    private string name;
    private string email;
    private string username;
    private string password;
    public void btnNext_Click()
    {
        transform.Find("table").gameObject.SetActive(false);
        transform.Find("table2").gameObject.SetActive(true);
    }

    public void btnPre_Click()
    {
        transform.Find("table").gameObject.SetActive(true);
        transform.Find("table2").gameObject.SetActive(false);
    }

    public void btnRegister_Click()
    {
        try
        {
            string[] tbs = { "table/tbName", "table/tbEmail", "table2/tbUserName", "table2/tbPassword" };
            transform.Find("table").gameObject.SetActive(true);
            foreach (string s in tbs)
            {
                Debug.Log(s);
                Debug.Log(transform.Find(s));
                Text text = transform.Find(s).Find("Text").GetComponent<Text>();
                switch (s)
                {
                    case "table/tbName":
                        name = text.text;
                        break;
                    case "table/tbEmail":
                        email = text.text;
                        break ;
                    case "table2/tbUserName":
                        username = text.text;
                        break;
                    case "table2/tbPassword":
                        password = text.text;
                        break;
                }
                transform.Find("table").gameObject.SetActive(false);
            }
            comfimPassword();
            StartCoroutine(register());
        }
        catch(Exception e)
        {
            setMsgError(e.Message);
        }
    }

    IEnumerator register()
    {
        WWWForm form = new WWWForm();
        form.AddField("name", name);
        form.AddField("email", email);
        form.AddField("username", username);        
        form.AddField("password", password);

        using (UnityWebRequest www = UnityWebRequest.Post(URL.account_register, form))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
                setMsgError(www.error);
            }
            else
            {
                string json = www.downloadHandler.text;
                Account account = JsonUtility.FromJson<Account>(json);
                PlayerPrefs.SetString("_token", account._token);
                PlayerPrefs.Save();
                Event.emit(Events.createPlayer, account);
                 
            }
        }
    }
    public void comfimPassword()
    {

    }

    public void setMsgError(string msg)
    {

    }
}
