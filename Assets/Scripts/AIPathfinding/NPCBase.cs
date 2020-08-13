using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Assets.Scripts.AIPathfinding
{
	public enum NPCStates
	{
		Idle,

		Alert,

		Aggro
	}
	public class NPCBase : MonoBehaviour
	{
		[SerializeField] float _maxSightConeAngle;

		[SerializeField] private GameObject _ragdoll;

		[SerializeField] protected NavMeshAgent _navMeshAgent;

		[SerializeField] protected float _detectDistance;
		[SerializeField] protected float _shootDistance;

		[SerializeField] protected Transform _playerTransform;

		protected Vector3? _targetPosition;

		[SerializeField] private Weapon _weapon;

		protected bool isCovered = false;

		protected NPCStates _currentState;

		public float DistanceToPlayer { get => PlayerDistanceTo(transform); }

		private void Awake()
		{
			HealthComponent _health = GetComponent<HealthComponent>();

			if (_health == null)
				Destroy(this);
			else
				_health.Death += OnDeath;
		}

		private void UpdateStates()
		{
			if (_currentState == NPCStates.Aggro)
			{
				Vector3 target = _playerTransform.position;
				target.y = transform.position.y;

				transform.LookAt(target);
				TemporaryShooting();
			}

		}

		protected virtual void Update()
		{
			CanSeePlayer();
			UpdateStates();

			if(!TargetReached())
			{
				_navMeshAgent.SetDestination(_targetPosition.Value);
			}
			
		}

		protected bool TargetReached()
		{
			if (_targetPosition.HasValue)
			{
				float distance = (_targetPosition.Value - transform.position).magnitude;
				if (distance < _navMeshAgent.stoppingDistance)
					return true;
				else
					return false;
			}
			else
				return true;
		}

		protected bool CanSeePlayer()
		{
			if (PlayerDistanceTo(transform) <= _detectDistance)
			{
				Vector3 direction = Vector3.Normalize(_playerTransform.position - transform.position);
				float angle = Mathf.Acos(Vector3.Dot(transform.forward, direction)) * Mathf.Rad2Deg;

				if (angle < _maxSightConeAngle)
				{
					if (Physics.Raycast(transform.position, direction, out RaycastHit hitInfo, PlayerDistanceTo(transform)))
					{
						if (hitInfo.transform == _playerTransform)
							return true;
						return false;
					}
					else
						return true;
				}
				else
				{
					return false;
				}
			}
			else
			{
				return false;
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

		protected float PlayerDistanceTo(Transform target)
		{
			return Vector3.Distance(_playerTransform.position, target.position);
		}

		

		private void OnDeath()
		{
			Destroy(gameObject);
			GameObject ragdoll = Instantiate(_ragdoll, transform.position, transform.rotation);

			SkinnedMeshRenderer ragdollRenderer = ragdoll.GetComponentInChildren<SkinnedMeshRenderer>();

			SkinnedMeshRenderer renderer = gameObject.GetComponentInChildren<SkinnedMeshRenderer>(false);

			ragdollRenderer.sharedMaterial = renderer.sharedMaterial;
			ragdollRenderer.sharedMesh = renderer.sharedMesh;
		}
	}

}