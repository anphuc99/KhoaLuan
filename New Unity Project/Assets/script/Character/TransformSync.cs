using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System.Linq;

public class TransformSync : MonoBehaviourPunCallbacks
{
    private Queue<KeyValuePair<Vector3, Quaternion>> syncTransform = new Queue<KeyValuePair<Vector3, Quaternion>>();
    private void FixedUpdate()
    {
        if (photonView.IsMine)
        {
            Rigidbody rb = GetComponent<Rigidbody>();
            if (rb.velocity != Vector3.zero)
            {
                photonView.RPC(nameof(Move),RpcTarget.Others, transform.position, transform.rotation);            
            }
        }
        else
        {
            if (syncTransform.Count > 0)
            {
                if (syncTransform.Count > 50)
                {
                    KeyValuePair<Vector3, Quaternion> dic = syncTransform.LastOrDefault();
                    transform.position = dic.Key;
                    transform.rotation = dic.Value;
                }
                else
                {
                    KeyValuePair<Vector3, Quaternion> dic = syncTransform.Dequeue();
                    transform.position = dic.Key;
                    transform.rotation = dic.Value;
                }
            }
        }
    }

    [PunRPC]
    private void Move(Vector3 pos, Quaternion rot)
    {
        KeyValuePair<Vector3, Quaternion> keyValuePair = new KeyValuePair<Vector3, Quaternion>(pos, rot);
        syncTransform.Enqueue(keyValuePair);
    }
}
