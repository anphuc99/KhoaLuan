using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressBar : MonoBehaviour
{
    public float progress;
    void Start()
    {
        transform.GetChild(0).localScale = new Vector3(progress, 1, 1);
    }

    public void setProgress(float value)
    {
        if (value < 1)
        {
            progress = value;
            transform.GetChild(0).localScale = new Vector3(value, 1, 1);
        }
        else
        {
            progress = 1;
            transform.GetChild(0).localScale = new Vector3(1, 1, 1);
        }
    }

    public void addProgress(float value)
    {
        progress += value;
        if (progress < 1)
        {
            transform.GetChild(0).localScale = new Vector3(progress, 1, 1);
        }
        else
        {
            progress = 1;
            transform.GetChild(0).localScale = new Vector3(1, 1, 1);
        }
    }


}
