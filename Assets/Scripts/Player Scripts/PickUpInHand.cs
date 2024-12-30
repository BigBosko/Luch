using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpItemInHand : MonoBehaviour
{
    [Header("References")]
    public LayerMask itemLayer;
    public Transform equipPos;
    public Camera cam;
    GameObject currentItem;
    GameObject wp;

    [Header("Pick up settings")]
    [SerializeField] private float maxPickUpDistance;

    [Header("Inventory settings")]
    [SerializeField] private int currentIndex;
    GameObject[] inventory = new GameObject[2];

    [Header("State")]
    public bool canGrab;
    public bool isHolding;

    void Update()
    {
        canGrab = detectPickable();

        if (canGrab)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                SetInInventory(); // Pick up item
            }
        }

        if (currentItem != null)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                Drop(); // Drop currently held item
            }
        }

        changeSlot(); // Switch between slots when the number keys are pressed
    }

    bool detectPickable()
    {
        RaycastHit hit;
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, maxPickUpDistance))
        {
            if ((itemLayer.value & (1 << hit.transform.gameObject.layer)) > 0 && hit.transform.gameObject != currentItem)
            {
                wp = hit.transform.gameObject;
                return true;
            }
        }
        return false;
    }

    void SetInInventory()
    {
        if (inventory[currentIndex] == null)
        {
            inventory[currentIndex] = wp; // Place item in current slot
            EquipItem(currentIndex); // Equip item (if necessary)
        }
        else
        {
            int otherSlot = (currentIndex == 0) ? 1 : 0;
            if (inventory[otherSlot] == null)
            {
                inventory[otherSlot] = wp; // Place item in other slot
                wp.SetActive(false); // Hide the item until equipped
            }
        }

        wp = null; // Reset wp reference
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
                currentItem.SetActive(false); // Hide currently held item
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
            EquipItem(0); // Switch to first slot
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2) && currentIndex != 1)
        {
            EquipItem(1); // Switch to second slot
        }
    }
}
