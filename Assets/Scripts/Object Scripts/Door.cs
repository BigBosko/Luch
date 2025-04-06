using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Door : Interactable
{
    private Lock lockComp;

    [SerializeField] private bool isInitiallyOpen = false;
    [SerializeField] private Vector3 openRotation = new Vector3(0, 90, 0);
    [SerializeField] private Vector3 closedRotation = Vector3.zero;
    private bool isOpen;

    protected virtual void Start()
    {
        lockComp = GetComponentInChildren<Lock>();

        isOpen = isInitiallyOpen;
        transform.localRotation = Quaternion.Euler(isOpen ? openRotation : closedRotation);
    }

    public override void Interact()
    {
        if (lockComp == null)
        {
            Debug.Log("Door has no lock");
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
        isOpen = !isOpen;
        transform.localRotation = Quaternion.Euler(isOpen ? openRotation : closedRotation);
    }
}
