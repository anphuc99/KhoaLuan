using Assets.script.Player;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SetMulti : MonoBehaviour
{
    public string multiplier;
    private int eventID;
    private void Awake()
    {
        eventID = Event.register(Events.setMultiplier, setMultiplier);
    }
    void Start()
    {
        setMultiplier(null);
    }

    private void setMultiplier(object context)
    {
        PlayerClient playerClient = Global.playerClient;
        //PlayerClient playerClient = new PlayerClient()
        //{
        //    account_id = 0,
        //    name = "haha",
        //    level = 1,
        //    speed = 10,
        //    jump = 10,
        //    shotForce = 10,
        //    point = 10,
        //    fans = 10,
        //    exp = 10,
        //};
        Debug.Log(playerClient.point);
        Global.playerClient = playerClient;
        TextMeshProUGUI textMeshProUGUI = null;
        if (TryGetComponent<TextMeshProUGUI>(out textMeshProUGUI))
        {
            textMeshProUGUI.text = playerClient[multiplier].ToString();
        }
        Slider slider = null;
        if (TryGetComponent<Slider>(out slider))
        {
            Debug.Log("ccccc");
            slider.value = float.Parse(playerClient[multiplier].ToString());
        }
    }

    private void OnDisable()
    {
        Event.unRegister(Events.setMultiplier, eventID);
    }
}
