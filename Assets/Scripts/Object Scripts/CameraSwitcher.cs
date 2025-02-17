using UnityEngine;

public class CameraSwitcher : MonoBehaviour
{
    public Camera[] cameras;  // Array of cameras
    public RenderTexture renderTexture; // Single Render Texture
    private int currentCameraIndex = 0;
    private Material quadMaterial;  // Material from the Quad

    private void Start()
    {
        // Get the Quad's material from its MeshRenderer
        quadMaterial = GetComponent<Renderer>().material;

        if (cameras.Length > 0)
        {
            // Assign the same RenderTexture to all cameras
            foreach (Camera cam in cameras)
            {
                cam.targetTexture = renderTexture;
                cam.enabled = false;  // Disable all cameras initially
            }

            // Enable the first camera
            cameras[currentCameraIndex].enabled = true;

            // Assign the RenderTexture to the material
            quadMaterial.mainTexture = renderTexture;
        }
    }

    public void SwitchCamera()
    {
        // Disable current camera
        cameras[currentCameraIndex].enabled = false;

        // Switch to the next camera
        currentCameraIndex = (currentCameraIndex + 1) % cameras.Length;

        // Enable the new active camera
        cameras[currentCameraIndex].enabled = true;
    }
}
