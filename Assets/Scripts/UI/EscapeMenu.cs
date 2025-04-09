using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EscapeMenu : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Canvas pauseMenu;

    private bool isPaused = false;

    void Start()
    {
        pauseMenu.enabled = false;
        // Initialize cursor state on game start
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        // Only check for Escape key press
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseGame();
        }
    }

    private void PauseGame()
    {
        isPaused = !isPaused;
        pauseMenu.enabled = isPaused;

        if (isPaused)
        {
            Time.timeScale = 0; // Freeze game time
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {
            Time.timeScale = 1; // Resume game time
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }
}