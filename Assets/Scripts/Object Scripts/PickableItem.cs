using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickableItem : Interactable
{
    protected InventoryHandler inventory;
    public Transform playerCamera;
    public bool isHeld = false;

    public string rotationType;

     protected virtual void Start()
     {
        playerCamera = GameObject.Find("Camera Holder").transform;
        inventory = FindObjectOfType<InventoryHandler>();
     }

    public override void Interact()
    {
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

