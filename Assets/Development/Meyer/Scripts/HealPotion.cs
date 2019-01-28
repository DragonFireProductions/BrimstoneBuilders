using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealPotion : Potions {

	private ParticleSystem _hit_effect;

	[ SerializeField ] public int HealAmount;
	public override void Cast( BaseCharacter enemy = null  ) {
		_hit_effect = Instantiate( this.hit_effect );
		_hit_effect.gameObject.transform.position = enemy.transform.position;
		_hit_effect.gameObject.transform.SetParent(enemy.transform);
		_hit_effect.gameObject.SetActive(true);

		enemy.stats.Health += HealAmount;
	    if(enemy.stats.Health > 100)
	    {
	        enemy.stats.Health = 100;
	    }
	    InstatiateFloatingText.InstantiateFloatingText("MAX HEALTH", Color.green, enemy);
        if ( !StaticManager.Instance.unlimitedPotions ){
			var e = enemy as Companion;
			StaticManager.UiInventory.RemoveMainInventory(this, e.inventory);
		}

	}

	public override void IncreaseSubClass( float amount ) {
        var a = AttachedCharacter as Companion;
        a.magic.IncreaseLevel(amount);
    }
}
