using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowResuitTeam : MonoBehaviour
{
    public Sprite win;
    public Sprite lose;
    public Sprite draw;
    public Image imgResults;    
    private int eventID;

    private void Awake()
    {
        eventID = Event.register(Events.resultsTeamWin, resultGame);
    }

    private void resultGame(object _team)
    {
        Debug.Log("cho de, cho chet");
        int team = (int)_team;
        if (team == -1)
        {
            imgResults.sprite = draw;
            imgResults.transform.Find("Text").GetComponent<Text>().text = "DRAW";
        }
        else
        {
            int myTeam = Global.myTeam;
            if (team != myTeam)
            {
                imgResults.sprite = lose;
                imgResults.transform.Find("Text").GetComponent<Text>().text = "LOSE";
            }
            else
            {
                imgResults.sprite = win;
                imgResults.transform.Find("Text").GetComponent<Text>().text = "WIN";
            }
        }
        imgResults.gameObject.SetActive(true);
    }

    private void OnDisable()
    {
        Event.unRegister(Events.resultsTeamWin, eventID);
    }
}
