using System.Collections;
using UnityEngine;
using TMPro;

public class CountDown : MonoBehaviour
{
	[SerializeField] private Alarm _alarm;
	private TextMeshProUGUI _text;


	private void Awake()
	{
		_text = GetComponent<TextMeshProUGUI>();
	}

	private void Update()
	{
		if(_alarm.SpawnDelay > 0)
		{
			int newTime = Mathf.RoundToInt(_alarm.SpawnDelay);
			_text.text = $"{newTime}s left";
		}
		else
		{
			_text.text = $"Police force has arrived!";
		}
	}
}
