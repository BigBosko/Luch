using System.Collections;
using System.Collections.Generic;
using UnityEditor.UI;
using UnityEngine;

public class HandleSwitch : Interactable
{
    private bool isOn;
    [SerializeField] private bool isInitialyOn = false;
    private float offXRot = 128;
    private float onXRot = 5;
    [SerializeField] private GameObject controlledLight;

    void Start()
    {
        if (isInitialyOn)
        {
            isOn = true;
            transform.localRotation = Quaternion.Euler(onXRot, 0, 0);
            controlledLight.GetComponent<StaticLight>().SetLightState(isOn);
        }
        else
        {
            isOn = false;
            transform.localRotation = Quaternion.Euler(offXRot, 0, 0);
        }
    }
    public override void Interact()
    {
        Debug.Log("Interacted with BIGSWITCH");
        ToggleSwitch();
        Action();

    }

    private void ToggleSwitch()
    {
        isOn = !isOn;
        MoveSwitch();
    }
    private void MoveSwitch()
    {
        if (isOn)
        {
                transform.localRotation = Quaternion.Euler(onXRot, 0, 0);
            }
            else
        {
            transform.localRotation = Quaternion.Euler(offXRot, 0, 0);
        }
    }
    
    private void Action()
    {   
        if (controlledLight != null)
        {
            if (controlledLight.GetComponent<StaticLight>() != null)
            {
                controlledLight.GetComponent<StaticLight>().TogglleLightState();
            }
        }
    }
}
