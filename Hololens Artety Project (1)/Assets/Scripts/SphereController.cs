using UnityEngine;
using System.Collections;
using HoloLensXboxController;

public class SphereController : MonoBehaviour
{
    public float movementSpeed = 1.0f;
    public float fowardSpeed = 1.0f;
    public float maxSpeed = 10.0f;
    float currentSpeed;
    public Camera capsuleCamera;
   
    public int invert = 1; // -1 for invert 
    private ControllerInput controllerInput;

    void Start()
    {

        controllerInput = new ControllerInput(0, 0.19f);


    }
    void Update()
    {
        controllerInput.Update();

    }

    void FixedUpdate()
    {
        //currentSpeed = capsule.velocity.magnitude;
        //transform.forward = capsuleCamera.transform.forward;
        //float h = Input.GetAxis("Mouse Y") * movementSpeed; //for mouse input within unity uncomment  these two lines, 
        //float v = Input.GetAxis("Mouse X") * movementSpeed;
        updateRotation();

        if (Input.GetKey("q"))// change the input as the test likes
        {
            
            transform.position += transform.forward * Time.deltaTime * (fowardSpeed + 1);
        }
        if (controllerInput.GetAxisRightTrigger()== 1.0f||Input.GetAxis("Right Trigger")== 1.0f)//d-pad
        {
            transform.position += transform.forward * Time.deltaTime * (fowardSpeed + 1);
        }
        // transform.position = transform.position + Camera.main.transform.forward * distance * Time.deltaTime;

    }
    void updateRotation()
    {
        //float v= Input.GetAxis("Right Stick X");
        //float h =Input.GetAxis("Right Stick Y");
        float h = controllerInput.GetAxisRightThumbstickY() * movementSpeed;// For bluetooth Xbox controller input Input.GetAxis("Right Stick X")
        float v = controllerInput.GetAxisRightThumbstickX() * movementSpeed;
        transform.Rotate(v, h, 0);
    }
}