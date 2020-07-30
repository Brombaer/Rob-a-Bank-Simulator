using UnityEngine;
using System.Collections;


public class BulletScript : MonoBehaviour {

	[Range(5, 100)]
	[Tooltip("After how long time should the bullet prefab be destroyed?")]
	public float destroyAfter;
	[Tooltip("If enabled the bullet destroys on impact")]
	public bool destroyOnImpact = false;
	[Tooltip("Minimum time after impact that the bullet is destroyed")]
	public float minDestroyTime;
	[Tooltip("Maximum time after impact that the bullet is destroyed")]
	public float maxDestroyTime;

	[Header("Impact Effect Prefabs")]
	public Transform [] metalImpactPrefabs;

	private Surface _surface;

	private void Start() 
	{
		StartCoroutine (DestroyAfter());
	}


	private void OnCollisionEnter(Collision collision) 
	{
		Instantiate(metalImpactPrefabs [Random.Range(0, metalImpactPrefabs.Length)], transform.position, Quaternion.LookRotation (collision.contacts [0].normal), collision.transform);
		Destroy(gameObject);
		
		
		if (!destroyOnImpact) 
		{
			StartCoroutine (DestroyTimer());
		}
		else 
		{
			Destroy(gameObject);
		}
	}



	private IEnumerator DestroyTimer () 
	{
		yield return new WaitForSeconds(Random.Range(minDestroyTime, maxDestroyTime));
		Destroy(gameObject);
	}

	private IEnumerator DestroyAfter () 
	{
		yield return new WaitForSeconds (destroyAfter);
		Destroy (gameObject);
	}
}