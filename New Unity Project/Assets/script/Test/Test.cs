using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    public void btnLogin_Click()
    {
        Event.emit(Events.login, null);
    }

    public void btnLobby_Click()
    {
        Event.emit(Events.goToLobby, null);
    }

    public void btnRegster_Click()
    {
        Event.emit(Events.register, null);
    }

    public void btnGame_Click()
    {
        Event.emit(Events.gameOnStart, null);
    }
}
