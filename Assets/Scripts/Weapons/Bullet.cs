using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
	[Range(5, 100)]
	[SerializeField] private float _destroyAfter;
	[SerializeField] private float _damage;

	private void Start()
	{
		Destroy(gameObject, _destroyAfter);
	}

	private void OnCollisionEnter(Collision collision)
	{
		var damageable = collision.collider.GetComponent<IDamageable>();

		if (damageable != null)
			damageable.Damage(_damage);
	}
}
