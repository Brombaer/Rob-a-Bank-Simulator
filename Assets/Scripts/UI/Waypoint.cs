using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
	[SerializeField] private Transform _target;



	private void Update()
	{
		LookAtTarget();
	}

	private void LookAtTarget()
	{
		transform.LookAt(_target);
	}
}
