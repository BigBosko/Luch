using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lock : Interactable
{

    public int lockId;
    public bool isLocked = true;

     
    public override void Interact()
    {
        Debug.Log("Interacting with Lock ID: " + lockId);
        InventoryHandler inventory = FindObjectOfType<InventoryHandler>();
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
