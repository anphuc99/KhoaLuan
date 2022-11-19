using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Setting : MonoBehaviour
{
    public string[] lang;
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
}
