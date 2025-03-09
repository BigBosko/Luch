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

    [Header("Flickering Settings")]
    [SerializeField] private bool isFlickering;
    [SerializeField] private float flickerTimes = 3;
    [SerializeField] private float flickerInterval = 0.1f;
    [SerializeField] private float waitTimeAfterFlicker = 3f;

    protected virtual void Start()
    {
        detectionCollider = GetComponent<CapsuleCollider>();

        if (detectionCollider == null)
        {
            Debug.LogError("No CapsuleCollider found on detectionZone!");
        }
        else
        {
            detectionCollider.isTrigger = true;
        }

        // Start flickering if enabled.
        if (isFlickering)
        {
            StartCoroutine(FlickerRoutine());
        }
        else
        {
            // Otherwise, ensure the light stays on.
            SetLightState(true);
        }
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

    public void TogglleLightState()
    {
        isLightOn = !isLightOn;
        if (lightSource != null)
        {
            lightSource.enabled = isLightOn;
        }
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
        // Can be overridden in derived classes.
    }

    private IEnumerator FlickerRoutine()
    {
        while (true)
        {
            Debug.Log("Starting flicker cycle");
            // Ensure the light is on at the start of the cycle.
            SetLightState(true);

            // Flicker off and on for the specified number of times.
            for (int i = 0; i < flickerTimes; i++)
            {
                Debug.Log("Flicker " + i + ": Turning off");
                SetLightState(false);
                yield return new WaitForSeconds(flickerInterval);

                Debug.Log("Flicker " + i + ": Turning on");
                SetLightState(true);
                yield return new WaitForSeconds(flickerInterval);
            }

            Debug.Log("Waiting for " + waitTimeAfterFlicker + " seconds");
            yield return new WaitForSeconds(waitTimeAfterFlicker);
        }
    }
}
