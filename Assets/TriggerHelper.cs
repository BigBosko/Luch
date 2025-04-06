using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerHelper : MonoBehaviour
{
    [SerializeField] private GameObject oneWayDoor;
    [SerializeField] private LayerMask playerLayer;
    private OneWayDoor oneWayDoorScript;

    private void Start()
    {
        oneWayDoorScript = oneWayDoor.GetComponent<OneWayDoor>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (((1 << other.gameObject.layer) & playerLayer) != 0) //binary check layerja
        {
            oneWayDoorScript.isInTrigger = true;
            Debug.Log("Is in the trigger");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (((1 << other.gameObject.layer) & playerLayer) != 0) //binary check layerja
        {
            oneWayDoorScript.isInTrigger = false;
            Debug.Log("Left the trigger");
        }
    }
}
