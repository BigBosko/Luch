using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lock : Interactable
{
    InventoryHandler inventory;

    public int lockId;
    private bool isLocked;
    private Door door;

    private void Start()
    {
       inventory = FindObjectOfType<InventoryHandler>();
       door =  GetComponentInParent<Door>();
       isLocked = true;
    }

    public override void Interact()
    {
        Debug.Log("Interacting with Lock ID: " + lockId);
        if (isLocked)
        {
            if (inventory.HasRightKey(lockId))
            {
                isLocked = false;
                door.ToggleDoor();
            }
            else
            {
                Debug.Log("Wrong key or no key");
            }
        }
    }

    public bool isLockLocked()
    {
        return isLocked;
    }
}
