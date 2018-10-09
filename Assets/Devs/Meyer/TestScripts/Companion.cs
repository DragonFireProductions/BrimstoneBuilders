using System.Collections;
using System.Collections.Generic;

using Assets.Meyer.TestScripts;
using Assets.Meyer.TestScripts.Player;

using Kristal;

using UnityEngine;
using UnityEngine.AI;

public class Companion : MonoBehaviour {
	

    [ SerializeField ] public NavMeshAgent agent;

	[ SerializeField ] public GameObject camHolder;
	

	[SerializeField] float health = 100;

	public CompanionLeader leader;

	public GameObject obj;

	public CompanionNav Nav;

	public Stat Stats;

    // Use this for initialization
    void Start () {
	    camHolder = transform.Find( "CamHolder" ).gameObject;
	    Stats = gameObject.GetComponent < Stat >( );
	    obj = gameObject;
	    Nav = gameObject.GetComponent < CompanionNav >( );
	    leader = Character.player.GetComponent < CompanionLeader >( );
	    agent = gameObject.GetComponent < NavMeshAgent >( );

    }

	public void Damage(Enemy attacker ) {

		UIInventory.instance.ShowNotification(attacker.name + " has chosen to attack " + this.gameObject.name, 5);
		UIInventory.instance.AppendNotification("\n Damage = " + DamageCalc.Instance.CalcAttack(attacker.Stats, this.Stats) );
		UIInventory.instance.AppendNotification("\n " + this.Stats.Name + " health was " + this.Stats.Health);
		this.Stats.Health -= DamageCalc.Instance.CalcAttack(attacker.Stats, this.Stats);
		UIInventory.instance.AppendNotification("\n" + this.Stats.name + " health is now " + this.Stats.Health);
		UIInventory.instance.UpdateCompanionStats(this.Stats);

        if (this.Stats.Health <= 0 && this != leader)
        {
            UIInventory.instance.AppendNotification("\n Enemy is now Dead");

	        if ( this == TurnBasedController.instance.PlayerSelectedCompanion ){
		        TurnBasedController.instance.switchCompanionSelected = true;
	        }
            leader.Remove(this);
        }
        else
        {
            UIInventory.instance.AppendNotification("\n" + this.Stats.name + " health is now " + this.Stats.Health);

        }


    }
    // Update is called once per frame
    void Update () { 
    }

	public GameObject CamHolder {
		get { return camHolder; }

	}

}
