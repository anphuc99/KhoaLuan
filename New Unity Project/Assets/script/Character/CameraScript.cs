using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraScript : MonoBehaviour
{
    public float mouseSensitivity = 200.0f;
    public float clampAngle = 80.0f;

    private float rotY = 0.0f; // rotation around the up/y axis
    private float rotX = 0.0f; // rotation around the right/x axis
    public GameObject player;
    private bool isRotCamera = false;
    public ButtonPress canvas;

    void Start()
    {
        Vector3 rot = transform.localRotation.eulerAngles;
        rotY = rot.y;
        rotX = rot.x;
        canvas.onButtonPressed = OnCanvasPressed;
        
    }

    void FixedUpdate()
    {
        if (player == null) return;
        if ((Application.platform == RuntimePlatform.WindowsPlayer || Application.platform == RuntimePlatform.WindowsEditor) && (isRotCamera || Input.GetMouseButton(1)))
        {
            float mouseX = Input.GetAxis("Mouse X");
            float mouseY = Input.GetAxis("Mouse Y");
            rotCamera(new Vector2 (mouseX, mouseY));
        }
        if (Input.GetKeyDown(KeyCode.BackQuote))
        {
            isRotCamera = !isRotCamera;
            Cursor.visible = !isRotCamera;
            Screen.lockCursor = isRotCamera;
        }
    }

    private void OnCanvasPressed(Vector2 pos)
    {
        if((Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer))
        {
            rotCamera(pos);
        }
    }

    private void rotCamera(Vector2 pos)
    {
        float mouseX = pos.x;
        float mouseY = -pos.y;

        rotY = player.transform.localRotation.eulerAngles.y;

        rotY += mouseX * mouseSensitivity * Time.deltaTime;
        rotX += mouseY * mouseSensitivity * Time.deltaTime;

        rotX = Mathf.Clamp(rotX, -clampAngle, clampAngle);

        Quaternion localRotation = Quaternion.Euler(rotX, rotY, 0.0f);
        transform.rotation = localRotation;
        Quaternion r_player = Quaternion.Euler(0, rotY, 0.0f);
        player.transform.rotation = r_player;
    }
    private void OnDisable()
    {
        Cursor.visible = true;
        Screen.lockCursor = false;
    }
}
