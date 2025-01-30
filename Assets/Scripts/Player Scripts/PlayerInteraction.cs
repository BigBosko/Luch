using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public DetectionHandler detectionHandler;
    public InventoryHandler inventory;

    void Update()
    {
        Interactable interactable = detectionHandler.GetDetectedInteractable();

        if(interactable != null && Input.GetKeyDown(KeyCode.E))
        {
            interactable.Interact();

        }
    }
}
