using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyPatrol : MonoBehaviour
{
    public Transform[] waypoints;
    public NavMeshAgent agent;
    public int targetWaypoint;
    public float waypointThreshold = 1f;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        agent.destination = waypoints[targetWaypoint].position;

        if (Vector3.Distance(agent.transform.position, waypoints[targetWaypoint].position) < waypointThreshold)
        {
            targetWaypoint++;
            if(targetWaypoint >= waypoints.Length)
            {
                targetWaypoint = 0;
            }
        }
    }

}
