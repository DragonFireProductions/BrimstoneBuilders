using System.Collections;
using System.Collections.Generic;

using Assets.Meyer.TestScripts;
using Assets.Meyer.TestScripts.Player;

using Kristal;

using TMPro;

using UnityEngine;
using UnityEngine.AI;

public class Companion : BaseCharacter {
	

	[ SerializeField ] public GameObject camHolder;
	

    // Use this for initialization
    void Start () {
		base.Awake();
		this.material.color = BaseColor;
	    camHolder = transform.Find( "CamHolder" ).gameObject;
	    Nav = gameObject.GetComponent < CompanionNav >( );
	    leader = StaticManager.character.GetComponent < CompanionLeader >( );
    }

	public override void Damage(BaseCharacter attacker ) {

		
		StaticManager.uiInventory.AppendNotification("    " + gameObject.name + ":") ;
		StaticManager.uiInventory.AppendNotification("\n Damage = " + StaticManager.DamageCalc.CalcAttack(attacker.stats, this.stats) );
		StaticManager.uiInventory.AppendNotification("\n health was " + this.stats.Health);
		float damage =  StaticManager.DamageCalc.CalcAttack(attacker.stats, this.stats);
		this.stats.Health -= damage;
		base.DamageDone((int)damage, this);
	
        if (this.stats.Health <= 0 && this != leader)
        {
          StaticManager.uiInventory.AppendNotification("\n Companion is now Dead");
			
            Leader.Remove(this);
        }
        else if (this != leader)
        {
          StaticManager.uiInventory.AppendNotification("\n health is now " + this.stats.Health);

        }
		else if ( this == leader && stats.Health <= 0 ){
            StaticManager.uiInventory.ShowGameOver(true);
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
