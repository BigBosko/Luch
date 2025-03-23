using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NightGoggles : Interactable
{
    [Header("References")]
    private InventoryHandler inventory;
    [SerializeField] private Light playerLight;

    public override void Interact()
    {
        inventory = FindObjectOfType<InventoryHandler>();

        if (inventory != null)
        {
            inventory.hasGoggles = true;
            Destroy(gameObject);

            playerLight.range = 8;

        }
    }
}
