using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractController : MonoBehaviour
{
    public Transform Camera;

    [SerializeField]
    private float _maxDistance = 2;
    [SerializeField]
    private Transform _interactionCenter;

    // public List<string> CollectableItems;

    [SerializeField]
    private float PlayerWallet;
    [SerializeField]
    public float SafedMoney;

    private void Awake()
    {

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            RaycastHit raycastHit;

            if (Physics.Raycast(Camera.position, Camera.forward, out raycastHit))
            {
                if ((raycastHit.point - _interactionCenter.position).magnitude < _maxDistance)
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

    private void OnTriggerEnter(Collider other)
    {
        SafedMoney = PlayerWallet;
        PlayerWallet = 0;
    }
}
