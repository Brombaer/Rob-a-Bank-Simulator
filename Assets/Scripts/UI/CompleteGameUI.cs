using System.Collections.Generic;
using System.Collections;
using System.Globalization;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using Assets.Scripts.AIPathfinding;

public class CompleteGameUI : MonoBehaviour
{
	[SerializeField] private TextMeshProUGUI _money;
	[SerializeField] private TextMeshProUGUI _kills;
	[SerializeField] private TextMeshProUGUI _time;

	[SerializeField] private HealthComponent _healthComp;

	[FMODUnity.EventRef]
	[SerializeField] private string _onClicked;

	private int _seconds;
	private int _minutes;
	private int _hours;
	private bool _doOnce = true;



	private void Awake()
	{
		TotalTime();
		TimeSpan _timeSpan = new TimeSpan(_hours, _minutes, _seconds);


		_time.text = $"Total Time: {_timeSpan:c}";
		_kills.text = $"Kills: {AIHandler.Instance.KillCount.ToString("#,#", CultureInfo.InvariantCulture)}";
	}

	public void UpdateUI(int money)
	{
		_money.text = $"Money Stolen {money.ToString("C")}";
	}

	public void ExitToMainMenu()
	{
		StartCoroutine(ButtonDelay());
	}

	private void TotalTime()
	{
		_seconds = Mathf.RoundToInt(Time.time);

		_minutes = _seconds / 60;
		_hours = _minutes / 60;
		_seconds = _seconds % 60;
	}

	IEnumerator ButtonDelay()
	{
		FMODUnity.RuntimeManager.PlayOneShot(_onClicked, gameObject.transform.position);

		yield return new WaitForSeconds(2);
		SceneManager.LoadScene(0);
	}
}
