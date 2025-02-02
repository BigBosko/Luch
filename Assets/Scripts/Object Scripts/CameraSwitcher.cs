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
            currentCameraIndex = (currentCameraIndex + 1) % cameras.Length; // Loop through cameras

            // Force update the material texture
            quadMaterial.SetTexture("_EmissionMap", cameras[currentCameraIndex].targetTexture);
            quadMaterial.SetTexture("_MainTex", cameras[currentCameraIndex].targetTexture);

            // Enable emission if not active
            quadMaterial.EnableKeyword("_EMISSION");

    }


}
