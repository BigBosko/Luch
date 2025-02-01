using UnityEngine;

public class Crouch : MonoBehaviour
{
    [Header("References")]
    public Transform playerObject;

    [Header("Crouch Settings")]
    public float crouchSpeed = 5f;
    private float normalHeight = 1f; //parent object
    private float crouchHeight;
    private float playerObjectNormalHeight = 1.8f;
    private float playerObjectCrouchHeight;
    public Transform equipPos;


    private Vector3 normalScale; //se dinamicno nastavi v start();

    public bool IsCrouching { get; private set; }

    private bool isTransitioning;
    public float CrouchSpeed => crouchSpeed;

    private void Start()
    {
        normalScale = transform.localScale;
        crouchHeight = normalHeight * 0.6f;

        playerObjectCrouchHeight = playerObjectNormalHeight * 0.6f;

        IsCrouching = false;
        isTransitioning = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C) && !isTransitioning)
        {
            if (IsCrouching)
            {
                if (CanUncrouch())
                {
                    ToggleCrouch();
                }
                else
                {
                    Debug.Log("You can't stand up, there is an obstacle above you");
                }
            }
            else ToggleCrouch();
        }
        UpdateHeight();
    }

    private void ToggleCrouch()
    {
        IsCrouching = !IsCrouching;
        isTransitioning = true;
    }

    private void UpdateHeight()
    {
        float targetHeight = IsCrouching ? crouchHeight : normalHeight;
        float targetObjectHeight = IsCrouching ? playerObjectCrouchHeight : playerObjectNormalHeight;
        transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(normalScale.x, targetHeight, normalScale.z), crouchSpeed * Time.deltaTime);


        if (Mathf.Abs(transform.localScale.y - targetHeight) < 0.01f)
        {
            isTransitioning = false;
        }
    }

    private bool CanUncrouch()
    {

        float buffer = 0.05f;
        float checkHeight = playerObjectCrouchHeight/2f + playerObjectNormalHeight + buffer;
        Vector3 rayOrigin = playerObject.position;

        Debug.DrawRay(rayOrigin, Vector3.up * checkHeight, Color.red, 2f);
        if (Physics.Raycast(rayOrigin, Vector3.up, checkHeight))
        {
            return false;
        }
        return true;
    }
}