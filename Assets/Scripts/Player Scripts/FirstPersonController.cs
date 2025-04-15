using UnityEngine;

public class FirstPersonController : MonoBehaviour
{
    [Header("References")]
    public Transform Orientation;
    Rigidbody rb;

    [Header("Movement Settings")]
    [SerializeField] private float walkMoveSpeed;
    [SerializeField] private float sprintMoveSpeed;
    [SerializeField] private float crouchMoveSpeed;
    [SerializeField] private float crouchSprintMoveSpeed;
    [SerializeField] private float sprintTransitionSpeed;
    [SerializeField] private float groundDrag;
    [SerializeField] private float airMultiplier;

    [Header("Input")]
    private float horizontalInput;
    private float verticalInput;

    [Header("GroundCheck")]
    public float playerHeight ;
    private bool isGrounded;

    [Header("State Controllers")]
    public StaminaControll staminaController;
    public Crouch crouchController;


    private float moveSpeed;
    private Vector3 moveDirection;


    private void Start()
    {
        moveSpeed = walkMoveSpeed;
        rb = GetComponent<Rigidbody>();

    }

    private void Update()
    {
        GetInput();
        MovePlayer();
        RotatePlayer();
        GroundCheck();
        HandleSpeed();
        VelocityControl();
    }

    private void GetInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
    }

    private void MovePlayer()
    {
        moveDirection = Orientation.forward * verticalInput + Orientation.right * horizontalInput;

        if (isGrounded)
        {
            rb.drag = groundDrag;
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);
        }
        else
        {
            rb.drag = 0;
            rb.AddForce(moveDirection.normalized * moveSpeed * airMultiplier * 10f, ForceMode.Force);
        }
    }

    private void RotatePlayer()
    {
        Vector3 cameraRotation = Camera.main.transform.eulerAngles;
        transform.rotation = Quaternion.Euler(0, cameraRotation.y, 0);
    }

    private void VelocityControl()
    {
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        if (flatVel.magnitude > moveSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }
    }
    private void HandleSpeed()
    {
        if (staminaController.CanSprint && Input.GetKey(KeyCode.LeftShift) && crouchController.IsCrouching)
        {
            moveSpeed = Mathf.Lerp(moveSpeed, crouchSprintMoveSpeed, sprintTransitionSpeed * Time.deltaTime);
        }
        
        else if (staminaController.CanSprint && Input.GetKey(KeyCode.LeftShift))
        {
            moveSpeed = Mathf.Lerp(moveSpeed, sprintMoveSpeed, sprintTransitionSpeed * Time.deltaTime);
        }
        else if (crouchController.IsCrouching)
        {
            moveSpeed = Mathf.Lerp(moveSpeed, crouchMoveSpeed, crouchController.CrouchSpeed * Time.deltaTime);
        }
        else
        {
            moveSpeed = Mathf.Lerp(moveSpeed, walkMoveSpeed, sprintTransitionSpeed * Time.deltaTime);
        }

    }

    private void GroundCheck()
    {
        isGrounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.1f);
    }

}
