using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryHandler : MonoBehaviour
{
    [Header("References")]
    public DetectionHandler detectionHandler;
    public Transform equipPos;
    private GameObject currentItem;
    private PickableItem pickableItem;

    [Header("Inventory settings")]
    private int currentIndex;
    public GameObject[] inventorySlots = new GameObject[2];

    [Header("State")]
    public bool isHolding;

    [Header("Ammo")]
    public int empAmmoCount = 0;

    void Update()
    {
        if (currentItem != null && Input.GetKeyDown(KeyCode.Q))
        {
            DropItem();
        }

        if(currentItem != null && Input.GetMouseButtonDown(0))
        {
            UseItem();
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
        if (inventorySlots[slotIndex] == null)
        {
            inventorySlots[slotIndex] = item;
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

            pickableItem = currentItem.GetComponent<PickableItem>();
            if (pickableItem != null)
            {
                pickableItem.SetItemHeld(false);
            }

            currentItem = null;
            inventorySlots[currentIndex] = null;
            isHolding = false;
        }
    }

    private void EquipItem(int slotIndex)
    {
        
        if (currentItem != null)
        {
            currentItem.SetActive(false);
            inventorySlots[currentIndex] = currentItem; // Save the item
        }

        currentItem = inventorySlots[slotIndex];
        currentIndex = slotIndex;
        
        //currentItem.transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, 0f, 0f);
        
        currentItem.SetActive(true);
        SetInHand(currentItem);

        pickableItem = currentItem.GetComponent<PickableItem>();
        if(pickableItem != null)
        {
            pickableItem.SetItemHeld(true);
        }
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

    private void UseItem()
    {
        if (currentItem != null)
        {
            // Check if the current item is of type UsableItem
            UsableItem usableItem = currentItem.GetComponent<UsableItem>();
            if (usableItem != null)
            {
                usableItem.Use();
            }
            else
            {
                Debug.Log("This item cannot be used.");
            }
        }
    }

    public void AddEmpAmmo()
    {
        empAmmoCount += 1;
        Debug.Log("EMP Ammo Count: " + empAmmoCount);
    }

}
