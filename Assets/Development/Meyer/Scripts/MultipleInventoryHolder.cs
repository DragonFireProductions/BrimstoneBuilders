using System.Collections;
using System.Collections.Generic;
using System.Linq;

using TMPro;

using UnityEngine;
using UnityEngine.Assertions;

public class MultipleInventoryHolder : MonoBehaviour {

	public List < PlayerInventory > alllables;
    [SerializeField] private WeaponItemList itemList; //WeaponListAsset set in inspector

    [HideInInspector] public static List<WeaponItem> WeaponAssetList; //public list of weapons from WeaponListAsset 

	public PlayerInventory inventory;

    public void Awake( ) {
		WeaponAssetList = itemList.itemList;
		alllables = new List < PlayerInventory >();
	}
	public void SwitchInventory(GameObject tab ) {
		string sub = tab.name.Substring( 0 , tab.name.Length - 3 );
		var _inventory = GetInventory( sub + "Inventory" );
		foreach ( var l_inventory in alllables ){
			l_inventory.parent.SetActive( l_inventory == _inventory );
		}
	}
    public WeaponItem GetItemFromAssetList(string name)
    {
        return WeaponAssetList.FirstOrDefault(_t => _t.objectName == name);
    }

    public void AddCompanionInventory(Companion companion ) {
		var tab = Instantiate(StaticManager.UiInventory.ItemsInstance.Tab);
		tab.name = companion.name + "Tab";
		tab.transform.SetParent(StaticManager.UiInventory.ItemsInstance.Tabs.gameObject.transform);
	    tab.transform.localScale = new Vector3(1,1,1);
		tab.SetActive(true);
	    tab.GetComponentInChildren < TextMeshProUGUI >( ).text = companion.name;
    }

	public PlayerInventory GetInventory(string parentName ) {
		foreach ( var l_playerInventory in alllables ){
			if (  l_playerInventory.parent.name == parentName ){
				return l_playerInventory;
			}
		}
		Assert.IsNotNull(null, "Cannot Find inventory parent with name" + parentName + " Line number : 29 - MultipleInventoryHolder");
		return null;
	}

	public void SendToCompanion(GameObject name) {
		string _name = name.name.ToString( );
		GetInventory(_name + "Inventory").PickedUpWeapons.Add(inventory.selectedObject);
		StaticManager.UiInventory.AddSlot(inventory.selectedObject, GetInventory(_name + "Inventory"));
		StaticManager.UiInventory.RemoveMainInventory(inventory.selectedObject, inventory);
		//inventory.PickedUpWeapons.Remove(inventory.selectedObject);

    }
    public void Equip()
    {
        inventory.selectedObject.name = "Sword";
         inventory.selectedObject.tag = "Weapon";
        inventory.character.attachedWeapon =  inventory.selectedObject;
         inventory.selectedObject.AttacheBaseCharacter = inventory.character;
        if (inventory.AttachedWeapons.Count > 0)
        {
           inventory.AttachedWeapons.Add(inventory.selectedObject);
            var ob = inventory.AttachedWeapons[0];
            inventory.AttachedWeapons.RemoveAt(0);
            StaticManager.UiInventory.AddSlot(ob, inventory);
            ob.attached = false;

            ob.transform.parent = GameObject.Find("Weapons").transform;
            ob.gameObject.name = ob.WeaponStats.objectName;
            ob.gameObject.SetActive(false);
        }

        inventory.selectedObject.GetComponent<BoxCollider>().enabled = false;

        StaticManager.UiInventory.RemoveMainInventory(inventory.selectedObject, inventory);
        inventory.selectedObject.gameObject.transform.position = inventory.character.cube.transform.position;
        inventory.selectedObject.gameObject.transform.rotation = inventory.character.cube.transform.rotation;
        inventory.selectedObject.gameObject.transform.SetParent(inventory.character.cube.transform);

        inventory.character.stats.IncreaseStats(inventory.selectedObject.WeaponStats);
        inventory.selectedObject.gameObject.SetActive(true);

    }
}
