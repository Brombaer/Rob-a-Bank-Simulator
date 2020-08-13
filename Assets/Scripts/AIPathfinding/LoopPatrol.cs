using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.AI;

namespace Assets.Scripts.AIPathfinding
{
	public class LoopPatrol : NPCBase
	{
		[SerializeField] private List<Waypoint> _waypoints;
		[SerializeField] private List<Waypoint> _coverPoints;
		[SerializeField] private Waypoint _closestCover;

		int _waypointCounter = 0;
	
		private float _currentWaitTime = 4.0f;
		[SerializeField] private float _waitTime = 4.0f;

		

		protected override void Update()
		{
			base.Update();

			if (CanSeePlayer() || _currentState == NPCStates.Aggro)
			{
				_currentState = NPCStates.Aggro;

				FindBestCover();
			}
			else
			{
				_currentState = NPCStates.Alert;


				//decides whether or not its time to move
				if (TargetReached())
				{
					if (_currentWaitTime >= _waitTime)
					{
						FindNextWaypoint();
					}
					else
					{
						_currentWaitTime += 1 * Time.deltaTime;
					}
				}
			}
		}

		private void FindNextWaypoint()
		{
			if (TargetReached())
			{
				//resets timer 
				_currentWaitTime = 0;

				//finds the next destination
				_targetPosition = _waypoints[_waypointCounter].transform.position;

				//increments counter with overflow exception
				_waypointCounter = (_waypointCounter + 1) % _waypoints.Count;
			}
		}

		private void FindBestCover()
		{
			_closestCover = _coverPoints[0];

			for (int i = 1; i < _waypoints.Count; i++)
			{
				if (PlayerDistanceTo(_coverPoints[i].transform) < PlayerDistanceTo(_coverPoints[i - 1].transform))
				{
					_closestCover = _coverPoints[i];
				}
			}
			_navMeshAgent.destination = _closestCover.transform.position;
		}
	}
}