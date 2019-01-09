using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Head : ArmorItem {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public override void Attach()
    {base.Attach();
		var companion = AttachedCharacter as Companion;
        if (companion.armor.currentHead)
        {
            companion.armor.currentHead.armor.SetActive(false);
            companion.armor.currentHead.tab.gameObject.SetActive(true);
        }
        else if (companion.armor.startHead)
        {
            companion.armor.startHead.SetActive(false);
        }
        companion.armor.currentHead = this;
		armor.SetActive(true);
    }
    public override void PickUp(Companion companion)
    {
		
        parent = companion.inventory.armorInventory.Head;
		base.PickUp(companion);
    }
}
