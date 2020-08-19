using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableItem : Interactable
{
    public string ItemType;
    public float ItemValue;
    public float ItemWeight;

    public override void Interact(PlayerInteractController interactor)
    {
        if (interactor.PlayerCurrentLoad < interactor.PlayerMaxLoadCapacity)
        {
            base.Interact(interactor);
            interactor.WalletAmount += ItemValue;
            interactor.PlayerCurrentLoad += ItemWeight;
            Destroy(gameObject);
        }
        else
        {
            Debug.Log("Maximum load capacity is reached");
        }
    }
}
