using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemLevelCalculation : MonoBehaviour {

	public void IncreaseLevel(BaseItems item, float int_f ) {
		item.Level += int_f;
		IncreaseStats(item);
	}

	public void IncreaseStats(BaseItems item) {
		item.stats.baseDamage =(int) item.Level * 2;

		if ( item is WeaponObject ){
			var weapon = item as WeaponObject;

			if ( item is GunType ){
				var gun = item as GunType;
				gun.Ammo = (int)item.Level;
				gun.Capacity = gun.Capacity + (int)item.Level * 2;
				gun.Range = gun.Range + item.Level * 2;
				gun.projectile.hits = gun.projectile.hits + (int)item.Level;
			}
		}
	}
}
