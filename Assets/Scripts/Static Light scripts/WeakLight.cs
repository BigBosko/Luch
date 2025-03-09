using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeakLight : StaticLight
{
    [Header("References")]
    private EnemyAI enemyAi;

    protected override void Start()
    {
        base.Start();
        enemyAi = FindAnyObjectByType<EnemyAI>();

    }
    private void Update()
    {
        DetectPlayer();
    }

    protected override void DetectPlayer()
    {
        if (isPlayerInZone && isLightOn)
        {
            NotifyRobot();
        }
    }

    private void NotifyRobot()
    {
        //Debug.Log("Player detected in light! Notify the robot.");
        enemyAi.isPlayerDetected = true;
        enemyAi.lastPosition = detectionCollider.transform.position;
    }


}
