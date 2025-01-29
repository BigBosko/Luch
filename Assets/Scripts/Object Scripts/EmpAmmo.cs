using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmpAmmo : PickableItem
{
    public override void Interact()
    {
        InventoryHandler inventory = FindObjectOfType<InventoryHandler>();

        if (inventory != null)
        {
            inventory.AddEmpAmmo();
            Destroy(gameObject);
        }
    }

}
