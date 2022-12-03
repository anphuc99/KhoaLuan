using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonPress : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public bool buttonDown;
    public bool buttonUp;
    public bool buttonPress;
    public delegate void OnButtonPressed(Vector2 pos);
    public OnButtonPressed onButtonPressed;
    public Touch pointerEventData;
    private int tuochID;
    private Vector2? oldPos = null;

    private void Awake()
    {
        if(Application.platform == RuntimePlatform.WindowsPlayer || Application.platform == RuntimePlatform.WindowsEditor)
        {
            Destroy(this);
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        buttonDown = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        buttonUp = true;
    }

    private void FixedUpdate()
    {
        if (buttonDown)
        {
            if (Input.touchCount >= 1)
            {
                tuochID = Input.touchCount - 1;
                Debug.Log("[Debug ButtonPressed]: "+tuochID);
            }            
            buttonDown = false;
            buttonPress = true;
        }

        if (buttonUp)
        {
            buttonUp = false;
            buttonPress = false;
            oldPos = null;
        }

        if (buttonPress)
        {
            Debug.Log("tuochID: "+tuochID);
            Vector2 newpos = Input.GetTouch(tuochID).position;
            Vector2 pos = newpos - oldPos ?? newpos;
            Debug.Log(pos.normalized);
            oldPos = newpos;
            if (onButtonPressed != null)
            {
                if(pos.magnitude < 2)
                {
                    onButtonPressed(pos);
                }
                else
                {
                    onButtonPressed(pos.normalized*2);
                }
            }
        }
    }
}
