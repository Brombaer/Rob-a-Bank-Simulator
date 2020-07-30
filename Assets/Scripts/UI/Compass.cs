using UnityEngine;
using UnityEngine.UI;

public class Compass : MonoBehaviour
{
	[SerializeField] private RectTransform _waypointLayer;
	[SerializeField] private RectTransform _northLayer;

	[SerializeField] private Transform _waypointPlace;
	[SerializeField] private Transform _player;

	private Vector3 _northDirection;
	private Quaternion _waypointDirection;


	private void Update()
	{
		ChangeNorthDirection();
		ChangeWaypointDirection();
	}

	private void ChangeNorthDirection()
	{
		_northDirection.z = _player.eulerAngles.y;
		_northLayer.localEulerAngles = _northDirection;
	}

	private void ChangeWaypointDirection()
	{
		Vector3 dir = _player.position - _waypointPlace.position;

		_waypointDirection = Quaternion.LookRotation(dir);

		_waypointDirection.z = -_waypointDirection.y;
		_waypointDirection.y = 0;
		_waypointDirection.x = 0;

		_waypointLayer.localRotation = _waypointDirection * Quaternion.Euler(_northDirection);
	}
}
