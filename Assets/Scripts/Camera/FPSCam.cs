using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSCam : MonoBehaviour
{
    public float sensX;
    public float sensY;

    public Transform orientation;

    float rotationX;
    float rotationY;


    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; 
        Cursor.visible = false;
    }

    void Update()
    {
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensX;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensY;

        rotationY += mouseX;
        rotationX -= mouseY;

        rotationX = Mathf.Clamp(rotationX, -90f, 90f);
        transform.rotation = Quaternion.Euler(rotationX, rotationY, 0);
        orientation.rotation = Quaternion.Euler(0, rotationY, 0);
    }
}
