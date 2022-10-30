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
        Event.register(Events.goBack, goBack);
        Event.register(Events.onStart, onStart);
        Event.register(Events.register, register);
        Event.register(Events.goToLobby, goToLobby);
        Event.register(Events.createPlayer, createPlayer);
        //PlayerPrefs.DeleteAll();
    }
    // Start is called before the first frame update
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

    public void goBack(object context)
    {
        if (curScene != null)
            SceneManager.UnloadSceneAsync(curScene);
        SceneManager.LoadSceneAsync(SceneName.Lobby, LoadSceneMode.Additive);
        curScene = SceneName.Lobby;
    }

    public void onStart(object context)
    {
        if (curScene != null)
            SceneManager.UnloadSceneAsync(SceneName.Lobby);
        SceneManager.LoadSceneAsync(SceneName.Wait4Player, LoadSceneMode.Additive);
        curScene = SceneName.Wait4Player;
    }
}
