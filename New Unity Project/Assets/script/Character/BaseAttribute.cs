using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseAttribute : MonoBehaviour
{
    [Tooltip("Move speed in meters/second")]
    public float moveSpeed;
    [Tooltip("Upward speed to apply when jumping in meters/second")]
    public float jumpSpeed;
    [Tooltip("kick power")]
    public float shotForce;

    private void Start()
    {
        moveSpeed = 200 + Global.playerClient.speed * 10;
        jumpSpeed = 40 + Global.playerClient.jump;
        shotForce = 15 + Global.playerClient.shotForce/2;
    }
}
