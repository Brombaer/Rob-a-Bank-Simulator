using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Assets.Scripts.AIPathfinding
{
	public class AnimationAI : MonoBehaviour
	{

		[SerializeField] private NavMeshAgent _navMeshAgent;
		[SerializeField] private Weapon _weapon;

		[SerializeField] private Animator _animator;
		private void Start()
		{
			_weapon.Fired += OnFired;
			
		}

		private void OnFired()
		{
			_animator.Play("Fire", 1);
		}

		private void Update()
		{
			float x = Vector3.Dot(transform.right, _navMeshAgent.velocity);
			float y = Vector3.Dot(transform.forward, _navMeshAgent.velocity);

			_animator.SetFloat("Vertical", y);
			_animator.SetFloat("Horizontal", x);
		}
	}
}