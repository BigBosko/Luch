using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectionHandler : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Camera playerCamera;
    [SerializeField] private LayerMask interactableLayer;
    [SerializeField] private float interactionRange;

    private Interactable detectedInteractable;

    public Interactable GetDetectedInteractable()
    {
        RaycastHit hit;
        Ray ray = playerCamera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, interactionRange, interactableLayer))
        {
            detectedInteractable = hit.transform.GetComponent<Interactable>();
            Debug.Log(detectedInteractable.name);
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
