using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIWaypoint : MonoBehaviour
{
	[SerializeField] private Sprite _worldIcon;
	[SerializeField] private Sprite _compassIcon;
	[SerializeField] private Transform _target;
	//[SerializeField] private Text _meter;
	[SerializeField] private Vector3 _offset;
	[SerializeField] private GameObject _imagePrefab;
	[SerializeField] private PlayerDeath _playerDeath;

	public Sprite CompassIcon { get => _compassIcon; }
	private Image _img;

	private void Start()
	{
		_img = Instantiate(_imagePrefab).GetComponentInChildren<Image>();

		_img.sprite = _worldIcon;
	}

	private void Update()
	{
		if (!_playerDeath.IsDead)
		{
			Vector2 pos = Camera.main.WorldToScreenPoint(transform.position);

			if (Vector3.Dot(transform.position - _target.position, _target.forward) < 0)
			{
				_img.enabled = false;
			}
			else
			{
				_img.enabled = true;
			}

			_img.transform.position = pos;

			//_meter.text = Mathf.RoundToInt(Vector3.Distance(transform.position, _target.position)).ToString();
		}
	}
}
