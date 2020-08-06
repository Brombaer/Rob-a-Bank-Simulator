using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FMODPlayer : MonoBehaviour
{
	[FMODUnity.EventRef]
	[SerializeField] private string _soundEvent;

	[SerializeField] private bool _loop;
	[SerializeField] private float _loopDuration;

	private FMOD.Studio.EventInstance _eventInst;

	private void Start()
	{
		if(_loop)
			StartCoroutine(LoopAudio());
		else
			FMODUnity.RuntimeManager.PlayOneShot(_soundEvent, gameObject.transform.position);
	}

	IEnumerator LoopAudio()
	{
		while (true)
		{
			yield return new WaitForSeconds(_loopDuration);
			FMODUnity.RuntimeManager.PlayOneShot(_soundEvent, gameObject.transform.position);
		}
	}
}
