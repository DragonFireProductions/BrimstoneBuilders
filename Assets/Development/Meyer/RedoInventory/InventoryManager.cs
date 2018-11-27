using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour {

	public List < TabScript > tabs;

	public List < BaseInventory > allInventories;

	public List < InventoryContainers > allContainers;

	public BaseInventory currInventory;

	public void AddSlot(BaseItems item ) {

		if ( item is Potions ){
			
		}
		else if ( item is WeaponObject ){
			//StaticManager.UiInventory.UpdateStats(item.stats, );
		}
	}
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
