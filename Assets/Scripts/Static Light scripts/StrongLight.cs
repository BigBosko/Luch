using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrongLight : StaticLight
{
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
        //kill logic
        Debug.Log("Player killed because of strong light");
    }

}
