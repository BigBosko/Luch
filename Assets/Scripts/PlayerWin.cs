using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWin : MonoBehaviour
{
    [Header("Refrences")]
    [SerializeField] private Canvas winScreen;

    private void Start()
    {
        winScreen.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        ShowWinScreen();
    }

    private void ShowWinScreen()
    {
        winScreen.enabled = true;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
