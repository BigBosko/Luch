using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : UsableItem
{
    public int keyId;

    private void Update()
    {
        if (isHeld)
        {
            FaceForward();
        }
    }
     public override void Use()
    {
        Debug.Log("Key used");
    }

    private void FaceForward()
    {
        Vector3 cameraForward = Camera.main.transform.forward;

        cameraForward.y = 0;

        transform.rotation = Quaternion.LookRotation(cameraForward);
    }

}
