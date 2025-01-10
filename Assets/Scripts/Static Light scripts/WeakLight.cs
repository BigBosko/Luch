using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeakLight : MonoBehaviour
{
    [Header("References")]
    public LayerMask playerLayer;
    private CapsuleCollider detectionCollider;
    public Light lightSource;
    public Transform detectionZone;

    [Header("States")]
    private bool isLightOn = false;
    private bool isPlayerInZone = false;


    void Start()
    {
        detectionCollider = detectionZone.GetComponent<CapsuleCollider>();
        detectionCollider.isTrigger = true;

        if (detectionCollider == null)
        {
            Debug.LogError("No CapsuleCollider found on detectionZone!");
        }

        SetLightState(false);
    }

    void Update()
    {
        LightInterval();
        DetectPlayer();
        Debug.Log("IsPlayerInZone= " + isPlayerInZone);
    }

    void OnTriggerEnter(Collider triggerObject)
    {
        if (((1 << triggerObject.gameObject.layer) & playerLayer) != 0)
        {
            isPlayerInZone = true;
        }
    }

    private void OnTriggerExit(Collider triggerObject)
    {
        if (((1 << triggerObject.gameObject.layer) & playerLayer) != 0)
        {
            isPlayerInZone = false;
        }
    }

    private void DetectPlayer()
    {
        if(isPlayerInZone && isLightOn)
        {
            NotifyRobot();
        }
    }

    private void LightInterval() //bolj zakompliciraj
    {
        if (Time.time % 4 < 2)
        {
            if (!isLightOn) SetLightState(true);
        }
        else
        {
            if (isLightOn) SetLightState(false);
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
}
