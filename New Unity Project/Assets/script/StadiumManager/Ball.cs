using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System.Linq;

public class Ball : MonoBehaviourPunCallbacks
{
    public int force;
    private int eventID;
    private int eventID2;
    private Queue<KeyValuePair<Vector3, Quaternion>> queuePoss = new Queue<KeyValuePair<Vector3, Quaternion>>();
    public bool IsMine;

    private void Awake()
    {
        eventID = Event.register(Events.onGameStart, onGameStart);
        eventID2 = Event.register(Events.onGameRestart, onGameStart);
    }

    private void FixedUpdate()
    {
        if (IsMine)
        {
            photonView.RPC(nameof(BallMove), RpcTarget.Others, transform.position, transform.rotation);
            queuePoss.Clear();
        }            
        else
        {
            if (queuePoss.Count > 0)
            {
                if (queuePoss.Count > 50)
                {
                    KeyValuePair<Vector3, Quaternion> dic = queuePoss.LastOrDefault();
                    transform.position = dic.Key;
                    transform.rotation = dic.Value;
                }
                else
                {
                    KeyValuePair<Vector3, Quaternion> dic = queuePoss.Dequeue();
                    transform.position = dic.Key;
                    transform.rotation = dic.Value;
                }
            }
        }
    }

    [PunRPC]
    private void BallMove(Vector3 pos, Quaternion rot)
    {
        KeyValuePair<Vector3, Quaternion> keyValuePair = new KeyValuePair<Vector3, Quaternion>(pos, rot);
        queuePoss.Enqueue(keyValuePair);
    }

    private void onGameStart(object context)
    {
        if (Global.state == State.gameStart)
        {
            queuePoss.Clear();
            transform.position = new Vector3(0, 6, -50);
            Collider sphereCollider = GetComponent<Collider>();
            sphereCollider.enabled = true;
            GetComponent<Renderer>().enabled = true;
            Rigidbody rb = GetComponent<Rigidbody>();
            rb.useGravity = true;
        }
    }


    private void OnCollisionEnter(Collision collision)
    {        
        if (Global.state != State.gameStart) return;
        if (collision.gameObject.tag == "Player" && collision.gameObject.GetComponent<PhotonView>().IsMine)
        {            
            queuePoss.Clear();
            Rigidbody rb = GetComponent<Rigidbody>();
            Vector3 force = transform.position - collision.transform.position;
            BaseAttribute attribute = collision.gameObject.GetComponent<BaseAttribute>();
            rb.AddForce(force*attribute.shotForce*10);
            IsMine = true;
            photonView.RPC(nameof(changeOwner), RpcTarget.Others);
        }
    }

    [PunRPC]
    private void changeOwner()
    {
        IsMine = false;
    }



    private void OnTriggerEnter(Collider other)
    {
        if (Global.state != State.gameStart) return;
        if (other.gameObject.tag == "Goal_blue")
        {
            goalBuleGoal();
        }

        if (other.gameObject.tag == "Goal_red")
        {
            goalRedGoal();
        }

        if(other.gameObject.tag == "Goal_red" || other.gameObject.tag == "Goal_blue")
        {
            GetComponent<Renderer>().enabled = false;
            GetComponent<Rigidbody>().velocity = Vector3.zero;
            GetComponent<Rigidbody>().useGravity = false;
            GetComponent<Collider>().enabled = false;
            photonView.TransferOwnership(PhotonNetwork.MasterClient);            
            if (PhotonNetwork.IsMasterClient)
            {
                StartCoroutine(waitGameRestart());
                IsMine = true;
                photonView.RPC(nameof(changeOwner), RpcTarget.Others);
            }
            Global.state = State.gamePause;
        }
    }

    private void goalBuleGoal()
    {
        if (!PhotonNetwork.IsMasterClient) return;
        int score = (int?)SetGlobal.getValue(Value.redScore) ?? 0;
        score++;
        SetGlobal.setValue(Value.redScore, score);
        if (score == Define.scoreWin)
        {
            Event.emit(Events.enoughScore, null);
        }
    }

    private void goalRedGoal()
    {
        if (!PhotonNetwork.IsMasterClient) return;
        int score = (int?)SetGlobal.getValue(Value.blueScore) ?? 0;
        score++;
        SetGlobal.setValue(Value.blueScore, score);
        if (score == Define.scoreWin)
        {
            Event.emit(Events.enoughScore, null);
        }
    }

    IEnumerator waitGameRestart()
    {
        yield return new WaitForSeconds(4);
        photonView.RPC(nameof(GameRestart), RpcTarget.All);
    }

    [PunRPC]
    private void GameRestart()
    {
        if (Global.state == State.gameEnd) return;
        Global.state = State.gameStart;
        Event.emit(Events.onGameRestart, null);
    }

    private void OnDisable()
    {
        Event.unRegister(Events.onGameStart, eventID);
        Event.unRegister(Events.onGameRestart, eventID2);
    }
}
