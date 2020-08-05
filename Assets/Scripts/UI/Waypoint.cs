using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Waypoint : MonoBehaviour
{
	[SerializeField] private Image _img;
	[SerializeField] private Transform _target;
	[SerializeField] private Text _meter;
	[SerializeField] private Vector3 _offset;


	private void Update()
	{
		float minX = _img.GetPixelAdjustedRect().width / 2;
		float maxX = Screen.width - minX;

		float minY = _img.GetPixelAdjustedRect().height / 2;
		float maxY = Screen.height - minY;
		
		Vector2 pos = Camera.main.WorldToScreenPoint(_target.position + _offset);
		
		if(Vector3.Dot(_target.position - transform.position, transform.forward) < 0)
		{
			if ((pos.x < Screen.width / 2))
				pos.x = maxX;
			else
				pos.x = minX;
		}

		pos.x = Mathf.Clamp(pos.x, minX, maxX);
		pos.y = Mathf.Clamp(pos.y, minY, maxY);

		_img.transform.position = pos;
		_meter.text = Mathf.RoundToInt(Vector3.Distance(_target.position, transform.position)).ToString();
	}
}
