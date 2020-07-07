using UnityEngine;

public class PlayerCharacter : MonoBehaviour
{
	[Header("View Settings")]
	[SerializeField] private Transform _playerArms;
	[Range(0, 2)]
	[SerializeField] private float _mouseSensitivity;

	[Space][Space]
	[Header("Movement Settings")]
	[SerializeField] private float _jumpSpeed = 15;
	[SerializeField] private float _gravity = 0.1f;
	[SerializeField] private float _walkSpeed = 4;
	[SerializeField] private float _sprintSpeed = 2;
	
	
	[Space][Space]
	[Header("Animation References")]
	[SerializeField] private Animator _animator;
	[SerializeField] private Animator[] _camAnimator;


	[SerializeField] private Weapon _automaticRifle;



	private float _xAxisClamp = 0;

	private Vector3 _moveDirection;
	private CharacterController _controller;
	private float _moveSpeed;
	


	
	
	private void Awake()
	{
		Cursor.lockState = CursorLockMode.Locked;
		_controller = GetComponent<CharacterController>();
	}

	private void Update()
	{
		Move();
		RotateCamera();
		Aim();
		Shoot();
	}

	private void Move()
	{
		float moveX = Input.GetAxis("Horizontal");
		float moveZ = Input.GetAxis("Vertical");

		if(moveX != 0 || moveZ != 0)
			_animator.SetBool("Walk", true);
		else
			_animator.SetBool("Walk", false);


		if (_controller.isGrounded)
		{
			_moveDirection = new Vector3(moveX, 0, moveZ);
			_moveDirection = transform.TransformDirection(_moveDirection);


			if (Input.GetKey(KeyCode.LeftShift) && moveZ == 1)
			{
				_moveSpeed = _sprintSpeed;
				_animator.SetBool("Run", true);
			}
			else
			{
				_moveSpeed = _walkSpeed;
				_animator.SetBool("Run", false);
			}


				_moveDirection *= _moveSpeed;

			if (Input.GetButtonDown("Jump"))
			{
				_moveDirection.y += _jumpSpeed;
			}
		}

		_moveDirection.y -= _gravity;
		_controller.Move(_moveDirection * Time.deltaTime);
	}

	private void RotateCamera()
	{
		float mouseX = Input.GetAxis("Mouse X");
		float mouseY = Input.GetAxis("Mouse Y");

		float rotAmountX = mouseX * _mouseSensitivity;
		float rotAmountY = mouseY * _mouseSensitivity;

		_xAxisClamp -= rotAmountY;

		Vector3 rotPlayerArms = _playerArms.rotation.eulerAngles;
		Vector3 rotPlayer = transform.rotation.eulerAngles;

		rotPlayerArms.x -= rotAmountY;
		rotPlayerArms.z = 0;
		rotPlayer.y += rotAmountX;

		if (_xAxisClamp > 90)
		{
			_xAxisClamp = 90;
			rotPlayerArms.x = 90;
		}
		else if (_xAxisClamp < -90)
		{
			_xAxisClamp = -90;
			rotPlayerArms.x = 270;
		}

		_playerArms.rotation = Quaternion.Euler(rotPlayerArms);
		transform.rotation = Quaternion.Euler(rotPlayer);
	}

	private void Aim()
	{
		if(Input.GetMouseButton(1))
		{
			_animator.SetBool("Aim", true);
			
			for(int i = 0; i < _camAnimator.Length; i++)
				_camAnimator[i].SetBool("Aim", true);
		}
		else
		{
			_animator.SetBool("Aim", false);
			
			for(int i = 0; i < _camAnimator.Length; i++)
				_camAnimator[i].SetBool("Aim", false);
		}
	}

	private void Shoot()
	{
		if(Input.GetMouseButton(0))
		{
			_animator.SetTrigger("Shoot");
			_automaticRifle.BeginFire();
		}
		else
		{
			_automaticRifle.StopFire();
		}
	}
}
