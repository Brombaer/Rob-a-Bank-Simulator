using System.Collections;
using UnityEngine;
using TMPro;

public class CountDown : MonoBehaviour
{
	[SerializeField] private float _counter;

	public float AlarmTimer { get => _counter; }
	
	private float _timer;
	private TextMeshProUGUI _text;


	private void Awake()
	{
		_text = GetComponent<TextMeshProUGUI>();
	}

	private void Start()
	{
		_timer = _counter;
	}

	private void Update()
	{
		if(_timer > 0)
		{
			int newTime = Mathf.RoundToInt(_timer);
			_text.text = $"{newTime}s left";
		}
		else
		{
			_text.text = $"Police force has arrived!";
		}

		_timer -= Time.deltaTime;
	}
}
