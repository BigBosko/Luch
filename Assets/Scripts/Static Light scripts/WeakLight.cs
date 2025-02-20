using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeakLight : StaticLight
{

    private EnemyAI enemyAi;
    protected override void Start()
    {
        base.Start();
        enemyAi = FindAnyObjectByType<EnemyAI>();
    }
    protected override void Update()
    {
        base.Update();
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
