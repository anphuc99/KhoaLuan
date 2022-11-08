using Assets.script.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private string curScene;
    private void Awake()
    {
        Event.register(Events.login, login);        
        Event.register(Events.goBack, goToLobby);
        Event.register(Events.onStart, onStart);
        Event.register(Events.register, register);
        Event.register(Events.goToLobby, goToLobby);
        Event.register(Events.createPlayer, createPlayer);
        Event.register(Events.enoughPlayers, enoughPlayers);
        //PlayerPrefs.DeleteAll();
    }
    // Start is called before the first frame update
    private void Start()
    {
        if (curScene != null)
            SceneManager.UnloadSceneAsync(curScene);
        SceneManager.LoadSceneAsync(SceneName.Test, LoadSceneMode.Additive);
        curScene = SceneName.Test;

        //Event.emit(Events.gameOnStart, null);
    }

    public void login(object context)
    {
        if (curScene != null)
            SceneManager.UnloadSceneAsync(curScene);
        SceneManager.LoadSceneAsync(SceneName.Login, LoadSceneMode.Additive);
        curScene = SceneName.Login;
    }

    public void createPlayer(object context)
    {
        if (curScene != null)
            SceneManager.UnloadSceneAsync(curScene);
        SceneManager.LoadSceneAsync(SceneName.ChooseCharater, LoadSceneMode.Additive);
        curScene = SceneName.ChooseCharater;
    }

    public void goToLobby(object context)
    {
        if (curScene != null)
            SceneManager.UnloadSceneAsync(curScene);
        SceneManager.LoadSceneAsync(SceneName.Lobby, LoadSceneMode.Additive);
        curScene = SceneName.Lobby;
    }

    public void register(object context)
    {
        if (curScene != null)
            SceneManager.UnloadSceneAsync(SceneName.Login);
        SceneManager.LoadSceneAsync(SceneName.Register, LoadSceneMode.Additive);
        curScene = SceneName.Register;
    }

    public void onStart(object context)
    {
        if (curScene != null)
            SceneManager.UnloadSceneAsync(SceneName.Lobby);
        SceneManager.LoadSceneAsync(SceneName.Wait4Player, LoadSceneMode.Additive);
        curScene = SceneName.Wait4Player;
    }

    public void enoughPlayers(object context)
    {
        if (curScene != null)
            SceneManager.UnloadSceneAsync(curScene);
        //SceneManager.LoadSceneAsync(SceneName.LoadStadium, LoadSceneMode.Additive);
        SceneManager.LoadSceneAsync(SceneName.Stadium, LoadSceneMode.Additive);
        curScene = SceneName.Stadium;
    }
}
