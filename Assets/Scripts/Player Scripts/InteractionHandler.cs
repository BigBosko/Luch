using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionHandler : MonoBehaviour
{
    public Camera cam;
    public LayerMask interactableLayer;
    public float interactionDistance = 3f;
    private Interactable detectedInteractable;

    // This method gets the currently detected interactable object
    public Interactable GetDetectedInteractable()
    {
        RaycastHit hit;
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, interactionDistance, interactableLayer))
        {
            detectedInteractable = hit.transform.GetComponent<Interactable>();
            if (detectedInteractable != null)
            {
                return detectedInteractable;
            }
        }
        detectedInteractable = null;
        return null;
    }

    // This method checks if there is any interactable detected
    public bool IsInteractableDetected()
    {
        return detectedInteractable != null;
    }
}
