using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WaypointPatrol : MonoBehaviour
{

    private GameObject[] waypoints;
    private NavMeshAgent navMeshAgent;
    private int waypointIndex;
    


    // Start is called before the first frame update
    void Start()
    {

        waypoints = GameManager.Instance.waypoints;
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
        if(navMeshAgent.remainingDistance < 3.3f && !navMeshAgent.pathPending)
        {
            Debug.Log(waypointIndex);
            waypointIndex = ++waypointIndex % waypoints.Length;
        }
    }
}
