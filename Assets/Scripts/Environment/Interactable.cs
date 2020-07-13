using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour
{
    public UnityEvent OnInteracted;

    public void Interact()
    {
        Debug.Log("Hit!");
        OnInteracted?.Invoke();
    }
}
