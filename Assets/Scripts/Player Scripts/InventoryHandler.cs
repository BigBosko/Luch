using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryHandler : MonoBehaviour
{
    [Header("References")]
    public DetectionHandler detectionHandler;
    public Transform equipPos;
    private GameObject currentItem;

    [Header("Inventory settings")]
    private int currentIndex;
    private GameObject[] inventory = new GameObject[2];

    [Header("State")]
    public bool isHolding;

    void Update()
    {
        if (currentItem != null && Input.GetKeyDown(KeyCode.Q))
        {
            DropItem();
        }

        HandleSlotSwitch();
    }

    public void AddToInventory(GameObject item)
    {
        // Try to place the item in the current or other slot
        if (TryAddItemToSlot(currentIndex, item) || TryAddItemToSlot(GetOtherSlotIndex(), item))
        {
            return;
        }

        Debug.Log("Inventory is full");
    }

    private bool TryAddItemToSlot(int slotIndex, GameObject item)
    {
        if (inventory[slotIndex] == null)
        {
            inventory[slotIndex] = item;
            if (currentIndex == slotIndex)
            {
                EquipItem(slotIndex);
            }
            else
            {
                item.SetActive(false); // Only deactivate if item is not equipped
            }
            return true;
        }
        return false;
    }

    private int GetOtherSlotIndex() => (currentIndex == 0) ? 1 : 0;

    private void DropItem()
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

    private void EquipItem(int slotIndex)
    {
        if (currentItem != null)
        {
            currentItem.SetActive(false);
            inventory[currentIndex] = currentItem; // Save the item
        }

        currentItem = inventory[slotIndex];
        currentIndex = slotIndex;
        currentItem.SetActive(true);
        SetInHand(currentItem);
    }

    private void SetInHand(GameObject item)
    {
        item.transform.position = equipPos.position;
        item.transform.parent = equipPos;
        item.GetComponent<Rigidbody>().isKinematic = true;
        isHolding = true;
    }

    private void HandleSlotSwitch()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) && currentIndex != 0)
        {
            EquipItem(0);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2) && currentIndex != 1)
        {
            EquipItem(1);
        }
    }
}
