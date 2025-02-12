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
    private float currentLightDistance;
    private float currentLightAngle;
    private float currentLightIntensity;

    [Header("Patrol")]
    public Transform[] waypoints;
    public NavMeshAgent agent;
    public int targetWaypoint;
    public float waypointThreshold; //robot height * 0.5 + buffer
    public float patrolSpeed;
    public float patrolLightDistance;
    public float patrolLightAngle;
    public float patrolLightIntensity;

    [Header("Chase")]
    public Transform player;
    public Vector3 lastPosition;
    public bool isChasing;
    public float lastPositionTreshold;
    public float chaseSpeed;
    public float chaseLightDistance;
    public float chaseLightAngle;
    public float ChaseLightIntensity;

    [Header("Scout")]
    public float scoutDuration;
    public bool isScouting;
    public float scoutLightDistance;
    public float scoutLightAngle;
    public float scoutLightIntensity;


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

    void UpdateLight(float newRange, float newAngle, float newIntensity, float transitionTime = 0.5f)
    {
        StopAllCoroutines();
        StartCoroutine(LerpLight(newRange, newAngle, newIntensity, transitionTime));
    }



    void Chase()
    {
        agent.speed = chaseSpeed;
        UpdateLight(chaseLightDistance, chaseLightAngle, ChaseLightIntensity, 1f);
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
        UpdateLight(patrolLightDistance, patrolLightAngle, patrolLightIntensity, 1f); // Lerp transition

        if (agent.remainingDistance < waypointThreshold)
        {
            targetWaypoint = (targetWaypoint + 1) % waypoints.Length;
        }
    }

    public void Scout()
    {

        if (!isScouting)
        {
            isScouting = true;
            UpdateLight(scoutLightDistance, scoutLightAngle, scoutLightIntensity, 1f);
            StartCoroutine(RotateWhileScouting());
        }
    }

    void Detect()
    {
        // Set current detection range and angle based on state
        currentLightDistance = isChasing ? chaseLightDistance : patrolLightDistance;
        currentLightAngle = isChasing ? chaseLightAngle : patrolLightAngle;

        Transform lightOrigin = spotLight.transform;
        isPlayerDetected = false; // Reset detection state

        Collider[] detectedColliders = Physics.OverlapSphere(lightOrigin.position, currentLightDistance, playerLayer);

        foreach (var collider in detectedColliders)
        {
            Vector3 directionToPlayer = (collider.transform.position - lightOrigin.position).normalized;
            float angleToPlayer = Vector3.Angle(lightOrigin.forward, directionToPlayer);

            if (angleToPlayer < currentLightAngle / 2) // Ensure it's inside the light cone
            {
                if (Physics.Raycast(lightOrigin.position, directionToPlayer, out RaycastHit hit, currentLightDistance))
                {
                    if (hit.collider.gameObject == collider.gameObject)
                    {
                        isPlayerDetected = true;
                        break;
                    }
                }
            }
        }
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

    IEnumerator LerpLight(float targetRange, float targetAngle, float targetIntensity, float duration)
    {
        float startRange = spotLight.range;
        float startAngle = spotLight.spotAngle;
        float startIntensity = spotLight.intensity;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / duration; // Normalized time (0 to 1)

            // Lerp between start and target values
            spotLight.range = Mathf.Lerp(startRange, targetRange, t);
            spotLight.spotAngle = Mathf.Lerp(startAngle, targetAngle, t);
            spotLight.intensity = Mathf.Lerp(startIntensity, targetIntensity, t);

            yield return null;
        }

        spotLight.range = targetRange;
        spotLight.spotAngle = targetAngle;
        spotLight.intensity = targetIntensity;
    }

}