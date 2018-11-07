using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;

public class CompanionInventory : PlayerInventory {
    public static UIInventory uiInventory = StaticManager.UiInventory;

    [HideInInspector] public List<WeaponObject> MainInventoryList;

    [HideInInspector] public List<WeaponObject> PickedUpWeapons; //Current list of items the player has picked up

	public Companion companion;

    [HideInInspector] public WeaponObject selectedObject { get; set; }

    public List<UIItemsWithLabels> Slots;

    public void Awake() {
		base.Awake();
	    companion = gameObject.GetComponent < Companion >( );
    }

	public void Start( ) {
		StaticManager.inventories.alllables.Add(this);
	}
    //returns 
    public WeaponObject GetItemFromInventory(string name)
    {
        return PickedUpWeapons.FirstOrDefault(_t => _t.WeaponStats.objectName == name);
    }

	public void AddToInventory(WeaponObject obj)
	{
		StaticManager.UiInventory.AddSlot(obj, companion.inventory);
		PickedUpWeapons.Add(obj);
	}
	// Update is called once per frame
	void Update () {
		
	}
}
