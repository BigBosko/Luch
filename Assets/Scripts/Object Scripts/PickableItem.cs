using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickableItem : Interactable
{
    public virtual void Interact()
    {
        base.Interact();
        Debug.Log("Item picked up!");

    }
}
