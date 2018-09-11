using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemy : BaseEnemy
{
    void Update()
    {
        FindClosestTarget();
        MoveToTarget();
        CheckForDamage();

        if (targetsInRange[0] == true)
        {
            attackTimer += Time.deltaTime;

            if (attackTimer >= attackSpeed && health.CurValue > 0)
            {
                //AttackPlayer();
            }
        }

        if (targetsInRange[2] == true)
        {
            attackTimer += Time.deltaTime;

            if (attackTimer >= attackSpeed && health.CurValue > 0)
            {
                AttackTurret();
            }
        }
    }
}
