using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpItemInHand : MonoBehaviour
{
    [Header("References")]
    public InteractionHandler InteractionHandler;  // Reference to the InteractionHandler
    public Transform equipPos;
    GameObject currentItem;
    GameObject wp;

    [Header("Pick up settings")]
    [SerializeField] private float PickUpDistance;

    [Header("Inventory settings")]
    [SerializeField] private int currentIndex;
    GameObject[] inventory = new GameObject[2];

    [Header("State")]
    public bool canGrab;
    public bool isHolding;

    void Update()
    {
        Interactable interactable = InteractionHandler.GetDetectedInteractable(); // Get the detected interactable
        canGrab = (interactable != null && interactable is PickableItem); // Ensure the interactable is a PickableItem

        if (canGrab && Input.GetKeyDown(KeyCode.E))
        {
            wp = interactable.gameObject;  // Set wp to the detected interactable's GameObject
            SetInInventory(); // Pick up the item
        }

        if (currentItem != null && Input.GetKeyDown(KeyCode.Q))
        {
            Drop(); // Drop the currently held item
        }

        changeSlot(); // Switch between inventory slots when the number keys are pressed
    }

    void SetInInventory()
    {
        if (wp != null)
        {
            Interactable interactable = wp.GetComponent<Interactable>();
            if (interactable != null && interactable is PickableItem)
            {
                if (inventory[currentIndex] == null)
                {
                    inventory[currentIndex] = wp; // Place item in the current slot
                    EquipItem(currentIndex); // Equip the item (if necessary)
                }
                else
                {
                    int otherSlot = (currentIndex == 0) ? 1 : 0;
                    if (inventory[otherSlot] == null)
                    {
                        inventory[otherSlot] = wp; // Place item in the other slot
                        wp.SetActive(false); // Hide the item until equipped
                    }
                }

                wp = null; // Reset wp reference
            }
        }
    }

    void Drop()
    {
        if (currentItem != null)
        {
            currentItem.transform.parent = null;
            currentItem.GetComponent<Rigidbody>().isKinematic = false;

            currentItem = null;
            inventory[currentIndex] = null;
            isHolding = false;
        }
    }

    void EquipItem(int slotIndex)
    {
        if (inventory[slotIndex] != null)
        {
            if (currentItem != null)
            {
                inventory[currentIndex] = currentItem;
                currentItem.SetActive(false); // Hide the currently held item
            }

            currentItem = inventory[slotIndex];
            currentIndex = slotIndex; // Update the active slot

            currentItem.SetActive(true); // Show the newly equipped item
            setInHand(currentIndex, currentItem); // Position the item in hand
        }
        else
        {
            if (currentItem != null)
            {
                currentItem.SetActive(false); // Hide unequipped item
                currentItem = null;
            }
            currentIndex = slotIndex;
        }
    }

    void setInHand(int slotIndex, GameObject itemReference)
    {
        itemReference.SetActive(true); // Ensure the item is visible
        itemReference.transform.position = equipPos.position; // Position the item at the equip position
        itemReference.transform.parent = equipPos; // Parent the item to the equip position
        itemReference.GetComponent<Rigidbody>().isKinematic = true; // Disable physics for the item
        isHolding = true; // Update holding state
        currentItem = itemReference; // Update the currently equipped item
    }

    void changeSlot()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) && currentIndex != 0)
        {
            EquipItem(0); // Switch to the first slot
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2) && currentIndex != 1)
        {
            EquipItem(1); // Switch to the second slot
        }
    }
}
