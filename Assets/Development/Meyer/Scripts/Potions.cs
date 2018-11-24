using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Potions : MonoBehaviour {

	public ParticleSystem hit_effect;

	public ParticleSystem cast_effect;

	public string PotionName;

	[HideInInspector] public WeaponItem item;

	public BaseCharacter AttachedBaseCharacter;

	public void Start( ) {
		item = StaticManager.inventories.GetItemFromAssetList( PotionName );
	}
	public abstract void Cast( Companion attacker );

	public virtual void PickUp(BaseCharacter character ) {
        if (AttachedBaseCharacter == null)
        {
            StaticManager.UiInventory.AddSlot(this, StaticManager.Character.inventory);
            gameObject.SetActive(false);
        }
    }

	public void OnTriggerEnter(Collider collider ) {
        if (collider.tag == "Player"  && tag == "PickUp")
        {
            StaticManager.Character.inventory.PickUp(this);
            this.GetComponent<BoxCollider>().enabled = false;
            AttachedBaseCharacter = StaticManager.Character;
        }
    }
}
