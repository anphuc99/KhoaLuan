using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using TMPro;
using Assets.script.Lib;

public class ChooseCharater : MonoBehaviour
{
    private string name;
    private string _token;
    public Animator animator;
    public TextMeshProUGUI msgText;
    private void Start()
    {
        StartCoroutine(setAnim());
    }

    IEnumerator setAnim()
    {
        animator.SetBool("jumpUp", true);
        animator.SetTrigger("jumpDown");
        yield return new WaitForSeconds(1);
        animator.SetBool("jumpUp", false);
    }
    public void btnStart_Click()
    {
        transform.Find("Dialog").gameObject.SetActive(true);
    }

    public void btnComfim_Click()
    {
        Text text = transform.Find("Dialog/Background/InputField/Text").gameObject.GetComponent<Text>();
        name = text.text;
        _token = PlayerPrefs.GetString("_token");
        StartCoroutine(createCharater());
    }
    IEnumerator createCharater()
    {
        WWWForm form = new WWWForm();
        form.AddField("name", name);
        form.AddField("_token", _token);

        using (UnityWebRequest www = UnityWebRequest.Post(URL.player_chooseCharacter, form))
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
                Event.emit(Events.goToLobby, json);
                Event.emit(Events.setAttribule, json);
            }
        }
    }

    private void setMsgError(string msg)
    {
        msgText.text = Lang.toText(msg);
    }
}
