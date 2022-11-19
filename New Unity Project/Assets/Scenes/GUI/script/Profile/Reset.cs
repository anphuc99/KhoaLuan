using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class Reset : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Button button = GetComponent<Button>();
        button.onClick.AddListener(resetPoint);
    }

    private void resetPoint()
    {
        StartCoroutine(reset());
    }

    IEnumerator reset()
    {
        WWWForm form = new WWWForm();
        form.AddField("speed", 0);
        form.AddField("jump", 0);
        form.AddField("shotForce", 0);
        form.AddField("point", Global.playerClient.level);
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
                Global.playerClient.speed = 0;
                Global.playerClient.jump = 0;
                Global.playerClient.shotForce = 0;
                Global.playerClient.point = Global.playerClient.level;
                Event.emit(Events.setMultiplier, null);
            }
        }
    }
}
