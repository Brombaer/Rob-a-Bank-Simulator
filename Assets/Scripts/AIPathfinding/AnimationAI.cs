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
		[SerializeField] private bool _isHandgun;
		[SerializeField] private GameObject _riflePrefab;
		[SerializeField] private GameObject _handgunPrefab;

		[SerializeField] private Animator _animator;


		private void Awake()
		{
			_handgunPrefab.SetActive(false);
			_riflePrefab.SetActive(false);
		}

		private void Start()
		{
			_weapon.Fired += OnFired;

			_animator.SetBool("isHandgun", _isHandgun);

			if (_isHandgun)
				_handgunPrefab.SetActive(true);
			else
				_riflePrefab.SetActive(true);
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