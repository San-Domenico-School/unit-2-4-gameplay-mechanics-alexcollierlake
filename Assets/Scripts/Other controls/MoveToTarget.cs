using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/*************
 * 
 * Alexandra Collier-Lake
 * 1/22/1014
 * Move to Target: //
 * Component of ice sphere
 * 
 *************/

public class MoveToTarget : MonoBehaviour
{
    [SerializeField] private NavMeshAgent navMeshAgent;
    private GameObject target;
    private Rigidbody targetRB;


    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.Find("Player");
        targetRB = target.GetComponent<Rigidbody>();
        navMeshAgent = GetComponent<NavMeshAgent>();

    }

    // Update is called once per frame
    void Update()
    {
        MoveTowardsTarget();
    }

    private void MoveTowardsTarget()
    {
        navMeshAgent.SetDestination(targetRB.transform.position);
    }
}
