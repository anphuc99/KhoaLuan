using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadStadium : MonoBehaviour
{
    public Slider progressBar;
    private bool start = false;
    private int event_id;
    // Start is called before the first frame update
    private void Awake()
    {
        event_id = Event.register(Events.goToGame, goToGame);
    }

    private void goToGame(object context)
    {
        start = true;
    }

    private void FixedUpdate()
    {
        if (start)
        {
            progressBar.value += 0.01f;
            if (progressBar.value >= 1)
            {
                SceneManager.UnloadSceneAsync(SceneName.LoadStadium);
            }
        }
    }

    private void OnDisable()
    {
        Event.unRegister(Events.goToGame, event_id);
    }
}
