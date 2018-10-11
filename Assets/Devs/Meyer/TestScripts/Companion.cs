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
    void Start () {
	    camHolder = transform.Find( "CamHolder" ).gameObject;
	    Nav = gameObject.GetComponent < CompanionNav >( );
	    leader = Character.player.GetComponent < CompanionLeader >( );

    }

	public void Damage(Enemy attacker ) {

		UIInventory.instance.ShowNotification(attacker.name + " has chosen to attack " + this.gameObject.name, 5);
		UIInventory.instance.AppendNotification("\n Damage = " + DamageCalc.Instance.CalcAttack(attacker.stats, this.stats) );
		UIInventory.instance.AppendNotification("\n " + this.stats.Name + " health was " + this.stats.Health);
		this.stats.Health -= DamageCalc.Instance.CalcAttack(attacker.stats, this.stats);
		UIInventory.instance.AppendNotification("\n" + this.stats.name + " health is now " + this.stats.Health);
		UIInventory.instance.UpdateCompanionStats(this.stats);

        if (this.stats.Health <= 0 && this != leader)
        {
            UIInventory.instance.AppendNotification("\n Enemy is now Dead");

	        if ( this == TurnBasedController.instance.PlayerSelectedCompanion ){
		        TurnBasedController.instance.switchCompanionSelected = true;
	        }
            leader.Remove(this);
        }
        else
        {
            UIInventory.instance.AppendNotification("\n" + this.stats.name + " health is now " + this.stats.Health);

        }


    }
	
    // Update is called once per frame
    void Update () { 
    }

	public override void Remove( BaseCharacter _obj ) {
        for (int l_i = 0; l_i < characters.Count; l_i++)
        {
            if (characters[l_i].gameObject == _obj.gameObject){
	            characters.RemoveAt( l_i );
				characterObjs.RemoveAt(l_i);
                TurnBasedController.instance.Companions.Remove(( Companion )_obj);
            }

        }
        Destroy(_obj.obj);
    }

	public GameObject CamHolder {
		get { return camHolder; }

	}

}
