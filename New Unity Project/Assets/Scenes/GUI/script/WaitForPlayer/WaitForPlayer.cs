using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaitForPlayer : MonoBehaviour
{
    public GameObject[] players;
    public GameObject btnAccept;    
    private int countPlayerAccept;
    private int[] eventIDs;
    private void Awake()
    {
        eventIDs = new int[2];
        eventIDs[0] = Event.register(Events.numberOfPlayersChange, numberOfPlayersChange);
        eventIDs[1] = Event.register(Events.playerAccept, playerAccept);
    }

    private void numberOfPlayersChange(object num)
    {
        int number = (int)num;
        showPlayerIn(ref number);
        resetPlayerAccept();
        showAcceptButton(ref number);
    }

    private void resetPlayerAccept()
    {
        countPlayerAccept = 0;
        countPlayer();
        btnAccept.GetComponent<Button>().interactable = true;
    }

    private void showPlayerIn(ref int number)
    {
        for (int i = 1; i <= Define.MaxPlayers; i++)
        {
            if (i <= number)
            {
                players[i - 1].SetActive(true);
            }
            else
            {
                players[i - 1].SetActive(false);
            }
        }
    }

    private void showAcceptButton(ref int number)
    {
        if (number == Define.MaxPlayers)
        {
            btnAccept.SetActive(true);
        }
        else
        {
            btnAccept.SetActive(false);
        }
    }

    public void Accept()
    {        
        btnAccept.GetComponent<Button>().interactable = false;
        Event.emit(Events.onAccept, null);
    }

    public void playerAccept(object player)
    {
        countPlayerAccept++;
        countPlayer();
        movePlayerToGame();
    }

    private void countPlayer()
    {
        for (int i = 1; i <= Define.MaxPlayers; i++)
        {
            if (i <= countPlayerAccept)
            {
                players[i - 1].GetComponent<Button>().interactable = true;
            }
            else
            {
                players[i - 1].GetComponent<Button>().interactable = false;
            }
        }
    }

    private void movePlayerToGame()
    {
        if (countPlayerAccept >= Define.MaxPlayers)
        {
            Event.emit(Events.enoughPlayers, null);
        }
    }

    private void OnDisable()
    {
        Event.unRegister(Events.numberOfPlayersChange, eventIDs[0]);
        Event.unRegister(Events.playerAccept, eventIDs[1]);
    }
}
