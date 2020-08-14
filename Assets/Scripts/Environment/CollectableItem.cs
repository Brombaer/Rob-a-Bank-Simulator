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
        base.Interact(interactor);

        interactor.WalletAmount += ItemValue;
        Destroy(gameObject);
    }
}
