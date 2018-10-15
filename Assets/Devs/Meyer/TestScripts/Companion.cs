using System.Collections;
using System.Collections.Generic;

using Assets.Meyer.TestScripts;
using Assets.Meyer.TestScripts.Player;

using Kristal;

using UnityEngine;
using UnityEngine.AI;

public class Companion : BaseCharacter {
	

	[ SerializeField ] public GameObject camHolder;

	public CompanionNav Nav;

    // Use this for initialization
    void Awake () {
		base.Awake();
		this.material.color = BaseColor;
	    camHolder = transform.Find( "CamHolder" ).gameObject;
	    Nav = gameObject.GetComponent < CompanionNav >( );
	    leader = Character.player.GetComponent < CompanionLeader >( );
    }

	public void Damage(Enemy attacker ) {

		
		UIInventory.instance.AppendNotification("    " + gameObject.name + ":") ;
		UIInventory.instance.AppendNotification("\n Damage = " + DamageCalc.Instance.CalcAttack(attacker.stats, this.stats) );
		UIInventory.instance.AppendNotification("\n health was " + this.stats.Health);
		float damage =  DamageCalc.Instance.CalcAttack(attacker.stats, this.stats);
		this.stats.Health -= damage;
		base.DamageDone((int)damage, this);
	
        if (this.stats.Health <= 0 && this != leader)
        {
            UIInventory.instance.AppendNotification("\n Companion is now Dead");

	        if ( this == TurnBasedController.instance.PlayerSelectedCompanion ){
		        TurnBasedController.instance.switchCompanionSelected = true;
	        }
            Leader.Remove(this);
        }
        else if (this != leader)
        {
            UIInventory.instance.AppendNotification("\n health is now " + this.stats.Health);

        }


    }
	public void Remove( BaseCharacter chara ) { }
    // Update is called once per frame
    void Update () { 
    }

	

	public GameObject CamHolder {
		get { return camHolder; }

	}
    public CompanionLeader Leader
    {
        get
        {
            return (CompanionLeader)leader;

        }
        set { leader = value; }
    }
}
