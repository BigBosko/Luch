using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lock : Interactable
{
    public int lockId;


    public override void Interact()
    {
        InventoryHandler inventory = FindObjectOfType<InventoryHandler>();
        if (inventory.HasRightKey(lockId))
        {
            Debug.Log("Lock unlocked!");
            Destroy(gameObject);
        }
        else
        {
            Debug.Log("Wrong key or no key");
        }
    }   
}
