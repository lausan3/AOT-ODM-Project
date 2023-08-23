using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public float sensX;
    public float sensY;
    public Transform orientation;
    float xRotation;
    float yRotation;
    public Transform playerObj;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        // get mouse input
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensX * 40;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensY * 40;

        yRotation += mouseX;
        xRotation -= mouseY;

        // lock y to -90 and 90 degrees
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        // rotate camera and orientation
        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
        orientation.rotation = Quaternion.Euler(0, yRotation, 0);
        
        // rotate player model
        playerObj.localRotation = Quaternion.Euler(0, yRotation, 0);
    }
}
