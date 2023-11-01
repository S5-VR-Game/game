using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class PlayerCam : MonoBehaviour
{
    public float sensX;
    public float sensY;
    
    public Transform orientation;
    
    float xRotation;
    float yRotation;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        // get mouse
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * this.sensX;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * this.sensY;

        yRotation += mouseX;
        xRotation -= mouseY;
        
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
        orientation.rotation = Quaternion.Euler(0, yRotation, 0);
    }
}
