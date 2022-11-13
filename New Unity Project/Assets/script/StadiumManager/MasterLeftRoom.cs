using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.Networking;

public class MasterLeftRoom : MonoBehaviourPunCallbacks
{
    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        if (Global.curMasterClient == otherPlayer.UserId)
        {

        }
    }

    IEnumerator MasterOut(string roomid)
    {
        WWWForm form = new WWWForm();
        form.AddField("_token", Global.account._token);
        form.AddField("roomID", roomid);
        form.AddField("newMaster", PhotonNetwork.MasterClient.UserId);

        using (UnityWebRequest www = UnityWebRequest.Post(URL.game_masterOutGameRoom, form))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
                Event.emit(Events.goBack, null);
            }
        }
    }
}
