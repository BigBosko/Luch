using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Keyboard : Interactable
{
    private CameraSwitcher cameraSwitcher;

    void Start()
    {
        cameraSwitcher = GameObject.FindObjectOfType<CameraSwitcher>();
    }
    public override void Interact()
    {
        Debug.Log("Interacted with keyboard.");
        ToggleCamera();
    }

    private void ToggleCamera()
    {
        cameraSwitcher.SwitchCamera();
    }

}
