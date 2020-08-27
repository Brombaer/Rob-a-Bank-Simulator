using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGame : Interactable
{
    [SerializeField] private CompleteGameUI _gameCompleteUIRef;

    public override void Interact(PlayerInteractController interactor)
    {
        Debug.Log("End game");

        var ui = Instantiate(_gameCompleteUIRef.gameObject);
        Time.timeScale = 0;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
