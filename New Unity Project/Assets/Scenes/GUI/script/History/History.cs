using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Networking;

public class History : MonoBehaviour
{
    public GameObject historyItemPerfap;
    void Start()
    {
        StartCoroutine(getHistory());
    }

    IEnumerator getHistory()
    {
        using (UnityWebRequest www = UnityWebRequest.Get(URL.game_getHistory+"/"+Global.account.id))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
            }
            else
            {
                string json = www.downloadHandler.text;
                Json.HistoryInfo[] historyInfos = JsonHelper.FromJson<Json.HistoryInfo>(json);
                setHistoryItem(historyInfos);
            }
        }
    }

    private void setHistoryItem(Json.HistoryInfo[] historyInfos)
    {
        int i = 1;
        foreach (Json.HistoryInfo historyInfo in historyInfos)
        {
            GameObject history = Instantiate(historyItemPerfap);
            HistoryItem historyItem = history.GetComponent<HistoryItem>();
            historyItem.date = DateTime.Parse(historyInfo.date);
            historyItem.result = historyInfo.result;
            historyItem.serial = i;
            historyItem.gameID = historyInfo.gameID;
            history.transform.parent = transform;
            history.transform.localScale = Vector3.one;
            i++;
        }
    }

}
