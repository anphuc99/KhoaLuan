using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Ball : MonoBehaviourPunCallbacks
{
    public int force;
    private int eventID;
    private int eventID2;
    private int eventID3;

    private void Awake()
    {
        eventID = Event.register(Events.onGameStart, onGameStart);
        eventID2 = Event.register(Events.onGameRestart, onGameStart);
        eventID3 = Event.register(Events.resultsTeamWin, resultGame);
    }

    private void onGameStart(object context)
    {
        transform.position = new Vector3(0,6,-50);
        SphereCollider sphereCollider = GetComponent<SphereCollider>();
        sphereCollider.isTrigger = false;
        GetComponent<Renderer>().enabled = true;
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.useGravity = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        //if (!PhotonNetwork.IsMasterClient) return;
        if (collision.gameObject.tag == "Player")
        {
            photonView.TransferOwnership(collision.gameObject.GetComponent<PhotonView>().Owner);
            Rigidbody rb = GetComponent<Rigidbody>();
            Vector3 force = transform.position - collision.transform.position;
            rb.AddForce(force*this.force);
        }
    }



    private void OnTriggerEnter(Collider other)
    {
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
            GetComponent<Collider>().isTrigger = true;
            if (PhotonNetwork.IsMasterClient)
            {
                StartCoroutine(waitGameRestart());                
            }
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
        Event.emit(Events.onGameRestart, null);
    }

    private void OnDisable()
    {
        Event.unRegister(Events.onGameStart, eventID);
        Event.unRegister(Events.onGameRestart, eventID2);
    }    

    private void resultGame(object context)
    {
        Destroy(this);
    }
}
