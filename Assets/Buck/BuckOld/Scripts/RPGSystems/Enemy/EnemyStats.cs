using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : CharacterStats
{
    public override void Die()
    {
        base.Die();

        Destroy(gameObject);
        //add ragdool effect or death animation
    }
}
