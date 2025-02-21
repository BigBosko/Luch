using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticLight : MonoBehaviour
{
    [Header("References")]
    public LayerMask playerLayer;
    protected CapsuleCollider detectionCollider;
    public Light lightSource;

    [Header("States")]
    protected bool isLightOn;
    protected bool isPlayerInZone = false;
    protected virtual void Start()
    {
        detectionCollider = GetComponent<CapsuleCollider>();

        if (detectionCollider == null)
        {
            Debug.LogError("No CapsuleCollider found on detectionZone!");
        }

        detectionCollider.isTrigger = true;
    }

    protected virtual void Update()
    {
        LightInterval();
    }

    protected void OnTriggerEnter(Collider triggerObject)
    {
        if (((1 << triggerObject.gameObject.layer) & playerLayer) != 0)
        {
            isPlayerInZone = true;
        }
    }

    protected void OnTriggerExit(Collider triggerObject)
    {

        if (((1 << triggerObject.gameObject.layer) & playerLayer) != 0)
        {
            isPlayerInZone = false;
        }

    }

    protected void LightInterval()
    {
        SetLightState(true);
    }

    public void TogglleLightState()
    {
        isLightOn = !isLightOn;
        lightSource.enabled = isLightOn;
    }

    public void SetLightState(bool state)
    {
        isLightOn = state;
        if (lightSource != null)
        {
            lightSource.enabled = state;
        }
    }

    public bool GetLightStatus()
    {
        return isLightOn;
    }

    protected virtual void DetectPlayer()
    { 
    }
}
