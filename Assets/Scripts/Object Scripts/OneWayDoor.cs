using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneWayDoor : Door
{
    public bool isInTrigger;
    

    protected override void Start()
    {
        base.Start();
    }

    public override void Interact()
    {
        if (isInTrigger)
        {
            Debug.Log("Opening one way door");
            base.Interact();
        }
    }


}
