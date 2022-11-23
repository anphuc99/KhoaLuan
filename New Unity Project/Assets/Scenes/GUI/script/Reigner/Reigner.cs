using Assets.script.Player;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using UnityEngine.Windows;
using TMPro;
using Assets.script.Lib;

public class Reigner : MonoBehaviour
{
    private string name;
    private string email;
    private string username;
    private string password;
    private string comfimPassword;
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
            string[] tbs = { "table/tbName", "table/tbEmail", "table2/tbUserName", "table2/tbPassword", "table2/tbComfimPassword" };
            transform.Find("table").gameObject.SetActive(true);
            foreach (string s in tbs)
            {
                Debug.Log(s);
                Debug.Log(transform.Find(s));
                InputField text = transform.Find(s).GetComponent<InputField>();
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
                    case "table2/tbComfimPassword":
                        comfimPassword = text.text;
                        break;
                }
                transform.Find("table").gameObject.SetActive(false);
            }
            checkEmail();
            ComfimPassword();
            StartCoroutine(register());
        }
        catch(Exception e)
        {
            Debug.Log(e.Message);
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
                Json.Msg msg = JsonUtility.FromJson<Json.Msg>(www.downloadHandler.text);
                setMsgError(msg.msg);
            }
            else
            {
                string json = www.downloadHandler.text;
                Account account = JsonUtility.FromJson<Account>(json);
                PlayerPrefs.SetString("_token", account._token);
                PlayerPrefs.Save();
                Event.emit(Events.createPlayer, account);
                Global.account = account;
            }
        }
    }

    public void checkEmail()
    {
        var trimmedEmail = email.Trim();

        if (trimmedEmail.EndsWith("."))
        {
            throw new Exception("email_incorrect");
        }
        try
        {
            var addr = new System.Net.Mail.MailAddress(email);
        }
        catch
        {
            throw new Exception("email_incorrect");
        }
    }
    public void ComfimPassword()
    {
        if (!password.Equals(comfimPassword))
         {
            throw new Exception("comfim_password_incorrect");
        }
    }

    public void setMsgError(string msg)
    {
        transform.Find("table2/msg").GetComponent<TextMeshProUGUI>().text = Lang.toText(msg);
    }
}
