using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioButtonClick : MonoBehaviour
{
    public AudioClip clip;
    void Start()
    {
        Button button = GetComponent<Button>();
        button.onClick.AddListener(playSound);
    }

    private void playSound()
    {
        Event.emit(Events.playSound, clip);
    }
}
