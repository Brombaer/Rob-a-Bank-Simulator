using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractController : MonoBehaviour
{
    public Transform Camera;

    [SerializeField]
    private float _maxInteractDistance = 2;
    [SerializeField]
    private float _maxOutlineDistance = 5;
    [SerializeField]
    private Transform _interactionCenter;

    // public List<string> CollectableItems;

    [SerializeField]
    private float PlayerWallet;
    [SerializeField]
    public float SafedMoney;

    public float WalletAmount { get => PlayerWallet; }


    private OutlineController _prevController;
    private OutlineController _currentController;

    private void Update()
    {
        InteractWithObject();
        OutlineInteractableObject();
    }

    private void InteractWithObject()
    {

        if (Input.GetKeyDown(KeyCode.Q))
        {
            RaycastHit raycastHit;

            if (Physics.Raycast(Camera.position, Camera.forward, out raycastHit))
            {
                if ((raycastHit.point - _interactionCenter.position).magnitude < _maxInteractDistance)
                {
                    var interactable = raycastHit.transform.GetComponent<Interactable>();

                    if (interactable != null)
                    {
                        interactable.Interact();
                    }

                    if (raycastHit.collider.gameObject.CompareTag("CollectableItem") && raycastHit.collider != null)
                    {
                        // string itemType = raycastHit.transform.gameObject.GetComponent<CollectableItem>().ItemType;
                        float itemValue = raycastHit.transform.gameObject.GetComponent<CollectableItem>().ItemValue;

                        PlayerWallet += itemValue;

                        Destroy(raycastHit.transform.gameObject);
                    }
                }
            }
        }
    }

    private void OutlineInteractableObject()
    {
        RaycastHit raycastHit;

        if (Physics.Raycast(Camera.position, Camera.forward, out raycastHit, _maxOutlineDistance))
        {
            if (raycastHit.collider.CompareTag("CollectableItem") && raycastHit.collider != null)
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
        else
        {
            HideOutline();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        SafedMoney = PlayerWallet;
        PlayerWallet = 0;
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
