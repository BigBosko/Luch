using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipPositionPos : MonoBehaviour
{
    [Header("References")]
    public Transform playerCamera;  // Reference to the camera
    private InventoryHandler inventory;

    [Header("Settings")]
    public Vector3 offset;             // The offset from the camera to the equip position (initially set in the editor)

}
