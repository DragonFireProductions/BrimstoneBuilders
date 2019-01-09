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
			parent.labels.Add(companion.armor.currentHead.label);
            companion.armor.currentHead.model.SetActive(false);
            companion.armor.currentHead.tab.gameObject.SetActive(true);
        }
        else if (companion.armor.startHead)
        {
            companion.armor.startHead.SetActive(false);
        }
		parent.DeleteLabel(label);
        companion.armor.currentHead = this;
		model.SetActive(true);
    }
    public override void PickUp(Companion companion)
    {
		
        parent = companion.inventory.armorInventory.Head;
		base.PickUp(companion);
    }
}
