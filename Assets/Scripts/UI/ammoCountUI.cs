using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ammoUIcount : MonoBehaviour
{
   [Header("Settings")]
   private int startCount= 0;
    

    [Header("References")]
    private InventoryHandler inventory;
    [SerializeField] private GameObject empGun;
    [SerializeField] private TextMeshProUGUI ammoText;

    [SerializeField] private string displayCountS;

    private void Start()
    {
        displayCountS = startCount.ToString();
        inventory = FindObjectOfType<InventoryHandler>();

    }

    void Update()
    {
        if (inventory.currentItem == empGun)
        {
            displayCountS = inventory.empAmmoCount.ToString();
            ammoText.text = "" + displayCountS;
            ammoText.enabled = true; // Show text when EMP gun is in hand
        }
        else
        {
            ammoText.enabled = false;
        }
    }
}
