using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicTurret : BaseTurret
{
    // Update is called once per frame
    void Update()
    {
        UpdateTurretTarget();

        LockOnTarget();

        CheckTime();

        CheckForDamage();

        if (target != null)
        {
            fireTimer += Time.deltaTime;

            if (fireTimer >= fireRate)
            {
                CheckForEnemy();
            }
        }
        if (fireTimer >= fireRate * effectsDisplayTime)
        {
            DisableEffects();
        }
    }
}
