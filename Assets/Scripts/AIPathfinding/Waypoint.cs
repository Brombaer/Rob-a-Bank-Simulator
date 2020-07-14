using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.AIPathfinding
{
	public class Waypoint : MonoBehaviour
	{
		[SerializeField] protected float degubDrawRadius = 1.0f;

		public virtual void OnDrawGizmos()
		{
			Gizmos.color = Color.red;
			Gizmos.DrawWireSphere(transform.position, degubDrawRadius);
		}
	}
}