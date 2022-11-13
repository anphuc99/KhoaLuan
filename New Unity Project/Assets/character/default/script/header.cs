using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class header : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ball")
        {
            foreach (ContactPoint contact in collision.contacts)
            {
                if(contact.thisCollider.GetType() == typeof(SphereCollider))
                {
                    Animator animator = GetComponent<Animator>();
                    animator.SetTrigger("header");
                }
                else
                {
                    Animator animator = GetComponent<Animator>();
                    animator.SetTrigger("kichTheBall");
                }
            }
        }
    }
}
