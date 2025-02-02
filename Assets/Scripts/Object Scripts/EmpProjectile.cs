using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmpProjectile : MonoBehaviour
{
    public LayerMask robotLayer;

    public float detectionDistance = 0.2f;

    EnemyAI robot;
    private void Start()
    {
        robot = FindAnyObjectByType<EnemyAI>();
    }

    void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, detectionDistance, robotLayer))
        {
            robot.Stun();
        }
    }
}
