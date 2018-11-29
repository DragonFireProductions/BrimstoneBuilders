using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Potions : BaseItems {

	public ParticleSystem hit_effect;

	public ParticleSystem cast_effect;

	public string PotionName;

    public abstract void Cast(BaseCharacter companion = null );
    public override void Attach( ) {
       
        Cast(AttachedCharacter);

        var c = AttachedCharacter as Companion;
        c.inventoryUI.RemoveObject(this);

        Destroy(gameObject);

    }

    public virtual void PickUp(BaseCharacter character ) {
        if (AttachedCharacter == null)
        {
            gameObject.SetActive(false);
        }
    }

	public void OnTriggerEnter(Collider collider ) {
        if (collider.tag == "Player"  && tag == "PickUp")
        {
            StaticManager.Character.inventory.PickUp(this);
            this.GetComponent<BoxCollider>().enabled = false;
            AttachedCharacter = StaticManager.Character;
        }
    }
    public virtual object this[string propertyName]
    {
        get { return this.GetType().GetField(propertyName).GetValue(this); }
        set { this.GetType().GetField(propertyName).SetValue(this, value); }
    }
}
