using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public virtual void Interact()
    {
        Debug.Log("Interacted with " + gameObject.name);
    }

    public virtual void Use()
    {
        Debug.Log("This item cannot be used directly.");
    }

}

