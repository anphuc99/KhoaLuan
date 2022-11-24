using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public float mouseSensitivity = 200.0f;
    public float clampAngle = 80.0f;

    private float rotY = 0.0f; // rotation around the up/y axis
    private float rotX = 0.0f; // rotation around the right/x axis
    public GameObject player;
    private bool isRotCamera = false;

    void Start()
    {
        Vector3 rot = transform.localRotation.eulerAngles;
        rotY = rot.y;
        rotX = rot.x;
    }

    void FixedUpdate()
    {
        if (isRotCamera || Input.GetMouseButton(1))
        {
            float mouseX = Input.GetAxis("Mouse X");
            float mouseY = -Input.GetAxis("Mouse Y");

            rotY = player.transform.localRotation.eulerAngles.y;

            rotY += mouseX * mouseSensitivity * Time.deltaTime;
            rotX += mouseY * mouseSensitivity * Time.deltaTime;

            rotX = Mathf.Clamp(rotX, -clampAngle, clampAngle);

            Quaternion localRotation = Quaternion.Euler(rotX, rotY, 0.0f);
            transform.rotation = localRotation;
            Quaternion r_player = Quaternion.Euler(0, rotY, 0.0f);
            player.transform.rotation = r_player;
        }
        if (Input.GetKeyDown(KeyCode.BackQuote))
        {
            isRotCamera = !isRotCamera;
            Cursor.visible = !isRotCamera;
            Screen.lockCursor = isRotCamera;
        }
    }

    private void OnDisable()
    {
        Cursor.visible = true;
        Screen.lockCursor = false;
    }
}
