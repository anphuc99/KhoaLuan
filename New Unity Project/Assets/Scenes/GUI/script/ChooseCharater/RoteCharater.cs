using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoteCharater : MonoBehaviour
{
    public float RotationSpeed = 5;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
            transform.Rotate(0, -(Input.GetAxis("Mouse X") * RotationSpeed * Time.deltaTime), 0, Space.World);
    }
}
