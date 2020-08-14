using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerInteractController : MonoBehaviour
{
    public GameObject Camera;
    [Space]
    [Header("Money information")]
    [SerializeField]
    private float _playerWallet;
    [SerializeField]
    private float _safedMoney;
    [SerializeField]
    private float _onHitMoneyLossPercent;
    public float WalletAmount { get => _playerWallet; set => _playerWallet = value; }

    [SerializeField]
    private GameObject _Van;
    [SerializeField]
    private HealthComponent _healthRef;

    [Space]
    [Tooltip("The maximum distance in which the player can interact with objects. ")]
    [SerializeField]
    private float _maxInteractDistance = 2;
    [Tooltip("The maximum distance in which objects get highlighted for the player.")]
    [SerializeField]
    private float _maxOutlineDistance = 5;

    private OutlineController _prevController;
    private OutlineController _currentController;
    private RaycastHit? _raycastHit;

    private void Start()
    {
        _healthRef.HealthChanged += OnHealthChanged;
    }

    private void OnHealthChanged(float arg1, bool arg2)
    {
        _playerWallet *= 1- _onHitMoneyLossPercent;
    }

    private void Update()
    {
        Raycast();
        OutlineInteractableObject();
        InteractWithObject();
    }

    private void Raycast()
    {
        RaycastHit[] raycastAll = Physics.RaycastAll(Camera.transform.position, Camera.transform.forward, _maxInteractDistance);
        raycastAll = raycastAll.OrderBy(x => x.distance).ToArray();

        Debug.DrawRay(Camera.transform.position, Camera.transform.forward, Color.blue, 3);

        if (raycastAll.Length > 0)
        {
            _raycastHit = raycastAll[0];
        }
        else
        {
            if (_raycastHit.HasValue)
            {
                HideOutline();
                _raycastHit = null;
            }
        }
    }

    private void OutlineInteractableObject()
    {
        if (_raycastHit.HasValue)
        {
            RaycastHit raycastHit = _raycastHit.Value;

            if (raycastHit.collider.CompareTag("InteractableObject") && raycastHit.collider != null)
            {
                _currentController = raycastHit.collider.GetComponent<OutlineController>();

                if (_prevController != _currentController)
                {
                    HideOutline();
                    ShowOutline();
                }

                _prevController = _currentController;
            }
            else
            {
                HideOutline();
            }
        }
    }

    private void InteractWithObject()
    {
        if (_raycastHit.HasValue)
        {
            RaycastHit raycastHit = _raycastHit.Value;
            if (Input.GetKeyDown(KeyCode.Q))
            {
                if ((raycastHit.point - Camera.transform.position).magnitude < _maxInteractDistance)
                {
                    var interactable = raycastHit.transform.GetComponent<Interactable>();

                    if (interactable != null)
                    {
                        interactable.Interact(this);
                    }
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == _Van)
        {
            _safedMoney = _playerWallet;
            _playerWallet = 0;
        }
    }

    private void ShowOutline()
    {
        if (_currentController != null)
        {
            _currentController.ShowOutline();
        }
    }

    private void HideOutline()
    {
        if (_prevController != null)
        {
            _prevController.HideOutline();
            _prevController = null;
        }
    }
}
