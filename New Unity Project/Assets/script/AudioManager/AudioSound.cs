using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSound : MonoBehaviour
{
    private AudioSource audioSource;
    private void Awake()
    {
        Event.register(Events.setVolumeSound, setVolumeSound);
        Event.register(Events.playSound, playSound);
    }
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void setVolumeSound(object context)
    {
        if (context != null)
        {
            audioSource.volume = (float)context;
        }
        else
        {
            audioSource.volume = PlayerPrefs.HasKey("volumeSound") ? PlayerPrefs.GetFloat("volumeSound") : 1;
        }

    }

    private void playSound(object clip)
    {        
        audioSource.clip = clip as AudioClip;
        audioSource.Play();
    }
}
