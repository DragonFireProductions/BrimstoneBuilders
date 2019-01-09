using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Belt : ArmorItem {

	// Use this for initialization
	protected override void Start () {
		type = Type.Belt;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public override void Attach()
    {
		base.Attach();
		var companion = AttachedCharacter as Companion;
        if (companion.armor.currentBelt)
        {
            companion.armor.currentBelt.armor.SetActive(false);
            companion.armor.currentBelt.tab.gameObject.SetActive(true);
        }
        else if (companion.armor.startBelt)
        {
            companion.armor.startBelt.SetActive(false);
        }
        companion.armor.currentBelt = this;
    }

    public override void PickUp(Companion companion) {
		
	    parent = companion.inventory.armorInventory.Belt;
		base.PickUp(companion);
    }
}
