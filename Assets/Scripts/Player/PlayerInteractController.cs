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

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
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
                }
            }
        }
    }
}
