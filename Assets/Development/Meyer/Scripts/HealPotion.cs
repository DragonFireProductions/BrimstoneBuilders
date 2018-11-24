using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealPotion : Potions {

	private ParticleSystem _hit_effect;

	[ SerializeField ] private int HealAmount;
	public override void Cast( Companion attacker ) {
		_hit_effect = Instantiate( this.hit_effect );
		_hit_effect.gameObject.transform.position = attacker.transform.position;
		_hit_effect.gameObject.transform.SetParent(attacker.transform);
		_hit_effect.gameObject.SetActive(true);
		attacker.stats.Health += HealAmount;

		if ( !StaticManager.Instance.unlimitedPotions ){
			StaticManager.UiInventory.RemoveMainInventory(this, attacker.inventory);
		}
		
	}
}
