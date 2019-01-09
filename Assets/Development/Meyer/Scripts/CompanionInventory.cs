using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;

public class CompanionInventory : PlayerInventory {
	
	public Companion companion;

    public void Awake() {
		base.Awake();
	    companion = gameObject.GetComponent < Companion >( );
    }
	
    //returns 
    public WeaponObject GetItemFromInventory(string name)
    {
        return companion.inventory.WeaponInventory.PickedUpWeapons.FirstOrDefault(_t => _t.stats.objectName == name);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
