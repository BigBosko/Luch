using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeakLight : MonoBehaviour
{
    [Header("References")]
    public LayerMask playerLayer;
    private CapsuleCollider detectionCollider;
    public Light lightSource; // Reference to the child light

    [Header("States")]
    public bool isLightOn = false;
    public bool isDetected = false;

    void Start()
    {
        // Find the CapsuleCollider in the child object
        detectionCollider = GetComponentInChildren<CapsuleCollider>();

        detectionCollider.isTrigger = true;

        // Find the light component
        lightSource = GetComponentInChildren<Light>();

        ToggleLight(false);
    }

    void Update()
    {
        // Example: Toggle the light every 2 seconds
        if (Time.time % 4 < 2)
        {
            if (!isLightOn) ToggleLight(true);
        }
        else
        {
            if (isLightOn) ToggleLight(false);
        }
    }

    private void OnTriggerEnter(Collider triggerObject)
    {
        if (((1 << triggerObject.gameObject.layer) & playerLayer) != 0 && isLightOn) //binary check layerja
        {
            NotifyRobot();
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (((1 << other.gameObject.layer) & playerLayer) != 0)
        {
            isDetected = false;
        }
    }

    private void NotifyRobot()
    {
        Debug.Log("Player detected in light! Notify the robot.");
        // Add logic to send the player's position to the robot
    }

    private void ToggleLight(bool state)
    {
        isLightOn = state;
        if (lightSource != null)
        {
            lightSource.enabled = state;
        }

        Debug.Log(state ? "Light turned on" : "Light turned off");
    }
}
