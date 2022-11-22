using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Setting : MonoBehaviour
{
    public string[] lang;
    public Slider volumSound;
    public Slider volumMusic;
    private int indexLang;

    private void Start()
    {
        string curLang = Global.clientLang;
        for (int i = 0; i < lang.Length; i++)
        {
            if (lang[i] == curLang)
            {
                indexLang = i;
            }
        }
        volumMusic.value = PlayerPrefs.HasKey("volumeMusic") ? PlayerPrefs.GetFloat("volumeMusic") : 1;
        volumSound.value = PlayerPrefs.HasKey("volumeSound") ? PlayerPrefs.GetFloat("volumeSound") : 1;
    }

    public void btnArrowLeftLang_Click()
    {
        if (indexLang > 0)
        {
            indexLang--;
            PlayerPrefs.SetString("Lang", lang[indexLang]);
            Global.clientLang = lang[indexLang];
            Event.emit(Events.setLanguage, null);
        }
    }
    
    public void btnArrowRightLang_Click()
    {
        if (indexLang < lang.Length - 1)
        {
            indexLang++;
            PlayerPrefs.SetString("Lang", lang[indexLang]);
            Global.clientLang = lang[indexLang];
            Event.emit(Events.setLanguage, null);
        }
    }

    public void setVoulmeSound(float value)
    {
        Event.emit(Events.setVolumeSound, value);
    }

    public void setVolumeMusic(float value)
    {
        Event.emit(Events.setVolumeMusic, value);
    }

    public void btnSave_Click()
    {
        PlayerPrefs.SetFloat("volumeMusic", volumMusic.value);
        PlayerPrefs.SetFloat("volumeSound", volumSound.value);
        Event.emit(Events.setVolumeMusic, null);
        Event.emit(Events.setVolumeSound, null);
        Event.emit(Events.goBack, null);
    }

    public void btnCancel_Click()
    {
        Event.emit(Events.setVolumeMusic, null);
        Event.emit(Events.setVolumeSound, null);
        Event.emit(Events.goBack, null);
    }
}
