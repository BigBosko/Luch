using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Experimental.GlobalIllumination;


public class EnemyAI : MonoBehaviour
{
    [Header("Audio")]
    public AudioClip patrolAudio;
    public AudioClip chaseAudio;
    private AudioSource audioSource;

    [Header("Detection")]
    public LayerMask playerLayer;
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
        agent.isStopped = true;
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
        audioSource.loop = true;
    }


    void Update()
    {
        if (isPlayerDetected)
        {
            isChasing = true;
            lastPosition = player.position;
            Chase();
        }
        else if (isChasing)
        {
            Chase();
        }
        else if (isScouting)
        {
            //nc
        }
        else
        {
            Patrol();
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
        // Existing chase logic
        agent.speed = chaseSpeed;
        UpdateLight(chaseLightDistance, chaseLightAngle, ChaseLightIntensity, 1f);
        agent.destination = lastPosition;

        // Play chase audio if not already playing
        if (audioSource.clip != chaseAudio || !audioSource.isPlaying)
        {
            audioSource.clip = chaseAudio;
            audioSource.Play();
        }

        // Existing logic to transition to scouting
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
        // Existing patrol logic
        agent.speed = patrolSpeed;
        agent.destination = waypoints[targetWaypoint].position;
        UpdateLight(patrolLightDistance, patrolLightAngle, patrolLightIntensity, 1f);

        // Play patrol audio if not already playing
        if (audioSource.clip != patrolAudio || !audioSource.isPlaying)
        {
            audioSource.clip = patrolAudio;
            audioSource.Play();
        }

        // Existing waypoint logic
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
            agent.isStopped = true; // Stop the agent from moving
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
        float totalRotation = 0f;
        float rotationSpeed = 360f / scoutDuration; // Degrees per second
        agent.updateRotation = false; // Disable agent's automatic rotation

        while (totalRotation < 360f)
        {
            Detect();
            float rotationThisFrame = rotationSpeed * Time.deltaTime;
            transform.Rotate(0, rotationThisFrame, 0);
            totalRotation += rotationThisFrame;
            yield return null;
        }

        // Ensure the final rotation is precise
        transform.rotation = Quaternion.Euler(0, transform.eulerAngles.y, 0);

        // Re-enable agent control
        agent.updateRotation = true;
        isScouting = false;
        agent.isStopped = false;
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