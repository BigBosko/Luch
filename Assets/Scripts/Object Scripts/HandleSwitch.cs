using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandleSwitch : Interactable
{
    public bool isOn;
    private bool isInitialyOn = false;

    void Start()
    {
        
    }

    public override void Interact()
    {
        
    }

    private void ToggleSwitch()
    {
        isOn = !isOn;
        MoveSwitch();
    }
    private void MoveSwitch()
    {

    }

}
