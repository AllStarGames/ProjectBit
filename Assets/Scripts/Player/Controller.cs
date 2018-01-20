using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ConfigurableJoint))]
[RequireComponent(typeof(Rigidbody))]
public class Controller : MonoBehaviour
{
    [Header("Joint Variables")]
    [Tooltip("")]
    public float jointMaxForce;
    [Tooltip("")]
    public float jointSpring;
    [Tooltip("")]
    public JointDriveMode jointMode;

    [Header("Look Variables")]
    [Tooltip("How fast the camera will move")]
    public float lookSensitivity;
    [Tooltip("The minimum and maximum angles the camera will rotate up and down")]
    public Vector2 lookConstraints;

    [Header("Movement Variables")]
    [Tooltip("The speed at which this object will rise (think jumping)")]
    public float liftForce;
    [Tooltip("The speed at which this object will move")]
    public float speed;
    [Tooltip("The speed at which this object will thrust (think dashing)")]
    public float thrusterForce;

    private Camera playerCam;
    private ConfigurableJoint joint;
    private Rigidbody body;
    private Vector2 input;
    private Vector2 mouseInput;
    private Vector3 camRotation;
    private Vector3 force;
    private Vector3 lift;
    private Vector3 rotation;
    private Vector3 xRotation;
    private Vector3 yRotation;
    private Vector3 velocity;
    private Vector3 xMovement;
    private Vector3 zMovement;

    public void Lift(Vector3 force)
    {
        lift = force;
    }
    public void Move(Vector3 vel)
    {
        velocity = vel;
    }
    public void Rotate(Vector3 rot)
    {
        rotation = rot;
    }
    public void RotateCamera(Vector3 camRot)
    {
        camRotation = camRot;
    }
    
    void FixedUpdate()
    {
        Movement();
        Rotation();
    }
    void Movement()
    {
        if(velocity != Vector3.zero)
        {
            body.MovePosition(body.position + velocity * Time.deltaTime);
        }

        if(lift != Vector3.zero)
        {
            body.AddForce(lift * Time.fixedDeltaTime, ForceMode.Acceleration);
        }
    }
    void Rotation()
    {
        body.MoveRotation(body.rotation * Quaternion.Euler(rotation));

        if(playerCam)
        {
            playerCam.transform.localEulerAngles = -camRotation;
        }
    }
    void SetJointSettings(float springPosition)
    {
        joint.yDrive = new JointDrive
        {
            mode = jointMode, positionSpring = springPosition, maximumForce = jointMaxForce
        };

    }
	// Use this for initialization
	void Start ()
    {
        playerCam = GetComponentInChildren<Camera>();
        joint = GetComponent<ConfigurableJoint>();
        body = GetComponent<Rigidbody>();

        input = Vector2.zero;
        mouseInput = Vector2.zero;

        camRotation = Vector3.zero;
        force = Vector3.zero;
        lift = Vector3.zero;
        rotation = Vector3.zero;
        xRotation = Vector3.zero;
        yRotation = Vector3.zero;
        velocity = Vector3.zero;
        xMovement = Vector3.zero;
        zMovement = Vector3.zero;

        SetJointSettings(jointSpring);
	}
	// Update is called once per frame
	void Update ()
    {
        input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        xMovement = transform.right * input.x;
        zMovement = transform.forward * input.y;

        Vector3 movement = (xMovement + zMovement).normalized * speed;
        Move(movement);

        mouseInput = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        yRotation = new Vector3(0.0f, mouseInput.x, 0.0f) * lookSensitivity;
        Rotate(yRotation);

        xRotation += new Vector3(mouseInput.y, 0.0f, 0.0f) * lookSensitivity;
        xRotation.x = Mathf.Clamp(xRotation.x, lookConstraints.x, lookConstraints.y);
        RotateCamera(xRotation);

        if(Input.GetButtonDown("Jump"))
        {
            force = Vector3.up * liftForce;
            SetJointSettings(0.0f);
        }
        else
        {
            SetJointSettings(jointSpring);
        }
        Lift(force);
	}
}