using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Assets.script.Player;

public class AddPoint : MonoBehaviour
{
    public string point;
    public Slider slider;
    public TextMeshProUGUI textMeshPro;
    void Start()
    {
        Button button = GetComponent<Button>();
        button.onClick.AddListener(addPoint);
    }

    private void addPoint()
    {
        int point = int.Parse(textMeshPro.text);
        if (point > 0)
        {
            slider.value++;
            point--;
            textMeshPro.text = point.ToString();
        }
    }
}
