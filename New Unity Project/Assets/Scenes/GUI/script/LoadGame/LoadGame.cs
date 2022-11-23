using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Assets.script.Lib;
using UnityEngine.SceneManagement;

public class LoadGame : MonoBehaviour
{

    public TextMeshProUGUI status;
    public Slider process;
    private int eventID1;
    private int eventID2;
    private int eventID3;
    private int eventID4;
    private int eventID5;
    private bool start;

    private void Awake()
    {
        eventID1 = Event.register(Events.loggedIn, delegate(object context) { status.text = Lang.toText("connecting"); });
        eventID2 = Event.register(Events.canConnect, connected);
        eventID3 = Event.register(Events.goToLobby, delegate (object context) { start = true; });
        eventID4 = Event.register(Events.login, UnloadScene);
        eventID5 = Event.register(Events.createPlayer, UnloadScene);
    }

    private void Start()
    {
        status.text = Lang.toText("logged_in");
    }

    // Update is called once per frame
    void Update()
    {
        if (start)
        {
            process.value += 0.01f;
            status.text = Lang.toText("loading");
            if (process.value >= 1)
            {
                SceneManager.UnloadSceneAsync(SceneName.LoadGame);
            }
        }
    }

    private void connected(object context)
    {
        status.text = Lang.toText("connected");
    }

    private void UnloadScene(object context)
    {
        SceneManager.UnloadSceneAsync(SceneName.LoadGame);
    }

    private void OnDisable()
    {
        Event.unRegister(Events.loggedIn, eventID1);
        Event.unRegister(Events.canConnect, eventID2);
        Event.unRegister(Events.goToLobby, eventID3);
        Event.unRegister(Events.login, eventID4);
        Event.unRegister(Events.createPlayer, eventID5);
    }
}
