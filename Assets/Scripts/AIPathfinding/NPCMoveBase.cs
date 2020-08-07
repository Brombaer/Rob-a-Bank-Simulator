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

		[SerializeField] protected float _detectDistance;
		[SerializeField] protected float _shootDistance;

		[SerializeField] protected Transform _playerTransform;

		protected float _distanceToPlayer;
		protected bool isShooting = false;
		protected bool isRunning = false;
		protected bool isAlert = false;
		protected bool isCovered = false;
		private void Start()
		{

		}

		private void Update()
		{
			PlayerDistance();
			SetDestination();
			if(isAlert)
			{
				transform.LookAt(_playerTransform);
			}
		}

		protected abstract void SetDestination();

		protected abstract void PlayerDistance();
	}
}