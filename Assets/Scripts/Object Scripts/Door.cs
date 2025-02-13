using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Door : Interactable
{
    private Lock lockComp;
    
    [SerializeField ]private bool isOpen;
    private float interactRotation = 90f;

    private void Start()
    {
        lockComp = GetComponentInChildren<Lock>();
        if (transform.localRotation == quaternion.identity)
        {
            isOpen = false;
        }
        else isOpen = true;

    }

    public override void Interact()
    {
        if (lockComp == null)
        {
            Debug.Log("Door has nos lock");
            ToggleDoor();
        }
        else
        {
            if (lockComp.isLockLocked())
            {
                Debug.Log("Door is locked");
            }
            else
            {
                ToggleDoor();
            }
        }
    }

    public void ToggleDoor()
    {
        if (isOpen)
        {
            transform.localRotation = quaternion.identity;
            isOpen = false;
        }
        else
        {
            transform.Rotate(0, -interactRotation, 0);
            isOpen = true;
        }
    }
}
