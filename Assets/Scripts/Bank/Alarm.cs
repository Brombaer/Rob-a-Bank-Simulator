using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alarm : MonoBehaviour
{
	[SerializeField] private PlayerCharacter _playerChar;
	[Space]
	[Header("Spawn Prefab List")]
	[SerializeField] private List<GameObject> _vehicleList = new List<GameObject>();
	[SerializeField] private List<GameObject> _enemyList = new List<GameObject>();
	[Space]
	[Header("Spawn Locations List")]
	[SerializeField] private List<Transform> _vehicleLocationList = new List<Transform>();
	[SerializeField] private List<Transform> _enemyLocationList = new List<Transform>();
	[Space]
	[Header("Spawn UI")]
	[SerializeField] private GameObject _detectedUIPrefab;
	[Space]
	[Header("FMOD Sounds")]
	[FMODUnity.EventRef]
	[SerializeField] private string _bell;
	[SerializeField] private Transform _bellLocation;

	public event Action<int> UpdateState;

	private CountDown _countDown;
	private bool _doOnce = true;
	private bool _inBank;

	private void Awake()
	{
		_countDown = _detectedUIPrefab.GetComponentInChildren<CountDown>();
	}

	private void Update()
	{
		CheckIfHolstered();
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			_inBank = true;
		}
	}

	private void OnTriggerExit(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			_inBank = false;
		}
	}

	private void CheckIfHolstered()
	{
		if (_doOnce && _inBank && !_playerChar.IsHolstered)
		{
			UpdateState?.Invoke(1);
			FMODUnity.RuntimeManager.PlayOneShotAttached(_bell, _bellLocation.gameObject);
			_detectedUIPrefab.SetActive(true);

			StartCoroutine(AlarmTriggered());
			_doOnce = false;
		}
	}

	IEnumerator AlarmTriggered()
	{
		yield return new WaitForSeconds(_countDown.AlarmTimer);

		for (int i = 0; i < _vehicleLocationList.Count; i++)
		{
			int chooseRandomVehicle = UnityEngine.Random.Range(0, _vehicleList.Count);

			Instantiate(_vehicleList[chooseRandomVehicle], _vehicleLocationList[i].position, _vehicleLocationList[i].rotation);
		}

		for (int i = 0; i < _enemyLocationList.Count; i++)
		{
			int chooseRandomEnemy = UnityEngine.Random.Range(0, _enemyList.Count);

			Instantiate(_enemyList[chooseRandomEnemy], _enemyLocationList[i].position, _enemyLocationList[i].rotation);
		}

		yield return new WaitForSeconds(2);

		_detectedUIPrefab.SetActive(false);
	}
}