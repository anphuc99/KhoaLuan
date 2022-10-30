using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyManager : MonoBehaviour
{
    public void bntStart_Click()
    {
        Event.emit(Events.onStart, null);
    }

    public void btnLogout_Click()
    {
        Event.emit(Events.login, null);
    }
}
