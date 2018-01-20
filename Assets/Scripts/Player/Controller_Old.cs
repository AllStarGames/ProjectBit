using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//public class Controller : MonoBehaviour
//{
//	[System.Serializable]
//	public struct Flags
//	{
//        public bool toggleAim;
//		public bool toggleCrouch;
//
//		bool crouched;
//        bool disabled;
//		bool grounded;
//		bool sprinting;
//		bool stunned;
//
//		public bool Crouched()
//		{
//			return crouched;
//		}
//        public bool Disabled()
//        {
//            return disabled;
//        }
//		public bool Grounded()
//		{
//			return grounded;
//		}
//		public bool Sprinting()
//		{
//			return sprinting;
//		}
//		public bool Stunned()
//		{
//			return stunned;
//		}
//
//		public void Initialize()
//		{
//			crouched = false;
//            disabled = false;
//			grounded = true;
//			sprinting = false;
//			stunned = false;
//		}
//		public void IsCrouched(bool value)
//		{
//			crouched = value;
//		}
//        public void IsDisabled(bool value)
//        {
//            disabled = value;
//        }
//		public void IsGrounded(bool value)
//		{
//			grounded = value;
//		}
//		public void IsSprinting(bool value)
//		{
//			sprinting = value;
//		}
//		public void IsStunned(bool value)
//		{
//			stunned = value;
//		}
//	}
//	[System.Serializable]
//	public struct JumpVariables
//	{
//		public float speed;
//
//		public float Speed()
//		{
//			return speed;
//		}
//		public void SetSpeed(float value)
//		{
//			speed = value;
//		}
//	}
//	[System.Serializable]
//	public struct LookVariables
//	{
//		[Tooltip("The look speed for this player")]
//		public float speed;
//		[Tooltip("The max and min angles this player can look along the vertical axis")]
//		public Vector2 verticalContraints;
//
//		private float yAxis;
//
//		public float Speed()
//		{
//			return speed;
//		}
//		public float YAxis()
//		{
//			return yAxis;
//		}
//		public Vector2 VerticalContraints()
//		{
//			return verticalContraints;
//		}
//		public void SetSpeed(float value)
//		{
//			speed = value;
//		}
//		public void SetVerticalContraints(float min, float max)
//		{
//			verticalContraints = new Vector2(min, max);
//		}
//		public void SetVerticalContraints(Vector2 constraints)
//		{
//			verticalContraints = constraints;
//		}
//		public void SetYAxis(float value)
//		{
//			yAxis = value;
//		}
//	}
//	[System.Serializable]
//	public struct MovementVariables
//	{
//		[Tooltip("The movement speed when this player is crouching")]
//		public float crouchSpeed;
//		[Tooltip("The default movement speed for this player")]
//		public float speed;
//		[Tooltip("The movement speed when this player is sprinting")]
//		public float sprintSpeed;
//		[Tooltip("The movement speed when this player is stunned")]
//		public float stunSpeed;
//
//		private float totalSpeed;
//
//		public float CrouchSpeed()
//		{
//			return crouchSpeed;
//		}
//		public float Speed()
//		{
//			return speed;
//		}
//		public float SprintSpeed()
//		{
//			return sprintSpeed;
//		}
//		public float StunSpeed()
//		{
//			return stunSpeed;
//		}
//		public float TotalSpeed()
//		{
//			return totalSpeed;
//		}
//		public void SetCrouchSpreed(float value)
//		{
//			crouchSpeed = value;
//		}
//		public void SetSpeed(float value)
//		{
//			speed = value;
//		}
//		public void SetSprintSpeed(float value)
//		{
//			sprintSpeed = value;
//		}
//		public void SetStunSpeed(float value)
//		{
//			stunSpeed = value;
//		}
//		public void SetTotalSpeed(float value)
//		{
//			totalSpeed = value;
//		}
//		
//	}
//
//	public Flags flags;
//	public JumpVariables jump;
//	public LookVariables rotation;
//	public MovementVariables movement;
//	public Transform hand;
//	private Camera camera;
//	private Rigidbody body;
//
//	public Rigidbody Body()
//	{
//		return body;
//	}
//	public void Disable()
//	{
//		this.enabled = false;
//	}
//	public void Enable()
//	{
//		this.enabled = true;
//	}
//	public void SetBody(Rigidbody rigidbody)
//	{
//		body = rigidbody;
//	}
//
//	// Method for handling player jumping
//	void Crouch()
//	{
//		if(flags.toggleCrouch)
//		{
//			if(Input.GetKeyDown(KeyCode.LeftControl) || Input.GetKeyDown(KeyCode.RightControl))
//			{
//				flags.IsSprinting(false);
//				flags.IsCrouched(!flags.Crouched());
//			}
//		}
//		else
//		{
//			if(Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl))
//			{
//				flags.IsSprinting(false);
//				flags.IsCrouched(true);
//			}
//			else
//			{
//				flags.IsCrouched(false);
//			}
//		}
//	}
//	// Method for handling player looking
//	void Look()
//	{
//		transform.Rotate(0.0f, Input.GetAxis("Mouse X") * rotation.Speed(), 0.0f);
//		hand.forward = camera.transform.forward;
//
//		rotation.SetYAxis(rotation.YAxis() + Input.GetAxis("Mouse Y") * rotation.Speed());
//		rotation.SetYAxis(Mathf.Clamp(rotation.YAxis(), rotation.VerticalContraints().x, rotation.VerticalContraints().y));
//
//		camera.transform.localEulerAngles = new Vector3(-rotation.YAxis(), camera.transform.localEulerAngles.y, 0.0f);
//    }
//	// Method for handling player movement
//	void Move()
//	{
//        if(!flags.Disabled())
//        {
//		    if(flags.Grounded())
//		    {
//		    	Crouch();
//		    	Sprint();
//
//		    	Vector3 velocity = new Vector3(Input.GetAxisRaw("Horizontal"), 0.0f, Input.GetAxisRaw("Vertical"));
//
//		    	if(flags.Crouched())
//		    	{
//		    		movement.SetTotalSpeed(movement.CrouchSpeed());
//		    	}
//		    	else if(flags.Sprinting())
//		    	{
//		    		movement.SetTotalSpeed(movement.SprintSpeed());
//		    	}
//		    	else
//		    	{
//		    		movement.SetTotalSpeed(movement.Speed());
//		    	}
//
//		    	velocity = transform.TransformDirection(velocity) * movement.TotalSpeed();
//		    	if(Input.GetKey(KeyCode.Space))
//		    	{
//		    		flags.IsCrouched(false);
//		    		flags.IsGrounded(false);
//		    		flags.IsSprinting(false);	
//		    		velocity.y = jump.Speed();
//		    	}
//		    	velocity += Physics.gravity;
//
//		    	body.velocity = velocity;
//		    }
//        }
//	}
//	void Sprint()
//	{
//		if(Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
//		{
//			flags.IsCrouched(false);
//			flags.IsSprinting(true);
//		}
//		else
//		{
//			flags.IsSprinting(false);
//		}
//	}
//	// Method for checking if the player is grounded or not
//	void UpdateGrounded()
//	{
//		if(Physics.Raycast(transform.position, -Vector3.up, 1.5f))
//		{
//			flags.IsGrounded(true);
//		}
//		else
//		{
//			flags.IsGrounded(false);
//		}
//	}
//
//	// Use this for initialization
//	void Start ()
//	{
//		flags.Initialize();
//
//		camera = GetComponentInChildren<Camera>();
//		body = GetComponent<Rigidbody>();
//	}
//	
//	// Update is called once per frame
//	void Update ()
//	{
//		UpdateGrounded();
//
//		/*MenuController menu = gameObject.GetComponentInChildren<MenuController>();
//		if(!menu.GetCanvas().enabled)
//		{
//			if(flags.Crouched())
//			{
//				Vector3 scale = gameObject.transform.localScale;
//				scale.y = 0.5f;
//				gameObject.transform.localScale = scale;
//			}
//			else
//			{
//				Vector3 scale = gameObject.transform.localScale;
//				scale.y = 1.0f;
//				gameObject.transform.localScale = scale;
//			}
//
//		}*/
//			Look();
//			Move();
//	}
//}