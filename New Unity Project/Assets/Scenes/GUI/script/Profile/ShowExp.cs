using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShowExp : MonoBehaviour
{
    public TextMeshProUGUI textMeshPro;
    private Slider slider;    
    // Start is called before the first frame update
    void Start()
    {
        slider = GetComponent<Slider>();
        slider.maxValue = Mathf.Floor(Mathf.Pow(1.4f,Global.playerClient.level-1) + 800);
        slider.value = Global.playerClient.exp;
        textMeshPro.text = slider.value +"/"+slider.maxValue;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
