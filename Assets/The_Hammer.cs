using System.Collections;
using System.Collections.Generic;
using System.Security.Policy;
using Assets.Meyer.TestScripts;
using Kristal;
using UnityEngine;
using UnityEngine.AI;

public class The_Hammer : EnemyLeader
{
    private float battleSpeed = 3;

    [SerializeField] GameObject[] players;
    private int currenemy;

    private AnimationClass animatorclass;

    void Awake()
    {
        base.Awake();
        animatorclass = GetComponent<AnimationClass>();
    }
	// Use this for initialization
	void Start ()
	{
        base.Start();
	    stats.Agility = 30.0f;
	    stats.AttackPoints = 6;
	    stats.Dexterity = 30.0f;
	    stats.Endurance = 30.0f;
	    stats.Health = 100.0f;
	    stats.MaxHealth = 100.0f;
	}

    public override void Damage(BaseCharacter attacker)
    {
        StaticManager.uiInventory.ShowNotification("   " + gameObject.name + " : ", 5);
        StaticManager.uiInventory.AppendNotification("\n Damage = " + StaticManager.DamageCalc.CalcAttack(attacker.stats, this.stats));
        StaticManager.uiInventory.AppendNotification("\n Health was " + this.stats.Health);
        float damage = StaticManager.DamageCalc.CalcAttack(attacker.stats, this.stats);

        this.stats.Health -= damage;
        Debug.Log("Enemycount: " + TurnBasedController.instance._enemy.characters.Count + "           Damage-Enemy- line: 64");

        base.DamageDone((int)damage, this);

        if (this.stats.Health <= 0)
        {
            StaticManager.uiInventory.AppendNotification("\n Enemy is now Dead");
            Leader.Remove(this);
        }
        else
        {
            StaticManager.uiInventory.AppendNotification("\n health is now " + this.stats.Health);

        }

    }
}
