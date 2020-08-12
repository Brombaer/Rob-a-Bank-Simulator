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
		[SerializeField] private List<Waypoint> _coverPoints;
		[SerializeField] private Waypoint _closestCover;

		int _waypointCounter = 0;

		//private RaycastHit _hit;
	
		private float _currentWaitTime = 4.0f;
		[SerializeField] private float _waitTime = 4.0f;

		

		protected override void SetDestination()
		{
			if (_distanceToPlayer <= _detectDistance || isAlert)
			{
				isAlert = true;
				//if (_distanceToPlayer > _shootDistance)
				//{
					isRunning = true;

					_closestCover = _coverPoints[0];

					for(int i = 1; i < _waypoints.Count; i++)
					{
						if(PlayerDistance(_coverPoints[i].transform) < PlayerDistance(_coverPoints[i - 1].transform))
						{
							_closestCover = _coverPoints[i];
						}
					}
					_navMeshAgent.destination = _closestCover.transform.position;
				//}
				//else
				//{
					//_navMeshAgent.destination = transform.position;
					//isShooting = true;
				//}
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
					_navMeshAgent.destination = _destination.position;
				}
				else
				{
					isRunning = false;
					_currentWaitTime += 1 * Time.deltaTime;
				}
			}
		}

		protected override float PlayerDistance(Transform target)
		{
			float distance = Vector3.Distance(_playerTransform.position, target.position);

			return distance;
		}
	}
}