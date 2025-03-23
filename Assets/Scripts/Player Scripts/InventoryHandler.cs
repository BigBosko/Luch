using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryHandler : MonoBehaviour
{
    [Header("References")]
    public Transform equipPos;
    public GameObject currentItem;
    private PickableItem pickableItem;

    [Header("Inventory settings")]
    private int currentIndex;
    public GameObject[] inventorySlots = new GameObject[2];

    [Header("State")]
    public bool isHolding;

    [Header("Non-physical inventory")]
    public int empAmmoCount = 0;
    public bool hasGoggles = false;

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
            currentItem.GetComponent<Collider>().enabled = true;
            currentItem.GetComponent<Rigidbody>().isKinematic = false;

            EquipFollow followScript = currentItem.GetComponent<EquipFollow>();
            if (followScript != null)
            {
                Destroy(followScript);
            }

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
        currentItem = item;

        EquipFollow followScript = currentItem.GetComponent<EquipFollow>();
        if (followScript == null)
        {
            followScript = currentItem.AddComponent<EquipFollow>();
        }
        followScript.equipPos = equipPos;

        currentItem.GetComponent<Rigidbody>().isKinematic = true;

        currentItem.GetComponent<Collider>().enabled = false;

        isHolding = true;
    }


    private void HandleSlotSwitch()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) && currentIndex != 0 && inventorySlots[0] != null)
        {
            EquipItem(0);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2) && currentIndex != 1 && inventorySlots[1] != null)
        {
            EquipItem(1);
        }
    }

    private void UseItem()
    {
        if (currentItem != null)
        {
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

    public bool HasRightKey(int lockId)
    {
        Debug.Log("Checking key in hand...");

        if (currentItem == null)
        {
            Debug.Log("No item in hand!");
            return false;
        }

        Key key = currentItem.GetComponent<Key>();
        if (key == null)
        {
            Debug.Log("Item is not a key!");
            return false;
        }

        Debug.Log("Key ID: " + key.keyId + ", Lock ID: " + lockId);
        return key.keyId == lockId;
    }
}
