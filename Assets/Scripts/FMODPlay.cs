using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FMODPlay : MonoBehaviour
{
	[FMODUnity.EventRef]
	[SerializeField] private string _amb;

	// Start is called before the first frame update
	void Start()
	{
		FMODUnity.RuntimeManager.PlayOneShot(_amb, gameObject.transform.position);
	}
}
