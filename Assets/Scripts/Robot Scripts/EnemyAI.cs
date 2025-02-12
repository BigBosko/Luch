using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Experimental.GlobalIllumination;


public class EnemyAI : MonoBehaviour
{
    [Header("Detection")]
    public LayerMask playerLayer;  // Only detect player layer
    public Light spotLight;
    public bool isPlayerDetected;

    [Header("Patrol")]
    public Transform[] waypoints;
    public NavMeshAgent agent;
    public int targetWaypoint;
    public float waypointThreshold; //robot height * 0.5 + buffer
    public float patrolSpeed;
    public float patrolLightDistance;
    public float patrolLightAngle;

    [Header("Chase")]
    public Transform player;
    public Vector3 lastPosition;
    public bool isChasing;
    public float lastPositionTreshold;
    public float chaseSpeed;
    public float chaseLightDistance;
    public float chaseLightAngle;

    [Header("Scout")]
    public float scoutDuration = 8f;
    public bool isScouting;
    public float scoutLightDistance;
    public float scoutLightAngle;


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
        spotLight.range = chaseLightDistance;
        spotLight.spotAngle = chaseLightAngle;

        agent.destination = lastPosition;

        if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
        {
            if (!isPlayerDetected)
            {
                isChasing = false;
                Scout();
            }
        }

    }


    void Patrol()
    {
        agent.speed = patrolSpeed;
        agent.destination = waypoints[targetWaypoint].position;
        spotLight.range = patrolLightDistance;
        spotLight.spotAngle = patrolLightAngle;

        if (Vector3.Distance(agent.transform.position, waypoints[targetWaypoint].position) < waypointThreshold)
        {
            //Debug.Log($"Patrol: Reached waypoint {targetWaypoint}");
            targetWaypoint++;
            if (targetWaypoint >= waypoints.Length)
            {
                targetWaypoint = 0; // Loop back to the first waypoint
            }
        }
    }


    void Detect()
    {
        float detectionDistance;
        float detectionAngle;
        if (isChasing)
        {
            detectionDistance = chaseLightDistance;
            detectionAngle = chaseLightAngle;
        }
        else
        {
            detectionDistance = patrolLightDistance;
            detectionAngle = patrolLightAngle;
        }

        Transform lightOrigin = spotLight.gameObject.transform;
        Collider[] detectedColliders = Physics.OverlapSphere(lightOrigin.position, detectionDistance, playerLayer);

        bool playerDetected = false; // Temporarily store the detection result

        foreach (var collider in detectedColliders)
        {
            Vector3 directionToPlayer = (collider.transform.position - lightOrigin.position).normalized;

            //Debug.Log($"Checking object: {collider.gameObject.name}");

            // Check if the player is within the cone's angle
            float angleToPlayer = Vector3.Angle(lightOrigin.forward, directionToPlayer);
            if (angleToPlayer < detectionAngle / 2)
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

    public void Stun()
    {
        agent.isStopped = true;

        agent.speed = 0;

        isChasing = false;

        StartCoroutine(StunDuration());
    }

    private IEnumerator StunDuration()
    {
        yield return new WaitForSeconds(5f);

        agent.isStopped = false;
        agent.speed = patrolSpeed;
    }

    public void Scout()
    {
        isScouting = true;
        spotLight.range = scoutLightDistance;
        spotLight.spotAngle = scoutLightAngle;
        if (!isScouting)
        {
            StartCoroutine(RotateWhileScouting());
        }
    }

    private IEnumerator RotateWhileScouting()
    {
        Quaternion startRotation = transform.rotation;
        Quaternion targetRotation = startRotation * Quaternion.Euler(0, 360, 0); // Rotate around Y-axis
        float elapsedTime = 0f;

        while (elapsedTime < scoutDuration)
        {
            elapsedTime += Time.deltaTime;
            float progress = elapsedTime / scoutDuration; // Normalize progress
            transform.rotation = Quaternion.Slerp(startRotation, targetRotation, progress);
            yield return null;
        }

        transform.rotation = targetRotation; // Ensure exact final rotation
        isScouting = false;
    }

}
