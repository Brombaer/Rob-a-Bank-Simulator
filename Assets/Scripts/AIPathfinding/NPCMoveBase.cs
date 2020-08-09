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

		[SerializeField] private Weapon _weapon;

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
				TemporaryShooting();
			}
			else
			{
				_weapon.StopFire();
			}
		}

		private void TemporaryShooting()
		{
			if (_weapon.CurrentAmmo > 0)
			{
				_weapon.BeginFire();
			}
			else
			{
				_weapon.Reload();
			}
		}

		protected abstract void SetDestination();

		protected abstract void PlayerDistance();
	}
}