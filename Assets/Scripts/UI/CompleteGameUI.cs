using System.Collections.Generic;
using System.Collections;
using System.Globalization;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class CompleteGameUI : MonoBehaviour
{
	[SerializeField] private TextMeshProUGUI _money;
	[SerializeField] private TextMeshProUGUI _kills;
	[SerializeField] private TextMeshProUGUI _time;

	[SerializeField] private HealthComponent _healthComp;

	[FMODUnity.EventRef]
	[SerializeField] private string _onClicked;

	public int KillCount { get; private set; }

	private float _timer;
	private bool _doOnce = true;



	private void Awake()
	{
		_healthComp.Death += KillCounter;
	}

	private void KillCounter()
	{
		KillCount++;
	}

	public void UpdateUI(int money, int hours, int seconds)
	{
		_money.text = $"Money Stolen {money.ToString("#,#", CultureInfo.InvariantCulture)}";

		_kills.text = $"Kills: {KillCount.ToString("#,#", CultureInfo.InvariantCulture)}";

		_time.text = $"Total Time: {hours}:{seconds}";
	}

	public void ExitToMainMenu()
	{
		StartCoroutine(ButtonDelay());
	}

	IEnumerator ButtonDelay()
	{
		FMODUnity.RuntimeManager.PlayOneShot(_onClicked, gameObject.transform.position);

		yield return new WaitForSeconds(2);
		SceneManager.LoadScene(0);
	}
}
