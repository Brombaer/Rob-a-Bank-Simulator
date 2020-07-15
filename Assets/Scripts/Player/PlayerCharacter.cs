using System;
using UnityEngine;

public class PlayerCharacter : MonoBehaviour
{
	[Header("View Settings")]
	[SerializeField] private Transform _playerArms;
	[SerializeField] private Transform _playerTransform;
	[Range(0, 2)]
	[SerializeField] private float _mouseSensitivity;

	[Space]
	[Space]
	[Header("Movement Settings")]
	[SerializeField] private float _jumpSpeed = 15;
	[SerializeField] private float _gravity = 0.1f;
	[SerializeField] private float _walkSpeed = 4;
	[SerializeField] private float _sprintSpeed = 2;
	[SerializeField] private float _crouchSpeed = 1;



	[Space]
	[Space]
	[Header("Animation References")]
	[SerializeField] private Animator _animator;
	[SerializeField] private Animator[] _camAnimator;


	[SerializeField] private Weapon _automaticRifle;
	[Range(0, 2)]
	[SerializeField] private float _aimTime = 0.085f; 
	[SerializeField] private float _currentAimTime = 3f;



	private float _xAxisClamp = 0;

	private Vector3 _moveDirection;
	private CharacterController _controller;
	private float _moveSpeed;
	private float _lastAimTime;

	private bool _isAiming;
	private bool _isCrouched;
	private bool _isSprinting;



	private void Awake()
	{
		Cursor.lockState = CursorLockMode.Locked;
		_controller = GetComponent<CharacterController>();

		_automaticRifle.Fired += OnFired;
	}

	private void OnFired()
	{
		if (!_isAiming)
			_animator.Play("Fire", 0, 0f);
		else
			_animator.Play("Aim Fire", 0, 0f);
	}

	private void Update()
	{
		Move();
		RotateCamera();
		Aim();
		Shoot();

		if (!_isSprinting && Input.GetKeyDown(KeyCode.R))
			Reload();
	}

	private void Move()
	{
		float moveX = Input.GetAxis("Horizontal");
		float moveZ = Input.GetAxis("Vertical");

		if (moveX != 0 || moveZ != 0)
			_animator.SetBool("Walk", true);
		else
			_animator.SetBool("Walk", false);


		if (_controller.isGrounded)
		{
			_moveDirection = new Vector3(moveX, 0, moveZ);
			_moveDirection = transform.TransformDirection(_moveDirection);

			_isCrouched = false;

			// Sprint & Crouch
			if (!_isAiming && Input.GetKey(KeyCode.LeftShift) && moveZ == 1)
			{
				_isSprinting = true;
				
				_moveSpeed = _sprintSpeed;
				_animator.SetBool("Run", true);
			}
			else if(!_isSprinting && Input.GetKey(KeyCode.LeftControl))
			{
				_controller.height = 1.5f;
				_moveSpeed = _crouchSpeed;
				_isCrouched = true;
			}
			else
			{
				_isSprinting = false;

				_moveSpeed = _walkSpeed;
				_animator.SetBool("Run", false);
			}

			if (!_isCrouched)
			{
				_controller.height = 2.5f;
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
		if ((!_isSprinting && Input.GetMouseButton(1) || Input.GetAxis("Aim") > 0) && _currentAimTime >= _aimTime)
		{
			Debug.Log("ADS");

			_animator.SetBool("Aim", true);
			
			_isAiming = true;

			for (int i = 0; i < _camAnimator.Length; i++)
				_camAnimator[i].SetBool("Aim", true);
		}
		else if(Input.GetMouseButtonUp(1) && _isAiming)
		{
			_currentAimTime = 0;
			_isAiming = false;

			_animator.SetBool("Aim", false);
			_isAiming = false;

			for (int i = 0; i < _camAnimator.Length; i++)
				_camAnimator[i].SetBool("Aim", false);
			
		}
		else
			_currentAimTime += 2.5f * Time.deltaTime;
	}

	private void Shoot()
	{
		if (!_isSprinting && Input.GetMouseButtonDown(0))
		{
			Debug.Log(Input.GetAxis("Shoot"));

			_automaticRifle.BeginFire();
		}
		else if (Input.GetMouseButtonUp(0))
		{
			_automaticRifle.StopFire();
		}
	}

	private void Reload()
	{
		if(!_automaticRifle.isReloading())
			_automaticRifle.Reload();

		if (_automaticRifle.CurrentAmmo > 0)
			_animator.Play("Reload Ammo Left");
		else
			_animator.Play("Reload Out Of Ammo");
	}
}
