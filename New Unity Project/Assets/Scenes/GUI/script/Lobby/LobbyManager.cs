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

    public void btnLeaderboard_Click()
    {
        Event.emit(Events.showLeaderboard, null);
    }

    public void btnSetting_Click()
    {
        Event.emit(Events.showSetting, null);
    }

    public void btnProfile_Click()
    {
        Event.emit(Events.showProfile, null);
    }

    public void btnHistory_Click()
    {
        Event.emit(Events.showHistory, null);
    }
}
