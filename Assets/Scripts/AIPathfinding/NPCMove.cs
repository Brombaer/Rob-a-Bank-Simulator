using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCMove : MonoBehaviour
{
    [SerializeField] private Transform _destination;

    [SerializeField] private NavMeshAgent _navMeshAgent;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        SetDestination();
    }

    private void SetDestination()
	{
        if(_destination != null)
		{
            Vector3 targetVector = _destination.transform.position;
            _navMeshAgent.SetDestination(targetVector);
		}
	}
}
