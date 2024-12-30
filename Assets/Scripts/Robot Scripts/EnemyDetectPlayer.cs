using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDetectPlayer : MonoBehaviour
{
    public float detectionDistance;  // Distance the cone can detect
    public float coneAngle;  // Angle of the cone (degrees)
    public LayerMask playerLayer;  // Only detect player layer

    private Transform lightOrigin;  // The origin of the light (usually at the front of the robot)

    void Start()
    {
        lightOrigin = transform;  // Assume the light origin is the Reflector's position
    }

    void Update()
    {
        DetectPlayerInCone();
    }

    public void DetectPlayerInCone()
    {
        // Find all colliders within a sphere around the light origin
        Collider[] detectedColliders = Physics.OverlapSphere(lightOrigin.position, detectionDistance, playerLayer);

        foreach (var collider in detectedColliders)
        {
            Vector3 directionToPlayer = collider.transform.position - lightOrigin.position;

            // Check if the player is within the cone's angle
            if (Vector3.Angle(lightOrigin.forward, directionToPlayer) < coneAngle / 2)
            {
                // Check if the player is within the cone's range and not obstructed
                RaycastHit hit;
                if (Physics.Raycast(lightOrigin.position, directionToPlayer, out hit, detectionDistance, playerLayer))
                {
                    // Ensure the raycast hits the player object
                    if (hit.collider.gameObject == collider.gameObject)
                    {
                        // Player is detected in the light's cone
                        Debug.Log("Player detected in the light's cone!");
                    }
                }
            }
        }
    }
}
