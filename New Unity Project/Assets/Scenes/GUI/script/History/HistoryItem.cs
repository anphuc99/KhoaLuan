using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HistoryItem : MonoBehaviour
{
    public int gameID;
    public int serial;
    public DateTime date;
    public int result;
    public Sprite[] bg;
    private void Start()
    {
        Text seri = transform.Find("serial").GetComponent<Text>();
        seri.text = serial.ToString();
        Text dt = transform.Find("date").GetComponent<Text>();
        Image image = transform.Find("bgItem").GetComponent<Image>();
        image.sprite = bg[result]; 
        if (PlayerPrefs.GetString("Lang") == "vi")
        {
            dt.text = date.ToString("dd-MM-yyyy");
        }
        else if (PlayerPrefs.GetString("Lang") == "en")
        {
            dt.text = date.ToString("MM-dd-yyyy");
        }

        Text fan_count = transform.Find("fans_count").GetComponent<Text>();
        if (result == 0)
        {
            fan_count.text = "+100";
        }
        else if (result == 1)
        {
            fan_count.text = "+0";
        }
        else if (result == 2)
        {
            fan_count.text = "-50";
        }
    }

    public void showResult()
    {
        Global.gameID = gameID;
        Event.emit(Events.showResult, null);
    }
}
