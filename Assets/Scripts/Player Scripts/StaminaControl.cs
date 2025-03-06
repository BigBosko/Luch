using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StaminaControll : MonoBehaviour
{
    [Header("Main Stamina Settings")]
    [SerializeField] private float maxStamina = 100f;
    [SerializeField] private float staminaDrain = 0.5f;
    [SerializeField] private float staminaRegen = 0.7f;
    [SerializeField] private float regenDelay = 3f;

    [Header("References")]
    [SerializeField] private Image staminaBar;

    [SerializeField] private float currentStamina;

    public bool CanSprint => currentStamina > 0 && !isStaminaExhausted;
    public float CurrentStamina => currentStamina;

    private float regenTimer = 0f;
    private bool isStaminaExhausted = false;

    private void Start()
    {
        currentStamina = maxStamina;
        staminaBar.gameObject.SetActive(false);
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

        StaminaUI();
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

    private void StaminaUI()
    {
        staminaBar.fillAmount = currentStamina / maxStamina;

        if (currentStamina == maxStamina)
        {
            StopAllCoroutines(); 
            StartCoroutine(HideStaminaBarAfterDelay(2f));
        }
        else
        {
            staminaBar.gameObject.SetActive(true);
        }
    }

    private IEnumerator HideStaminaBarAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        staminaBar.gameObject.SetActive(false);
    }

}
