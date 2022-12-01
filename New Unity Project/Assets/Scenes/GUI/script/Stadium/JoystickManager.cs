using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoystickManager : MonoBehaviour
{
    private Joystick joystick;
    private void Awake()
    {
        if(Application.platform == RuntimePlatform.WindowsPlayer)
        {
            Destroy(gameObject);
            return;
        }
    }

    private void Start()
    {
        joystick = GetComponent<Joystick>();
    }

    private void FixedUpdate()
    {
        if(joystick != null)
        {
            Event.emit(Events.horizotalSpeed, joystick.Horizontal);
            Event.emit(Events.verticalSpeed, joystick.Vertical);
        }
    }

    public void btnJump_Click()
    {
        Event.emit(Events.jumpSpeed, null);
    }

}
