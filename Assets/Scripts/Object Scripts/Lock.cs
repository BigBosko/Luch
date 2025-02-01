using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lock : Interactable
{
    InventoryHandler inventory;

    public int lockId;
    public bool isLocked = true;

    private void Start()
    {
       inventory = FindObjectOfType<InventoryHandler>();
    }

    public override void Interact()
    {
        Debug.Log("Interacting with Lock ID: " + lockId);
        if (isLocked)
        {
            if (inventory.HasRightKey(lockId))
            {
                isLocked = false;
                Transform door = transform.parent;

                door.Rotate(0, -90, 0);
                Debug.Log("Lock unlocked!");
            }
            else
            {
                Debug.Log("Wrong key or no key");
            }
        }
    }   
}
