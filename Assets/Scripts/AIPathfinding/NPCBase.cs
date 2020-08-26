﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.MemoryProfiler;
using UnityEngine;
using UnityEngine.AI;

namespace Assets.Scripts.AIPathfinding
{
	public enum NPCStates
	{
		Idle,

		Alert,

		Aggro,

		Commando

	}
	public class NPCBase : MonoBehaviour
	{
		[SerializeField] float _maxSightConeAngle;

		[SerializeField] private GameObject _ragdoll;

		[SerializeField] protected NavMeshAgent _navMeshAgent;

		[SerializeField] protected float _detectDistance;
		[SerializeField] protected float _shootDistance;

		protected Vector3? _targetPosition;

		[SerializeField] private Weapon _rifle;
		[SerializeField] private Weapon _handgun;

		protected bool isCovered = false;
		[SerializeField] private bool usingHandgun = false;

		[SerializeField] private bool _instantCommando = false;

		
		public Weapon Weapon
		{
			get => usingHandgun? _handgun : _rifle;
		}

		public bool UsingHandgun
		{
			get => usingHandgun;
		}

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

		protected virtual void Start()
		{
			

			AIHandler.Instance.Aggro += OnAggro;
		}

		protected void OnAggro()
		{
			if (_instantCommando)
				_currentState = NPCStates.Commando;
			else
				_currentState = NPCStates.Aggro;
		}

		private void UpdateStates()
		{
			if (_currentState == NPCStates.Aggro)
			{
				Vector3 target = GetPlayerTransform().position;
				target.y = transform.position.y;

				transform.LookAt(target);
				TemporaryShooting();
			}

			if(_currentState == NPCStates.Commando)
			{
				Vector3 target = GetPlayerTransform().position;
				target.y = transform.position.y;

				transform.LookAt(target);
				TemporaryShooting();

				_targetPosition = GetPlayerTransform().position;
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
				Vector3 lineStart = transform.position + new Vector3(0, 1.6f, 0);

				Vector3 direction = Vector3.Normalize(GetPlayerTransform().position - lineStart);
				float angle = Mathf.Acos(Vector3.Dot(transform.forward, direction)) * Mathf.Rad2Deg;

				if (angle < _maxSightConeAngle)
				{
					Debug.DrawLine(lineStart, lineStart + direction * _detectDistance);
					if (Physics.Raycast(lineStart, direction, out RaycastHit hitInfo, PlayerDistanceTo(transform)))
					{
						if (hitInfo.transform == GetPlayerTransform())
							return true;
						return false;
					}
					else
						return false;
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
			Weapon weapon = Weapon;
			if (weapon.CurrentAmmo > 0)
			{
				if (CanSeePlayer())
					weapon.BeginFire();
				else
					weapon.StopFire();
			}
			else
			{
				weapon.Reload();
			}
		}

		protected float PlayerDistanceTo(Transform target)
		{
			return Vector3.Distance(GetPlayerTransform().position, target.position);
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

		protected Transform GetPlayerTransform()
		{
			return AIHandler.Instance.PlayerTransform.transform;
		}
	}

}