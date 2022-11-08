using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitForGame : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(waitForGame());
    }

    IEnumerator waitForGame()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            if (!PhotonNetwork.IsMasterClient)
            {
                yield return new WaitForFixedUpdate();
                continue;
            };
            int time = (int?)SetGlobal.getValue(Value.TimeWait) ?? Define.WaitForGame;
            time = time - 1;
            SetGlobal.setValue(Value.TimeWait, time);
            if (time == 0)
            {
                break;
            }
        }
    }
}
