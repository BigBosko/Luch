using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class EnemyAI : MonoBehaviour
{
    [Header("Patrol")]
    public Transform[] waypoints;
    public NavMeshAgent agent;
    public int targetWaypoint;
    public float waypointThreshold; //robot height *0.5 + buffer
    public float patrolSpeed;

    [Header("Detection")]
    public float detectionDistance;  // Distance the cone can detect
    public float coneAngle;  // Angle of the cone (degrees)
    public LayerMask playerLayer;  // Only detect player layer
    public Transform lightOrigin;  // The origin of the light (usually at the front of the robot)
    public bool isPlayerDetected;


    [Header("Chase")]
    public Transform player;
    public Vector3 lastPosition;
    public bool isChasing;
    public float lastPositionTreshold;
    public float chaseSpeed;

    /*[Header("Scouting")]
    public float scoutDuration = 5f;
    public float scoutRotationSpeed = 30f;
    public bool isScouting;*/


    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }


    void Update()
    {
        if (isPlayerDetected)
        {
            isChasing = true;
            lastPosition = player.position;
            Chase();
        }
        else if (!isPlayerDetected)
        {
            if (isChasing)
            {
                Chase();
            }
            else if (!isChasing)
            {
                Patrol();
            }
        }
        Detect();
    }

    
    void Chase()
    {
        agent.speed = chaseSpeed;
        if (Vector3.Distance(agent.transform.position, lastPosition) <= lastPositionTreshold)
        {
            isChasing = false;
        }

        agent.destination = lastPosition;
    }


    void Patrol()
    {
        agent.speed = patrolSpeed;
        agent.destination = waypoints[targetWaypoint].position;

        if (Vector3.Distance(agent.transform.position, waypoints[targetWaypoint].position) < waypointThreshold)
        {
            Debug.Log($"Patrol: Reached waypoint {targetWaypoint}");
            targetWaypoint++;
            if (targetWaypoint >= waypoints.Length)
            {
                targetWaypoint = 0; // Loop back to the first waypoint
            }
        }
    }


    void Detect()
    {
        // Find all colliders within a sphere around the light origin
        Collider[] detectedColliders = Physics.OverlapSphere(lightOrigin.position, detectionDistance, playerLayer);

        bool playerDetected = false; // Temporarily store the detection result

        foreach (var collider in detectedColliders)
        {
            Vector3 directionToPlayer = (collider.transform.position - lightOrigin.position).normalized;

            //Debug.Log($"Checking object: {collider.gameObject.name}");

            // Check if the player is within the cone's angle
            float angleToPlayer = Vector3.Angle(lightOrigin.forward, directionToPlayer);
            if (angleToPlayer < coneAngle / 2)
            {
                //Debug.Log($"Object {collider.gameObject.name} is within cone angle.");

                // Perform a raycast to see if the path to the player is clear
                RaycastHit hit;
                if (Physics.Raycast(lightOrigin.position, directionToPlayer, out hit, detectionDistance))
                {
                    //Debug.DrawRay(lightOrigin.position, directionToPlayer * detectionDistance, Color.red);

                    //Debug.Log($"Raycast hit: {hit.collider.gameObject.name}");

                    // Ensure the raycast hits the player object
                    if (hit.collider.gameObject == collider.gameObject)
                    {
                        //Debug.Log("Player detected!");
                        playerDetected = true;
                        break; // Exit loop if player is detected
                    }
                    else
                    {
                        //Debug.Log($"Raycast hit {hit.collider.gameObject.name} instead of player.");
                    }
                }
                else
                {
                    //Debug.Log("Raycast didn't hit anything.");
                }
            }
            else
            {
                //Debug.Log($"Object {collider.gameObject.name} is outside the cone angle.");
            }
        }

        // Update the detection state
        isPlayerDetected = playerDetected;
    }


}
