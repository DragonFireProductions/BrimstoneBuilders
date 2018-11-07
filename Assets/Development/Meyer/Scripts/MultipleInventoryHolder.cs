using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultipleInventoryHolder : MonoBehaviour {

	public List < PlayerInventory > alllables;

	public void Awake( ) {
		alllables = new List < PlayerInventory >();
	}
	public void SwitchInventory(PlayerInventory inventory ) {
		foreach ( var l_inventory in alllables ){
			l_inventory.parent.SetActive( l_inventory == inventory );
		}
	}

	public void AddCompanionInventory( ) {

	}

	public void GetInventory(string parentName ) {
		foreach ( var l_playerInventory in alllables ){
			if parentName = alllables.parent.name;
		}
	}
}
