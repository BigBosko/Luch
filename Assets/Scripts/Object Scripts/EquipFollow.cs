using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipFollow : MonoBehaviour
{
    public Transform equipPos;

    void LateUpdate()
    {
        if (equipPos != null)
        {
            transform.position = equipPos.position;
            transform.rotation = equipPos.rotation;
        }
    }
}
