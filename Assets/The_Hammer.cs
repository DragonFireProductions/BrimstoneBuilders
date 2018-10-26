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
	    stats.Health = 15.0f;
		stats.Strength = 30.0f;
		stats.AdjustScale(stats.Strength);
	}

    public override void Damage(BaseCharacter attacker)
    {
        StaticManager.UiInventory.ShowNotification("   " + gameObject.name + " : ", 5);
        //StaticManager.UiInventory.AppendNotification("\n Damage = " + StaticManager.DamageCalc.CalcAttack(attacker.stats, this.stats));
        //StaticManager.UiInventory.AppendNotification("\n Health was " + this.stats.Health);
        float damage = StaticManager.DamageCalc.CalcAttack(attacker.stats, this.stats);

        this.stats.Health -= damage;

        if (this.stats.Health <= 0)
        {
            //StaticManager.UiInventory.AppendNotification("\n Enemy is now Dead");
            Leader.Remove(this);
        }
        else
        {
            //StaticManager.UiInventory.AppendNotification("\n health is now " + this.stats.Health);

        }

    }
}
