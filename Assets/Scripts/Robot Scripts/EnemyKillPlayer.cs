using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyKillPlayer : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private Canvas deathScreen;

    private void Start()
    {
        deathScreen.enabled = false;
    }

    private void OnTriggerEnter(Collider triggerObject)
    {
        if (((1 << triggerObject.gameObject.layer) & playerLayer) != 0) //binary check layerja
        {
            KillPlayer();
        }
    }

    private void KillPlayer()
    {
        Debug.Log("Player Killed");
        ShowDeathScreen();
    }
    private void ShowDeathScreen()
    {
        deathScreen.enabled = true;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
