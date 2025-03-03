using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : PickableItem
{
    public int keyId;

    private void Start()
    {
        rotationType = "Forward";
    }

    private void Update()
    {
        if (isHeld)
        {
            Debug.Log("Key is held");
            FaceForward();
        }
    }

    private void FaceForward()
    {
        Vector3 cameraForward = Camera.main.transform.forward;

        cameraForward.y = 0;

        transform.rotation = Quaternion.LookRotation(cameraForward);
    }

}
