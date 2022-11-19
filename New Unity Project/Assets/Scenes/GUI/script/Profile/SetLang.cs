using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetLang : MonoBehaviour
{
    public string[] lang;
    private int indexLang;
    public Sprite vi;
    public Sprite en;

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
        setFlag();
    }

    public void btnArrowLeftLang_Click()
    {
        if (indexLang > 0)
        {
            indexLang--;
            PlayerPrefs.SetString("Lang", lang[indexLang]);
            Global.clientLang = lang[indexLang];
            Event.emit(Events.setLanguage, null);
            setFlag();
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
            setFlag();
        }
    }

    private void setFlag()
    {
        string lang = Global.clientLang;
        switch (lang)
        {
            case "vi":
                GetComponent<Image>().sprite = vi;
                break;
            case "en":
                GetComponent<Image>().sprite = en;
                break;
        }
    }
}
