using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.AI;

namespace Assets.Scripts.AIPathfinding
{
	public class LoopPatrol : NPCMoveBase
	{
		[SerializeField] private List<Waypoint> _waypoints;

		int _waypointCounter = 0;

		//private RaycastHit _hit;
	
		private float _currentWaitTime = 4.0f;
		[SerializeField] private float _waitTime = 4.0f;

		

		protected override void SetDestination()
		{
			if (_distanceToPlayer <= _detectDistance)
			{
				isAlert = true;
				if (_distanceToPlayer > _shootDistance)
				{
					isRunning = true;
					_navMeshAgent.destination = _playerTransform.position;
				}
				else
				{
					_navMeshAgent.destination = transform.position;
					isShooting = true;
				}
			}
			else
			{
				isAlert = false;
				if (((_destination == null) || (_destination.position == transform.position)))
				{
					//resets timer 
					_currentWaitTime = 0;

					//finds the next destination
					_destination = _waypoints[_waypointCounter].transform;
					_destination.position = _waypoints[_waypointCounter].transform.position;

					//increments counter with overflow exception
					_waypointCounter = (_waypointCounter + 1) % _waypoints.Count;
				}

				//decides whether or not its time to move
				if (_currentWaitTime >= _waitTime)
				{
					isRunning = true;
					_navMeshAgent.SetDestination(_destination.position);
				}
				else
				{
					isRunning = false;
					_currentWaitTime += 1 * Time.deltaTime;
				}
			}
		}

		protected override void PlayerDistance()
		{
			_distanceToPlayer = Vector3.Distance(_playerTransform.position, transform.position);
		}
	}
}