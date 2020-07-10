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

		FMOD.ATTRIBUTES_3D at3d = new FMOD.ATTRIBUTES_3D();
		FMOD.VECTOR fVector = new FMOD.VECTOR();
		fVector.x = transform.position.x;
		fVector.y = transform.position.y;
		fVector.z = transform.position.z;
		at3d.position = fVector;

		_eventInst.set3DAttributes(at3d);
	}

	private void Update()
	{
		if (Input.GetMouseButtonDown(1))
			_eventInst.start();

	}
}
