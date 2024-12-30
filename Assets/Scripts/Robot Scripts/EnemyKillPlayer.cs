using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyKillPlayer : MonoBehaviour
{
    public LayerMask playerLayer;

    private void OnTriggerEnter(Collider triggerObject)
    {
        if (((1 << triggerObject.gameObject.layer) & playerLayer) != 0) //binary check layerja
        {
            KillPlayer();
        }
    }

    private void KillPlayer()
    {
        Debug.Log("Player killed!");
    }
}
