using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Ball : MonoBehaviour
{
    public int force;
    private PhotonView photonView;
    // Start is called before the first frame update
    void Start()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.useGravity = true;
        photonView = GetComponent<PhotonView>();
    }

    // Update is called once per frame
    void Update()
    {
        
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
    }

    private void goalBuleGoal()
    {
        Debug.Log("goalBuleGoal");
    }

    private void goalRedGoal()
    {
        Debug.Log("goalRedGoal");
    }
}
