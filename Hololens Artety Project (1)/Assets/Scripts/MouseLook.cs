using System;
using UnityEngine;
using HoloLensXboxController;


public class MouseLook : MonoBehaviour
{
    public float mouseSensitivity = 100.0f;
    public float clampAngle = 80.0f;


    private float rotY = 0.0f; // rotation around the up/y axis
    private float rotX = 0.0f; // rotation around the right/x axis
    private ControllerInput controllerInput;

    void Start()
    {
        //gamepad = GetComponent<Gamepad_Client>();
        controllerInput = new ControllerInput(0, 0.19f);

        Vector3 rot = transform.localRotation.eulerAngles;
        rotY = rot.y;
        rotX = rot.x;
    }

    void Update()
    {
        controllerInput.Update();

        float mouseX=controllerInput.GetAxisRightThumbstickX();//
        float mouseY =controllerInput.GetAxisRightThumbstickY();//
        //mouseX= Input.GetAxis("Mouse X");
        //mouseY= -Input.GetAxis("Mouse Y");
        rotY += mouseX * mouseSensitivity * Time.deltaTime;
        rotX += mouseY * mouseSensitivity * Time.deltaTime;

        rotX = Mathf.Clamp(rotX, -clampAngle, clampAngle);

        Quaternion localRotation = Quaternion.Euler(rotX, rotY, 0.0f);
        transform.rotation = localRotation;
    }
}