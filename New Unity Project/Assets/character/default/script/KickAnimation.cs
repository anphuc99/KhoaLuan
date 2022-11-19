using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KickAnimation : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ball")
        {
            Animator animator = GetComponent<Animator>();
            animator.SetTrigger("kichTheBall");
        }
    }
}
