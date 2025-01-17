using UnityEngine;

public class Crouch : MonoBehaviour
{
    [Header("References")]
    private CapsuleCollider playerCollider;

    [Header("Crouch Settings")]
    public float crouchSpeed = 5f;
    private float normalHeight = 1f;
    private float normalColliderHeight;
    private float crouchColliderHeight;
    private float crouchHeight;


    private Vector3 normalScale; //se dinamicno nastavi v start();

    public bool IsCrouching { get; private set; }

    private bool isTransitioning;
    public float CrouchSpeed => crouchSpeed;

    private void Start()
    {
        playerCollider = GetComponent<CapsuleCollider>();

        normalScale = transform.localScale;
        crouchHeight = normalHeight * 0.6f;
        normalColliderHeight = normalHeight * 2f;
        crouchColliderHeight = crouchHeight * 3.4f;

        Debug.Log("crouchColliderHeight: " + crouchColliderHeight);
        Debug.Log("crouch height: " + crouchHeight);

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
        float targetColliderHeight = IsCrouching ? crouchColliderHeight : normalColliderHeight;
        float targetHeight = IsCrouching ? crouchHeight : normalHeight;
        transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(normalScale.x, targetHeight, normalScale.z), crouchSpeed * Time.deltaTime);
        playerCollider.height = Mathf.Lerp(playerCollider.height, targetColliderHeight, crouchSpeed * Time.deltaTime);

        if (Mathf.Abs(transform.localScale.y - targetHeight) < 0.01f &&
            Mathf.Abs(playerCollider.height - targetColliderHeight) < 0.01f)
        {
            isTransitioning = false;
        }
    }

    private bool CanUncrouch()
    {

        float checkHeight = normalHeight + crouchHeight/6f + 0.02f;
        Vector3 rayOrigin = transform.position + Vector3.up * (crouchHeight / 2f);

        Debug.DrawRay(rayOrigin, Vector3.up * checkHeight, Color.red, 2f);
        if (Physics.Raycast(rayOrigin, Vector3.up, checkHeight))
        {
            return false;
        }
        return true;
    }
}



