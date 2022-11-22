using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioMusic : MonoBehaviour
{
    public AudioClip lobby;
    public AudioClip stadium;    
    private AudioSource audioSource;

    private void Awake()
    {
        Event.register(Events.goBack, playLobbySound);
        Event.register(Events.goToLobby, playLobbySound);
        Event.register(Events.goToGame, playeStadiumSound);
        Event.register(Events.endGame, playLobbySound);
        Event.register(Events.setVolumeMusic, setVolumeMusic);
    }

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = lobby;
        audioSource.Play();
        setVolumeMusic(null);
    }

    public void playLobbySound(object context)
    {
        if (audioSource.clip != lobby)
        {
            audioSource.clip = lobby;
            audioSource.Play();
        }
    }

    public void playeStadiumSound(object context)
    {
        if(audioSource.clip != stadium)
        {
            audioSource.clip = stadium;
            audioSource.Play();
        }
    }

    public void setVolumeMusic(object context)
    {
        if (context != null)
        {
            audioSource.volume = (float)context;
        }
        else
        {
            audioSource.volume = PlayerPrefs.HasKey("volumeMusic") ? PlayerPrefs.GetFloat("volumeMusic") : 1;
        }
    }
}
