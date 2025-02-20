using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightController : MonoBehaviour
{
    public bool isOn = true;
    public Light lightSource;

    private void Start()
    {
        if (lightSource == null)
        {
            lightSource = GetComponent<Light>();
        }
    }

    public void ToggleLight()
    {
        isOn = !isOn;
        if (lightSource != null)
        {
            lightSource.enabled = isOn;
        }
    }
}

