using System.Collections;
using UnityEngine;

public class FadeScreen : MonoBehaviour
{
	[SerializeField] private bool _fadeIn;
	[SerializeField] private float _fadeDuration = 2;

	private Animator _animator;

	private void Awake()
	{
		gameObject.SetActive(true);


		_animator = GetComponent<Animator>();

		if (_animator != null)
		{
			_animator.SetBool("In", _fadeIn);
			StartCoroutine(DestroyAfterSeconds(_fadeDuration));
		}
	}

	IEnumerator DestroyAfterSeconds(float seconds)
	{ 
		yield return new WaitForSeconds(seconds);
		Destroy(gameObject);
	}
}
