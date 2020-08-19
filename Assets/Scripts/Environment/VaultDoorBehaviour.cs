using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VaultDoorBehaviour : DoorBehaviour
{
	[SerializeField]
	private GameObject _vaultDrill;
	[SerializeField]
	private float _timeLeft;
	[SerializeField]
	private GameObject _drillUIPrefab;

	private DrillUI _drillUI;
	private bool _doOnce = true;

	
	public float CurrentTimer { get => _timeLeft; }


	private void Awake()
	{
		_drillUI = _drillUIPrefab.GetComponent<DrillUI>();

		_drillUIPrefab.SetActive(false);
		_vaultDrill.SetActive(false);

		if (_targetAngle == _openAngle)
		{
			_isOpen = true;
		}

		else
		{
			_isOpen = false;
		}
		_currentAngle = transform.localEulerAngles.y;
	}

	// Update is called once per frame
	private void Update()
	{
		if (_targetAngle != _currentAngle)
		{
			_vaultDrill.SetActive(true);

			if (_timeLeft <= 0)
			{
				_drillUIPrefab.SetActive(false);
				float rotationAmount = Mathf.Min(_rotationSpeed * Time.deltaTime, Mathf.Abs(_targetAngle - _currentAngle));
				_currentAngle += Mathf.Sign(_targetAngle - _currentAngle) * rotationAmount;

				transform.localRotation = Quaternion.Euler(0, _currentAngle, 0);
			}
			else
			{
				if (_doOnce)
				{
					_drillUIPrefab.SetActive(true);
					_doOnce = false;
				}
			}

			_timeLeft -= Time.deltaTime;
		}
		if (_isOpen) _closeAngle = _currentAngle;
	}
}
