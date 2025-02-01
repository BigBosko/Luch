using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : Interactable
{
    private Lock lockComp;

    private bool isOpen = false;
    private float interactRotation = 90f;

    private void Start()
    {
        lockComp = GetComponentInChildren<Lock>();

        if(lockComp == null)
        {
            Debug.LogError("No Lock component found on " + gameObject.name);
        }
    }

    public override void Interact()
    {
        if (lockComp.isLocked == true)
        {
            Debug.Log("The door is locked.");
        }
        else
        {
            if (isOpen)
            {
                transform.Rotate(0, -interactRotation, 0);
                isOpen = false;
            }
            else
            {
                transform.Rotate(0, interactRotation, 0);
                isOpen = true;

            }
        }
    }
}
