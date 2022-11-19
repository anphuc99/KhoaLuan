using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShowProgressNumber : MonoBehaviour
{
    public TextMeshProUGUI textMeshPro;
    private Slider mainSlider;
    void Start()
    {
        mainSlider = GetComponent<Slider>();
        mainSlider.onValueChanged.AddListener(delegate { setNunber(); });
        setNunber();
    }

    private void setNunber()
    {
        textMeshPro.text = Mathf.Floor(mainSlider.value).ToString();
    }
}
