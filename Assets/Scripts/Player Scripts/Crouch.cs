using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crouch : MonoBehaviour
{
    [Header("Crouch Settings")]
    [SerializeField] private float crouchSpeed = 5f;
    [SerializeField] private float normalHeight = 2f;
    [SerializeField] private float crouchHeight = 1f;

    private CharacterController characterController;
    public bool IsCrouching { get; private set; }

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            ToggleCrouch();
        }

        UpdateHeight();
    }

    private void ToggleCrouch()
    {
        IsCrouching = !IsCrouching;
    }

    private void UpdateHeight()
    {
        float targetHeight = IsCrouching ? crouchHeight : normalHeight;
        characterController.height = Mathf.Lerp(characterController.height, targetHeight, crouchSpeed * Time.deltaTime);
    }

    public float CrouchSpeed => crouchSpeed;
}



