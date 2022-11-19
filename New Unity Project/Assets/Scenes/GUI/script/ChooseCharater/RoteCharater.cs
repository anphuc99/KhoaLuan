using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoteCharater : MonoBehaviour
{
    public float RotationSpeed = 1000;
    private bool IsClick = false;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            Debug.Log("cccccccc");
            if (Physics.Raycast(ray, out hit, 100))
            {
                IsClick = true;
            }
        }

        if (Input.GetMouseButton(0) && IsClick)
        {
            transform.Rotate(0, -(Input.GetAxis("Mouse X") * RotationSpeed * Time.deltaTime), 0, Space.World);
        }

        if (Input.GetMouseButtonUp(0))
        {
            IsClick = false;
        }
    }
}
