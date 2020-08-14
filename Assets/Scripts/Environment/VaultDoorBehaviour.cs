using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VaultDoorBehaviour : DoorBehaviour
{
    [SerializeField]
    private GameObject _vaultDrill;
    [SerializeField]
    private float _timeLeft;
    
    private void Awake()
    {
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

            _timeLeft -= Time.deltaTime;
            if (_timeLeft <= 0)
            {
                float rotationAmount = Mathf.Min(_rotationSpeed * Time.deltaTime, Mathf.Abs(_targetAngle - _currentAngle));
                _currentAngle += Mathf.Sign(_targetAngle - _currentAngle) * rotationAmount;

                transform.localRotation = Quaternion.Euler(0, _currentAngle, 0);
            }
        }

        if (_isOpen == true)
        {
            _closeAngle = _currentAngle;
        }
    }

    //public void SwitchDoorState()
    //{
    //    _isOpen = !_isOpen;
    //
    //    _targetAngle = _isOpen ? _openAngle : _closeAngle;
    //}
}
