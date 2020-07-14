using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Assets.Scripts.AIPathfinding
{
	public abstract class NPCMoveBase : MonoBehaviour
	{
		[SerializeField] protected Transform _destination;

		[SerializeField] protected NavMeshAgent _navMeshAgent;

		private void Start()
		{

		}

		private void Update()
		{
			SetDestination();
		}

		protected abstract void SetDestination();
	}
}