using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crouch : MonoBehaviour
{
    [Header("References")]
    private CapsuleCollider playerCollider;

    [Header("Crouch Settings")]
    [SerializeField] private float crouchSpeed = 5f;
    [SerializeField] private float normalHeight = 2f;
    [SerializeField] private float crouchHeight = 1.2f;


    private Vector3 normalScale; //se dinamicno nastavi v start();
    private Vector3 crouchScale = new Vector3(1f, 0.5f, 1f);

    public bool IsCrouching { get; private set; }

    public float CrouchSpeed => crouchSpeed;

    private void Start()
    {
        playerCollider = GetComponent<CapsuleCollider>();
        normalScale = transform.localScale;

        IsCrouching = false;
        Debug.Log(transform.localScale);
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
        Vector3 targetScale = IsCrouching ? crouchScale : normalScale;
        float targetHeight = IsCrouching ? crouchHeight : normalHeight;

        if (transform.localScale != targetScale)
        {
            transform.localScale = Vector3.Lerp(transform.localScale, targetScale, crouchSpeed * Time.deltaTime);

            if (Vector3.Distance(transform.localScale, targetScale) < 0.01)
            {
                transform.localScale = targetScale;
            }

        }


        playerCollider.height = Mathf.Lerp(playerCollider.height, targetHeight, crouchSpeed * Time.deltaTime);

        playerCollider.center = new Vector3(playerCollider.center.x, Mathf.Lerp(playerCollider.center.y / 2, targetHeight / 2, crouchSpeed * Time.deltaTime), playerCollider.center.z);
    }
}



