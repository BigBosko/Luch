using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticLight : MonoBehaviour
{
    [Header("References")]
    public LayerMask playerLayer;
    private CapsuleCollider detectionCollider;
    public Light lightSource;

    [Header("States")]
    private bool isLightOn = false;
    private bool isPlayerInZone = false;

    void Start()
    {
        detectionCollider = GetComponent<CapsuleCollider>();

        if (detectionCollider == null)
        {
            Debug.LogError("No CapsuleCollider found on detectionZone!");
        }

        detectionCollider.isTrigger = true;
        SetLightState(false);
    }


    void Update()
    {
        LightInterval();
        DetectPlayer();
        Debug.Log("IsPlayerInZone= " + isPlayerInZone + " | Light is " + (isLightOn ? "ON" : "OFF"));
    }

    void OnTriggerEnter(Collider triggerObject)
    {
        if (((1 << triggerObject.gameObject.layer) & playerLayer) != 0)
        {
            Debug.Log("Player detected in the detection zone!");
            isPlayerInZone = true;
        }
        else
        {
            Debug.Log("Non-player object detected.");
        }
    }

    private void OnTriggerExit(Collider triggerObject)
    {
        Debug.Log("Player left the detection zone.");

        if (((1 << triggerObject.gameObject.layer) & playerLayer) != 0)
        {
            // When the player exits the detection zone
            Debug.Log("Player left the detection zone.");
            isPlayerInZone = false;
        }
        else
        {
            Debug.Log("Non-player object left the detection zone.");
        }
    }

    private void DetectPlayer()
    {
        if (isPlayerInZone && isLightOn)
        {
            if (gameObject.CompareTag("WeakLight"))
            {
                NotifyRobot();
            }
            else if (gameObject.CompareTag("StrongLight"))
            {
                KillPlayer();
            }
        }
    }

    private void LightInterval()
    {
        float intervalTime = Time.time % 4;

        if (intervalTime < 2)
        {
            if (!isLightOn)
            {
                SetLightState(true);
                Debug.Log("Light turned ON.");
            }
        }
        else
        {
            if (isLightOn)
            {
                SetLightState(false);
                Debug.Log("Light turned OFF.");
            }
        }
    }

    private void NotifyRobot()
    {
        Debug.Log("Player detected in light! Notify the robot.");
    }

    private void SetLightState(bool state)
    {
        isLightOn = state;
        if (lightSource != null)
        {
            lightSource.enabled = state;
        }

        Debug.Log(state ? "Light turned on" : "Light turned off");
    }

    private void KillPlayer()
    {
        //kill logic
        Debug.Log("Player killed because of strong light");
    }
}
