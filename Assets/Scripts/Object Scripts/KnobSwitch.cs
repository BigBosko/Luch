using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnobSwitch : Interactable
{
    [Header("References")]
    private Transform knobPos;

    private float onXPos = -0.07f;
    private float offXPos = 0.07f;
    [SerializeField] private bool isInitialyOn;
    private bool isOn;
    [SerializeField] private GameObject[] controlledDoors;

    void Start()
    {
        knobPos = transform.GetChild(0);

        if (isInitialyOn)
        {
            knobPos.localPosition = new Vector3(onXPos, knobPos.localPosition.y, knobPos.localPosition.z);
            isOn = true;
        }
        else
        {
            knobPos.localPosition = new Vector3(offXPos, knobPos.localPosition.y, knobPos.localPosition.z);
            isOn = false;
        }
    }

    public override void Interact()
    {
        ToggleSwitch();
    }

    private void ToggleSwitch()
    {
        isOn = !isOn;
        MoveKnob();
        Action();
    }

    private void MoveKnob()
    {
        if (isOn)
        {
            knobPos.localPosition = new Vector3(offXPos, knobPos.localPosition.y, knobPos.localPosition.z);
        }
        else
        {
            knobPos.localPosition = new Vector3(onXPos, knobPos.localPosition.y, knobPos.localPosition.z);
        }
    }

    private void ToggleDoor(GameObject door)
    {
        door.SetActive(!door.activeSelf);  // Toggle the active state of the door
    }

    private void Action()
    {
        foreach (var door in controlledDoors)
        {
            ToggleDoor(door);  // Toggle each door in the array
        }
    }
}
