using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FMODTest : MonoBehaviour
{
	[FMODUnity.EventRef]
	[SerializeField] private string _event01;

	[FMODUnity.BankRef]
	[SerializeField] private string _bank01;

	[SerializeField] private bool _loop;
	[SerializeField] private float _loopDuration;
	[SerializeField] private int _loopAmount;

	private float _lastLoopDuration;
	private FMOD.Studio.EventInstance _eventInst;

	private void Start()
	{
		FMODUnity.RuntimeManager.PlayOneShot(_event01, gameObject.transform.position);
		
		if(_loop)
			StartCoroutine(LoopAudio());
	}

	private void Update()
	{

	}

	IEnumerator LoopAudio()
	{
		while (true)
		{
			yield return new WaitForSeconds(1);
			FMODUnity.RuntimeManager.PlayOneShot(_event01, gameObject.transform.position);
		}
	}
}
