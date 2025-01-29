using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickableItem : Interactable
{
    public override void Interact()
    {
        InventoryHandler inventory = FindObjectOfType<InventoryHandler>();

        if (inventory != null)
        {
            inventory.AddToInventory(gameObject);
            Debug.Log("Picked up: " + gameObject.name);
        }
    }
    public override void Use()
    {
        //koda ko je predmet uporabljen
    }
}
