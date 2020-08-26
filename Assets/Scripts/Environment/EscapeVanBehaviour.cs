using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EscapeVanBehaviour : MonoBehaviour
{
    [SerializeField] private GameObject _player;
    [SerializeField] private PlayerInteractController _playerScript;

    [SerializeField] private GameObject[] _vanRearDoors;
    [SerializeField] private VanRearDoorBehaviour _leftVanDoorScript;
    [SerializeField] private VanRearDoorBehaviour _rightVanDoorScript;

    [SerializeField] private GameObject[] _stolenGoods;

    private void Awake()
    {
        foreach (GameObject gameObject in _stolenGoods)
        {
            gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == _player)
        {
            _leftVanDoorScript.SwitchDoorState();
            _rightVanDoorScript.SwitchDoorState();
            
            _playerScript.SafedMoney += _playerScript.PlayerWallet;
            _playerScript.PlayerWallet = 0;

            _playerScript.PlayerCurrentLoad = 0;

            //for (int i = 0; i < _stolenGoods.Length; i++)
            //{
            //    if ()
            //    {
            //        _stolenGoods[i].gameObject.SetActive(true);
            //    }
            //}
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == _player)
        {
            _leftVanDoorScript.SwitchDoorState();
            _rightVanDoorScript.SwitchDoorState();
        }
    }
}
