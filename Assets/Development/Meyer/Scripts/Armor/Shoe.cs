using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoe : ArmorItem {

	// Use this for initialization
	protected override void Start () {
		type = Type.Shoe;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public override void Attach()
    {
		base.Attach();
		var companion = AttachedCharacter as Companion;
        if (companion.armor.currentShoe)
        {
            companion.armor.currentShoe.armor.SetActive(false);
            companion.armor.currentShoe.tab.gameObject.SetActive(true);
        }
        else if (companion.armor.currentShoe)
        {
            companion.armor.startShoe.SetActive(false);
        }
        companion.armor.currentBelt = this;
		armor.SetActive(true);
    }
    public override void PickUp(Companion companion)
    {
		
        parent = companion.inventory.armorInventory.Shoes;
		base.PickUp(companion);
    }
}
