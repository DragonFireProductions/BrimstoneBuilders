using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterStats))]
public class CharacterCombat : MonoBehaviour
{
    public float attackSpeed = 1f;//COME BACK AND MAKE THIS INFLUENCED BY PERCENTAGE OF CHARACTER SPEED OR AGILITY
    float attackCooldown;
    const float combatCooldown = 5;
    float lastAttackTime;

    public float attackDelay = 0.6f;

    public event System.Action OnAttack;

    CharacterStats myStats;
    CharacterStats opponentStats;

    public bool inCombat { get; private set; }

    void Start()
    {
        myStats = GetComponent<CharacterStats>();
    }

    void Update()
    {
        attackCooldown -= Time.deltaTime;

        if (Time.time - lastAttackTime > combatCooldown)
        {
            inCombat = false;
        }
    }

    public void Attack(CharacterStats targetStats)
    {
        if (attackCooldown <= 0f)
        {
            opponentStats = targetStats;

            if (OnAttack != null)
                OnAttack();
            attackCooldown = 1f / attackSpeed;
            inCombat = true;
            lastAttackTime = Time.time;
        }
    }

    public void AttackHit_AnimationEvent()
    {
        //opponentStats.TakeDamage(myStats.damage.GetValue());

        if (opponentStats.currentHealth <= 0)
        {
            inCombat = false;
        }
    }
}
