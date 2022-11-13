using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class character : MonoBehaviour
{
    [Tooltip("Maximum slope the character can jump on")]
    [Range(5f, 60f)]
    public float slopeLimit = 45f;
    [Tooltip("Move speed in meters/second")]
    public float moveSpeed = 5f;
    [Tooltip("Turn speed in degrees/second, left (+) or right (-)")]
    public float turnSpeed = 300;
    [Tooltip("Whether the character can jump")]
    public bool allowJump = false;
    [Tooltip("Upward speed to apply when jumping in meters/second")]
    public float jumpSpeed = 4f;

    public bool IsGrounded { get; private set; }
    public float ForwardInput { get; set; }
    public float TurnInput { get; set; }
    public bool JumpInput { get; set; }

    private Rigidbody rigidbody;
    private CapsuleCollider capsuleCollider;
    private Animator animator;
    private PhotonView photonView;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
        capsuleCollider = GetComponent<CapsuleCollider>();
        animator = GetComponent<Animator>();
        photonView = GetComponent<PhotonView>();

        if (!photonView.IsMine) return;
        Camera camera = Camera.main;
        camera.transform.parent.SetParent(transform);
        camera.transform.parent.transform.localPosition = new Vector3(0, 1, 0);
        camera.transform.localPosition = new Vector3(0, 1, -2);
        camera.transform.localRotation = Quaternion.identity;
        CameraScript cameraScript = camera.transform.parent.gameObject.AddComponent<CameraScript>();
        cameraScript.player = gameObject;
    }

    private void FixedUpdate()
    {
        if (!photonView.IsMine) return;
        TurnInput = Input.GetAxis("Horizontal");
        ForwardInput = Input.GetAxis("Vertical");
        CheckGrounded();
        ProcessActions();
    }

    /// <summary>
    /// Checks whether the character is on the ground and updates <see cref="IsGrounded"/>
    /// </summary>
    private void CheckGrounded()
    {
        IsGrounded = false;
        float capsuleHeight = Mathf.Max(capsuleCollider.radius * 2f, capsuleCollider.height);
        Vector3 capsuleBottom = transform.TransformPoint(capsuleCollider.center - Vector3.up * capsuleHeight / 2f);
        float radius = transform.TransformVector(capsuleCollider.radius, 0f, 0f).magnitude;
        Ray ray = new Ray(capsuleBottom + transform.up * .01f, -transform.up);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, radius * 5f))
        {
            float normalAngle = Vector3.Angle(hit.normal, transform.up);
            if (normalAngle <= slopeLimit)
            {
                float maxDist = radius / Mathf.Cos(Mathf.Deg2Rad * normalAngle) - radius + .02f;
                if (hit.distance <= maxDist)
                    IsGrounded = true;
            }
        }
    }

    /// <summary>
    /// Processes input actions and converts them into movement
    /// </summary>
    private void ProcessActions()
    {
        // Process Movement/Jumping
        if (IsGrounded)
        {
            animator.SetTrigger("jumpDown");
            animator.SetBool("jumpUp", false);
            // Reset the velocity
            rigidbody.velocity = Vector3.zero;
            // Check if trying to jump
            if (JumpInput && allowJump)
            {
                // Apply an upward velocity to jump
                rigidbody.velocity += Vector3.up * jumpSpeed;
            }

            // Apply a forward or backward velocity based on player input
            Vector3 velocity = transform.forward * Mathf.Clamp(ForwardInput, -1f, 1f) + transform.right * Mathf.Clamp(TurnInput, -1f, 1f);

            rigidbody.velocity += velocity * moveSpeed;

            if (velocity != Vector3.zero)
            {
                animator.SetBool("run", true);
            }
            else
            {
                animator.SetBool("run", false);
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                rigidbody.velocity += Vector3.up * jumpSpeed;
                animator.SetBool("jumpUp", true);

            }
        }        
    }
}
