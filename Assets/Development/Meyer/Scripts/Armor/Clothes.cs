using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clothes : ArmorItem {

	// Use this for initialization
	protected override void Start () {
		type = Type.Clothes;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public override void Attach( ) {
		base.Attach();
		var companion = AttachedCharacter as Companion;
        if (companion.armor.currentBody)
        {
            companion.armor.currentBody.armor.SetActive(false);
            companion.armor.currentBody.tab.gameObject.SetActive(true);
        }
        else if (companion.armor.startBody)
        {
            companion.armor.startBody.SetActive(false);
        }
        companion.armor.currentBody = this;
		armor.SetActive(true);
    }
    public override void PickUp(Companion companion)
    {
		
        parent = companion.inventory.armorInventory.Clothes;
		base.PickUp(companion);
    }
}
