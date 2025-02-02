using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickableItem : Interactable
{
    public InventoryHandler inventory;
    public Transform playerCamera;
    public bool isHeld = false;

    public string rotationType;

    private void Start()
    {
        playerCamera = GameObject.Find("Camera Holder").transform;
    }

    public override void Interact()
    {
        inventory = FindObjectOfType<InventoryHandler>();
        inventory.AddToInventory(gameObject);
    }

    private void Update()
    {
        /*if (isHeld)
        {
            FacePlayer();
        }*/
    }

    public void SetItemHeld(bool isHeldStatus)
    {
        isHeld = isHeldStatus;
    }

    /*public void FacePlayer()
    {
        Vector3 direction = playerCamera.position - transform.position;

        direction.y = 0;

        Quaternion lookRotation = Quaternion.LookRotation(direction);

        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, lookRotation.eulerAngles.y, transform.rotation.eulerAngles.z);
    }*/
}

