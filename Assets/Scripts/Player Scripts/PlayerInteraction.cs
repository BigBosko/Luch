using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField] private DetectionHandler detectionHandler;
    [SerializeField] private Canvas pressE;

    private void Start()
    {
        pressE.enabled = false;
    }

    void Update()
    {
        Interactable interactable = detectionHandler.GetDetectedInteractable();

        if(interactable != null)
        {
            pressE.enabled = true;

            if (Input.GetKeyDown(KeyCode.E))
            {
                interactable.Interact();
            }
        }

        else
        {
            pressE.enabled = false;
        }
    }
}
