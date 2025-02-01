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
        Debug.Log("Switching Camera... Current Camera Index: " + currentCameraIndex);

        if (cameras.Length > 0)
        {
            // Cycle to the next camera
            currentCameraIndex = (currentCameraIndex + 1) % cameras.Length;

            // Get the new RenderTexture
            RenderTexture newTexture = cameras[currentCameraIndex].targetTexture;

            // Update the material texture
            quadMaterial.SetTexture("_MainTex", newTexture); // Force update the main texture

            // Debug info to check
            Debug.Log("Switched to Camera: " + cameras[currentCameraIndex].name);
            Debug.Log("New RenderTexture: " + newTexture.name);
        }
    }


}
