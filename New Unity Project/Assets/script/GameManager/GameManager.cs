using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private void Awake()
    {
        Event.register(Events.loggedIn, Login);
    }
    // Start is called before the first frame update
    void Start()
    {
        SceneManager.LoadScene(SceneName.Login, LoadSceneMode.Additive);
    }

    public void Login(Dictionary<string, object> context)
    {        
        SceneManager.UnloadSceneAsync(SceneName.Login);
        SceneManager.LoadScene(SceneName.Lobby, LoadSceneMode.Additive);
    }
}
