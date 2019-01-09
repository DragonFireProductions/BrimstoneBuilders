using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoulder : ArmorItem {

	// Use this for initialization
	protected override void Start () {
		type = Type.Shoulder;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public override void Attach()
    {
		base.Attach();
		var companion = AttachedCharacter as Companion;
        if (companion.armor.currentShoulder)
        {
			parent.labels.Add(companion.armor.currentShoulder.label);
            companion.armor.currentShoulder.model.SetActive(false);
            companion.armor.currentShoulder.tab.gameObject.SetActive(true);
        }
        else if (companion.armor.startShoulder)
        {
            companion.armor.startShoulder.SetActive(false);
        }
		parent.DeleteLabel(label);
        companion.armor.currentShoulder = this;
		model.SetActive(true);
    }
    public override void PickUp(Companion companion)
    {
		
        parent = companion.inventory.armorInventory.Shoulder;
		base.PickUp(companion);
    }
}
