using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WaypointPatrol : MonoBehaviour
{

    [SerializeField] private GameObject[] waypoints;
    private NavMeshAgent navMeshAgent;
    private int waypointIndex;
    private GameManager gameManagerScript;


    // Start is called before the first frame update
    void Start()
    {
       
        navMeshAgent = gameObject.GetComponent<NavMeshAgent>();
 
        waypointIndex = Random.Range(0, waypoints.Length);
    }

    // Update is called once per frame
    void Update()
    {
        MoveToNextWaypoint();
    }

    private void MoveToNextWaypoint()
    {
        navMeshAgent.SetDestination(waypoints[waypointIndex].transform.position);
        if(navMeshAgent.remainingDistance < 0.1f && !navMeshAgent.pathPending && waypointIndex < 15)
        {

            waypointIndex = ++waypointIndex;
        }
    }
}
