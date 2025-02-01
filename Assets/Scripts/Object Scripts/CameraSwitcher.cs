using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitcher : MonoBehaviour
{
    public Material quadMaterial;
    public Camera[] cameras;
    private int currentCameraIndex = 0;


    private void Start()
    {
        if (cameras.Length > 0)
        {
            quadMaterial.mainTexture = cameras[currentCameraIndex].targetTexture;
        }
    }

    public void SwitchCamera()
    {
        if (currentCameraIndex < cameras.Length - 1)
        {
            currentCameraIndex++;
            quadMaterial.mainTexture = cameras[currentCameraIndex].targetTexture;
        }
        else
        {
            currentCameraIndex = 0;
        }
    }
}
