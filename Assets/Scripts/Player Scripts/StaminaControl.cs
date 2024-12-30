using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaminaControll : MonoBehaviour
{
    [Header("Main Stamina Settings")]
    [SerializeField] private float maxStamina = 100f;
    [SerializeField] private float staminaDrain = 0.5f;
    [SerializeField] private float staminaRegen = 0.7f;
    [SerializeField] private float regenDelay = 3f;

    [SerializeField] private float currentStamina;
    public bool CanSprint => currentStamina > 0 && !isStaminaExhausted;
    public float CurrentStamina => currentStamina;

    private float regenTimer = 0f;
    private bool isStaminaExhausted = false;

    private void Start()
    {
        currentStamina = maxStamina;
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.LeftShift) && CanSprint)
        {
            DrainStamina();
            regenTimer = regenDelay;
            isStaminaExhausted = currentStamina <= 0;
        }
        else
        {
            HandleRegeneration();
            if (currentStamina > 0)
            {
                isStaminaExhausted = false;
            }
        }
    }

    private void DrainStamina()
    {
        currentStamina = Mathf.Max(0, currentStamina - staminaDrain * Time.deltaTime);
    }

    private void HandleRegeneration()
    {
        if (regenTimer > 0)
        {
            regenTimer -= Time.deltaTime;
        }
        else
        {
            RegenerateStamina();
        }
    }

    private void RegenerateStamina()
    {
        currentStamina = Mathf.Min(maxStamina, currentStamina + staminaRegen * Time.deltaTime);
    }
}
