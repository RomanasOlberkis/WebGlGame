using System;
using System.Collections.ObjectModel;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class Player : MonoBehaviour
{


	

	[System.Serializable]
	public class MoveSettings
	{
		public float forwardVel = 10;
		public float rotateVel = 100;
		public float jumpVel = 5000;
		public float distToGround = 20;
		public LayerMask ground;
		
	}
	
			[System.Serializable]
     		public class PhysSettings
     		{
     			public float downAccel = 0.75f;
	     
     		}
	        [System.Serializable]
	        public class InputSettings
	        {
		        public float inputDelay = 0.1f;
		        [FormerlySerializedAs("FORWARD_AXIS")] public string forwardAxis = "Vertical";
		        [FormerlySerializedAs("TURN_AXIS")] public string turnAxis = "Horizontal";
		        [FormerlySerializedAs("JUMP_AXIS")] public string jumpAxis = "Jump";

	        }

        public MoveSettings moveSetting = new MoveSettings();
        public PhysSettings physSetting = new PhysSettings();
        public InputSettings inputSetting = new InputSettings();

        private Vector3 _velocity = Vector3.zero;
        private Quaternion _targetRotation;
        private Rigidbody _rBody;
        private float _forwardInput, _turnInput, _jumpInput;

        public Quaternion TargetRotation => _targetRotation;

      
     
        public bool IsGrounded() // Ray to check if the char is touching the ground for a jump
        {
	        if (Physics.Raycast(transform.position, Vector3.down, moveSetting.distToGround, moveSetting.ground))
	        {
		       return true;
	        }
	        else
	        {
		        return false;
	        }
        } 
        
        private Animator _anim;
		private CharacterController _controller;
		private static readonly int AnimationPar = Animator.StringToHash("AnimationPar");

		private int count;
		void Start ()
		{
			count = 0;
			_rBody = GetComponent<Rigidbody>();

			_targetRotation = transform.rotation;
			if (GetComponent<Rigidbody>())
			{
				_rBody = GetComponent<Rigidbody>();
			}
			else
			{
				Debug.LogError("Char has no rigiB");
			}

			_forwardInput = _turnInput = _jumpInput = 0;
			
			_controller = GetComponent <CharacterController>();
			_anim = gameObject.GetComponentInChildren<Animator>();
		}

		void GetInput()
		{
			_forwardInput = Input.GetAxis(inputSetting.forwardAxis);
			_turnInput = Input.GetAxis(inputSetting.turnAxis);
			_jumpInput = Input.GetAxis(inputSetting.jumpAxis);
		}

		
		void Update()
		{
			Jump();
			/*
			if(IsGrounded()==true && _rBody.velocity.magnitude>2f )
			{
				FindObjectOfType<AudioManager>().Play("Step");
				
			}*/
			
		}

		[SerializeField]  ParticleSystem collectParticle = null;
		public void Collect()
		{
			collectParticle.Play();
		}
		
		void FixedUpdate ()
		{
			GetInput();
			Turn();
			Run();

			_rBody.velocity = transform.TransformDirection(_velocity);
			
			//animation activation
			if (Mathf.Abs(_forwardInput) > inputSetting.inputDelay) {
				_anim.SetInteger (AnimationPar, 1);
			}  else {
				_anim.SetInteger (AnimationPar, 0);
			}
		}

		void Run() //char running
		{
			if (Mathf.Abs(_forwardInput) > inputSetting.inputDelay)
			{
				_velocity.z = moveSetting.forwardVel * _forwardInput;
			}
			else
			{
				_velocity.z = 0;
			}
		}
		
		void Turn() //char turning
		{
			if (Mathf.Abs(_turnInput)>inputSetting.inputDelay)
				
			{
				_targetRotation *=Quaternion.AngleAxis(moveSetting.rotateVel*_turnInput*Time.deltaTime,Vector3.up);
			}

			transform.rotation = _targetRotation;
		}

	
		void Jump()
		{
			if (_jumpInput == 1 && IsGrounded()==true)
			{
				Collect();
				FindObjectOfType<AudioManager>().Play("JumpSound");
				_velocity.y = moveSetting.jumpVel;
				Debug.LogError("Char jumped");
			}
			
			
			else if (_jumpInput == 0 && IsGrounded())
			{
				_velocity.y = 0;
			}
			else if(IsGrounded()==false)
			{
			
				_velocity.y -= physSetting.downAccel;
				Debug.LogError("char going down");
			}
		}
		private void OnTriggerEnter(Collider other)
		{
			if (other.gameObject.CompareTag("CardbordBox"))
			{
				other.gameObject.SetActive(false);
				count = count + 1;
			}
		}
		
		
		
}
