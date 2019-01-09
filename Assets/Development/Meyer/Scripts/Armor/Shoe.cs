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
		    parent.labels.Add(companion.armor.currentShoe.label);
            companion.armor.currentShoe.label.gameObject.SetActive(true);
            companion.armor.currentShoe.tab.gameObject.SetActive(true);
        }
        else if (companion.armor.startShoe)
        {
			
            companion.armor.startShoe.SetActive(false);
        }
        companion.armor.currentShoe = this;
		parent.DeleteLabel(label);
		model.SetActive(true);
		parent.FixLayout();
    }
    public override void PickUp(Companion companion)
    {
		
        parent = companion.inventory.armorInventory.Shoes;
		base.PickUp(companion);
    }
}
