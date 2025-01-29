using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmpGun : UsableItem
{
    [Header("References")]
    public Transform firePoint;
    public GameObject empProjectile;
    public float projectileSpeed;
    private InventoryHandler inventory;

    void Start()
    {
        GameObject player = GameObject.Find("Player");
        inventory = player.GetComponent<InventoryHandler>();
    }

    private void Update()
    {
        if (isHeld)
        {
            FaceForward();
        }
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

    private void FaceForward()
    {
        Vector3 cameraForward = Camera.main.transform.forward;

        cameraForward.y = 0;

        transform.rotation = Quaternion.LookRotation(cameraForward);
    }
}
