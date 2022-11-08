using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StadiumUI : MonoBehaviour
{
    public Text Time;
    public Text blue_score;
    public Text red_score;
    public Sprite[] countDowns;
    public Image countDownObject;

    private int eventID;
    private void Awake()
    {
        eventID = Event.register(Events.onSetValueGlobal, onSetValueGlobal);
    }

    public void onSetValueGlobal (object _key)
    {
        string key = (string) _key;
        switch (key)
        {
            case Value.TimeWait:
                int time = (int)SetGlobal.getValue(key);
                Time.text = time.ToString();
                if(time == 0)
                {
                    Event.emit(Events.onBeginGameStart, null);
                    StartCoroutine(countDownt());
                }
                break;
            case Value.Time:
                int time2 = (int)SetGlobal.getValue(key);
                TimeSpan timeSpan = TimeSpan.FromSeconds(time2);
                string str = timeSpan.ToString(@"mm\:ss");
                Time.text = str;
                break;
            case Value.redScore:
                int redScore = (int)SetGlobal.getValue(key);
                red_score.text = redScore.ToString();
                break;
            case Value.blueScore:
                int blueScore = (int)SetGlobal.getValue(key);
                blue_score.text = blueScore.ToString();
                break;
        }
    }

    IEnumerator countDownt()
    {
        countDownObject.gameObject.SetActive(true);
        foreach (Sprite item in countDowns)
        {
            countDownObject.sprite = item;
            yield return new WaitForSeconds(1);
        }
        countDownObject.gameObject.SetActive(false);
        Event.emit(Events.onGameStart, null);
    }

    private void OnDisable()
    {
        Event.unRegister(Events.onSetValueGlobal, eventID);
    }
}
