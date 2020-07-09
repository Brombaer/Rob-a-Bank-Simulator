using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FMODTest : MonoBehaviour
{
	[FMODUnity.EventRef]
	[SerializeField] private string _event01;

	[FMODUnity.BankRef]
	[SerializeField] private string _bank01;

	private FMOD.Studio.EventInstance _eventInst;

	private void Start()
	{
		_eventInst = FMODUnity.RuntimeManager.CreateInstance(_event01);
	}

	private void Update()
	{
		if (Input.GetMouseButtonDown(1))
			_eventInst.start();

	}
}
