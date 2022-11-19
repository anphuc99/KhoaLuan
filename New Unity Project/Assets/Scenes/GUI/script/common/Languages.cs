using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Assets.script;

public class Languages : MonoBehaviour
{
    public TMP_FontAsset fontVi;
    public TMP_FontAsset fontEn;
    public Material materialVi;
    public Material materialEn;
    public string key;
    private TextMeshProUGUI tmp;
    private Dictionary<string, TMP_FontAsset> fontLangs = new Dictionary<string, TMP_FontAsset>();
    private Dictionary<string, Material> materialLangs = new Dictionary<string, Material>();
    private string langCSVPath = "/Lang/Lang.csv";
    private string curLang;

    private int eventID;
    private void Awake()
    {
        eventID = Event.register(Events.setLanguage, setLang);
    }
    void Start()
    {
        tmp = GetComponent<TextMeshProUGUI>();
        fontLangs.Add("en", fontEn);
        fontLangs.Add("vi", fontVi);
        materialLangs.Add("en", materialEn);
        materialLangs.Add("vi", materialVi);
        setLang(null);
    }

    private void setFont()
    {
        tmp.font = fontLangs[curLang];
    }

    private void setMaterial()
    {
        tmp.fontMaterial = materialLangs[curLang];
    }

    private void setLang(object context)
    {
        curLang = PlayerPrefs.GetString("Lang");
        Debug.Log(curLang);
        List<Dictionary<string, string>> list = CsvHelper.fromCsv(Application.dataPath + langCSVPath);
        foreach (Dictionary<string, string> pair in list)
        {
            if (pair["key"] == key)
            {
                tmp.text = pair[curLang];
                break;
            }
        }
        setFont();
        setMaterial();
    }

    private void OnDisable()
    {
        Event.unRegister(Events.setLanguage, eventID);
    }
}
