using UnityEngine;

public class Crouch : MonoBehaviour
{
    [Header("References")]
    private CapsuleCollider playerCollider;
    private Rigidbody rb;

    [Header("Crouch Settings")]
    public float crouchSpeed = 5f;
    private float normalHeight = 1f;
    private float normalColliderHeight;
    private float crouchColliderHeight;
    private float crouchHeight;


    private Vector3 normalScale; //se dinamicno nastavi v start();
    private Vector3 crouchScale; //se dinamicno nastavi v start();

    public bool IsCrouching { get; private set; }
    public float CrouchSpeed => crouchSpeed;

    private void Start()
    {
        playerCollider = GetComponent<CapsuleCollider>();
        rb = GetComponent<Rigidbody>();

        normalScale = transform.localScale;
        crouchScale = new Vector3(normalScale.x, normalScale.y * 0.6f, normalScale.z);
        crouchHeight = normalHeight * 0.6f;
        normalColliderHeight = normalHeight * 2f;
        crouchColliderHeight = crouchHeight * 3.4f;

        Debug.Log("crouchColliderHeight: " + crouchColliderHeight);
        Debug.Log("crouch height: " + crouchHeight);

        IsCrouching = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            if (IsCrouching)
            {
                if (UnCrouchCheck()) ToggleCrouch();
            }
            else ToggleCrouch();
        }
        UpdateHeight();
    }

    private void ToggleCrouch()
    {
        IsCrouching = !IsCrouching;
    }

    private void UpdateHeight()
    {
        float targetColliderHeight = IsCrouching ? crouchColliderHeight : normalColliderHeight;
        float targetHeight = IsCrouching ? crouchHeight : normalHeight;

        transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(normalScale.x, targetHeight, normalScale.z), crouchSpeed * Time.deltaTime);
        playerCollider.height = Mathf.Lerp(playerCollider.height, targetColliderHeight, crouchSpeed * Time.deltaTime);
    }

    private bool UnCrouchCheck()
    {
        Debug.DrawRay(transform.position + playerCollider.center, Vector3.up * (normalHeight / 2f + 0.01f), Color.red, 2f);
        if (Physics.Raycast(transform.position + playerCollider.center, Vector3.up, normalHeight / 2f + 0.01f)) return false;
        else return true;

    }
}



