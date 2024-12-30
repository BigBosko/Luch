using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyKillPlayer : MonoBehaviour
{
    public string playerTag = "Player"; // Tag the player GameObject with "Player"

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(playerTag))
        {
            KillPlayer();
        }
    }

    private void KillPlayer()
    {
        // Add your kill logic here (e.g., play animation, reduce health, restart level)
        Debug.Log("Player killed!");
    }
}
