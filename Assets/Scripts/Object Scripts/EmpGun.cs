using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmpGun : UsableItem
{
    [Header("References")]
    public Transform firePoint;
    public GameObject empProjectile;
    public float projectileSpeed;
    protected override void Start()
    {
        inventory = FindObjectOfType<InventoryHandler>();
        if (inventory == null)
        {
            Debug.LogError("InventoryHandler not found! Ensure it exists in the scene.");
        }
        rotationType = "Forward";
    }



    public override void Use()
    {
        if (inventory.empAmmoCount > 0)
        {
            FireEmp();
            inventory.empAmmoCount--;
        }
        else
        {
            Debug.Log("No EMP ammo!");
        }
    }

    private void FireEmp()
    {
        GameObject emoProjectile = Instantiate(empProjectile, firePoint.position, firePoint.rotation);
        Rigidbody rb = emoProjectile.GetComponent<Rigidbody>();

        rb.velocity = firePoint.forward * projectileSpeed;


    }

}
