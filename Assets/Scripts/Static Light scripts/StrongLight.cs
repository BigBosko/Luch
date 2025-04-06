using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrongLight : StaticLight
{
    [SerializeField] private Canvas deathScreen;
    protected override void Start()
    { 
        base.Start();
    }
      
    protected void Update()
    {
        DetectPlayer();
        //Debug.Log("IsPlayerInZone= " + isPlayerInZone + " | Light is " + (isLightOn ? "ON" : "OFF"));
    }

    protected override void DetectPlayer()
    {
        if (isPlayerInZone && isLightOn)
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
