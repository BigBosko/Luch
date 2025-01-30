using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectionHandler : MonoBehaviour
{
    public Camera playerCamera;
    public LayerMask interactableLayer;
    public float interactionRange = 4f;
    private Interactable detectedInteractable;

    public Interactable GetDetectedInteractable()
    {
        RaycastHit hit;
        Ray ray = playerCamera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, interactionRange, interactableLayer))
        {
            detectedInteractable = hit.transform.GetComponent<Interactable>();
            return detectedInteractable;
        }

        detectedInteractable = null;
        return null;
    }

    public bool IsInteractableDetected()
    {
        return detectedInteractable != null;
    }
}
