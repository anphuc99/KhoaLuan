using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Networking;

public class AddPointToServer : MonoBehaviour
{
    public Slider speed;
    public Slider jump;
    public Slider shotForce;
    public TextMeshProUGUI point;
    void Start()
    {
        Button button = GetComponent<Button>();
        button.onClick.AddListener(sendToServer);
    }

    private void sendToServer()
    {
        StartCoroutine(send());
    }

    IEnumerator send()
    {
        WWWForm form = new WWWForm();
        form.AddField("speed", (int)speed.value);
        form.AddField("jump", (int)jump.value);
        form.AddField("shotForce", (int)shotForce.value);
        form.AddField("point", int.Parse(point.text));
        form.AddField("_token", Global.account._token);

        using (UnityWebRequest www = UnityWebRequest.Post(URL.player_setMultiplier, form))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
            }
            else
            {
                Global.playerClient.speed = (int)speed.value;
                Global.playerClient.jump = (int)jump.value;
                Global.playerClient.shotForce = (int)shotForce.value;
                Global.playerClient.point = int.Parse(point.text);
                Event.emit(Events.setMultiplier, null);
            }
        }
    }

    
}
