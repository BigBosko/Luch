using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipPosition : MonoBehaviour
{
    [Header("References")]
    public Transform cameraTransform;  // Reference to the camera
    public Transform equipPos;         // Reference to where the item is equipped

    [Header("Settings")]
    public Vector3 offset;             // The offset from the camera to the equip position (initially set in the editor)

    void Update()
    {
        // Update the equip position relative to the camera, keeping the same horizontal distance
        Vector3 targetPosition = cameraTransform.position + cameraTransform.forward * offset.z;

        // Keep the equip position's X and Z the same, but change the Y to follow the camera's vertical movement
        targetPosition.y = cameraTransform.position.y + offset.y;

        equipPos.position = targetPosition;
    }
}
