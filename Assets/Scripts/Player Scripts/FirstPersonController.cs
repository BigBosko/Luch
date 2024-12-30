using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class FirstPersonController : MonoBehaviour
{
    [Header("References")]
    private CharacterController controller;
    public Transform Orientation;

    [Header("Movement Settings")]
    [SerializeField] private float walkMoveSpeed;
    [SerializeField] private float sprintMoveSpeed;
    [SerializeField] private float crouchMoveSpeed;
    [SerializeField] private float crouchSprintMoveSpeed;
    [SerializeField] private float sprintTransitionSpeed;
    [SerializeField] private float gravity = 9.81f;

    [Header("Input")]
    private float horizontalInput;
    private float verticalInput;

    private float speed;
    private Vector3 moveDirection;
    private float verticalVelocity;

    [Header("State Controllers")]
    public StaminaControll staminaController;
    public Crouch crouchController;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        speed = walkMoveSpeed;
    }

    private void Update()
    {
        GetInput();
        MovePlayer();
        RotatePlayer();
        HandleSpeed();
    }

    private void GetInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
    }

    private void MovePlayer()
    {
        moveDirection = Orientation.forward * verticalInput + Orientation.right * horizontalInput;
        moveDirection = moveDirection.normalized * speed;
        moveDirection.y = CalculateVerticalVelocity();

        controller.Move(moveDirection * Time.deltaTime);
    }

    private float CalculateVerticalVelocity()
    {
        if (controller.isGrounded)
        {
            verticalVelocity = -1f; // Prevent floating when grounded
        }
        else
        {
            verticalVelocity -= gravity * Time.deltaTime;
        }
        return verticalVelocity;
    }

    private void RotatePlayer()
    {
        Vector3 cameraRotation = Camera.main.transform.eulerAngles;
        transform.rotation = Quaternion.Euler(0, cameraRotation.y + 180f, 0);
    }

    private void HandleSpeed()
    {
        if (staminaController.CanSprint && Input.GetKey(KeyCode.LeftShift) && crouchController.IsCrouching)
        {
            speed = Mathf.Lerp(speed, crouchSprintMoveSpeed, sprintTransitionSpeed * Time.deltaTime);
        }
        
        else if (staminaController.CanSprint && Input.GetKey(KeyCode.LeftShift))
        {
            speed = Mathf.Lerp(speed, sprintMoveSpeed, sprintTransitionSpeed * Time.deltaTime);
        }
        else if (crouchController.IsCrouching)
        {
            speed = Mathf.Lerp(speed, crouchMoveSpeed, crouchController.CrouchSpeed * Time.deltaTime);
        }
        else
        {
            speed = Mathf.Lerp(speed, walkMoveSpeed, sprintTransitionSpeed * Time.deltaTime);
        }
    }
}
