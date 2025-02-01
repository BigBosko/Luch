using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Keyboard : Interactable
{
    CameraSwitcher cameraSwitcher;

    public override void Interact()
    {
        ToggleCamera();
    }

    private void ToggleCamera()
    {
        cameraSwitcher.SwitchCamera();
    }

}
