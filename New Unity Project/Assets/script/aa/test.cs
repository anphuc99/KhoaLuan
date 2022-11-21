using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    private Rigidbody rb;
    public GameObject ball2;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        ball2.transform.position = transform.position + new Vector3(0, 0, 250);
    }

    private void OnCollisionEnter(Collision collision)
    {        
        if (collision.gameObject.tag == "Player")
        {
            Rigidbody rb = GetComponent<Rigidbody>();
            Vector3 force = transform.position - collision.transform.position;
            BaseAttribute attribute = collision.gameObject.GetComponent<BaseAttribute>();
            rb.velocity = force*10;
        }
    }
}
