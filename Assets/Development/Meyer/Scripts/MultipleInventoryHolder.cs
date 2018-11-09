using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;

using TMPro;

using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

public class MultipleInventoryHolder : MonoBehaviour {

	public List < PlayerInventory > alllables;
    [SerializeField] private WeaponItemList itemList; //WeaponListAsset set in inspector

    [HideInInspector] public static List<WeaponItem> WeaponAssetList; //public list of weapons from WeaponListAsset 

	public PlayerInventory inventory;

	public List < GameObject > tabs;

	public Camera playerCam;

	public void Awake( ) {
		tabs            = new List < GameObject >( );
		WeaponAssetList = itemList.itemList;
		alllables       = new List < PlayerInventory >( );
		playerCam       = GameObject.Find( "PlayerCamera" ).GetComponent<Camera>();
	}

	public void SetPosOfCam( ) {
		Vector3 pos = inventory.character.transform.position + (inventory.character.transform.forward * 3);
		pos.y = 1;
        playerCam.transform.position = pos;
        playerCam.transform.LookAt(inventory.character.transform);
	}
	public void SwitchInventory(GameObject tab ) {
		string sub = tab.name.Substring( 0 , tab.name.Length - 3 );
		var _inventory = GetInventory( sub + "Inventory" );
		foreach ( var l_inventory in alllables ){
			if ( l_inventory == _inventory ){
				l_inventory.parent.SetActive(true);
				l_inventory.Tab.GetComponent < Image >( ).color = Color.red;
				inventory = l_inventory;
				SetPosOfCam();
				StaticManager.UiInventory.UpdateStats(inventory.character.attachedWeapon.WeaponStats, StaticManager.UiInventory.ItemsInstance.WeaponInventoryStats);
				StaticManager.UiInventory.UpdateStats(inventory.character.stats, StaticManager.UiInventory.ItemsInstance.CharacterInventoryStats );
			}
			else{
				l_inventory.parent.SetActive(false);
				l_inventory.Tab.GetComponent < Image >( ).color = Color.gray;
				l_inventory.character.gameObject.layer = 11;
			}
		}
	}
    public WeaponItem GetItemFromAssetList(string name)
    {
        return WeaponAssetList.FirstOrDefault(_t => _t.objectName == name);
    }

    public GameObject AddCompanionInventory(Companion companion ) {
		var tab = Instantiate(StaticManager.UiInventory.ItemsInstance.Tab);
		tab.name = companion.name + "Tab";
		tab.transform.SetParent(StaticManager.UiInventory.ItemsInstance.Tabs.gameObject.transform);
	    tab.transform.localScale = new Vector3(1,1,1);
		tab.SetActive(true);
	    tab.GetComponentInChildren < TextMeshProUGUI >( ).text = companion.name;
		tabs.Add(tab);

	    return tab;
    }

	public PlayerInventory GetInventory(string parentName ) {
		alllables.RemoveAll( item => item == null );
		foreach ( var l_playerInventory in alllables ){
			if (  l_playerInventory.parent.name == parentName ){
				return l_playerInventory;
			}
		}
		Assert.IsNotNull(null, "Cannot Find inventory parent with name" + parentName + " Line number : 29 - MultipleInventoryHolder");
		return null;
	}

	public GameObject GetTab(string CompanionName ) {
		foreach ( var l_gameObject in tabs ){
			if ( l_gameObject.name == CompanionName + "Tab" ){
				return l_gameObject;
			}
		}
		Assert.IsNotNull(null, "Tab for " + CompanionName + " couldn't be found");

		return null;
	}
	public void SendToCompanion(GameObject name) {
		string _name = name.name.ToString( );
		GetInventory(_name + "Inventory").PickedUpWeapons.Add(inventory.selectedObject);
		StaticManager.UiInventory.AddSlot(inventory.selectedObject, GetInventory(_name + "Inventory"));
		StaticManager.UiInventory.RemoveMainInventory(inventory.selectedObject, inventory);
		SwitchInventory(GetTab(name.name));
		//inventory.PickedUpWeapons.Remove(inventory.selectedObject);

    }

	public void Destroy(PlayerInventory inventory ) {
		Destroy(inventory.Tab);
		Destroy(inventory.parent);
		if ( inventory == this.inventory ){
			SwitchInventory(StaticManager.Character.inventory.Tab);
		}
		
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
		StaticManager.UiInventory.UpdateStats(inventory.selectedObject.WeaponStats, StaticManager.UiInventory.ItemsInstance.WeaponInventoryStats);
	    StaticManager.UiInventory.UpdateStats(inventory.character.stats, StaticManager.UiInventory.ItemsInstance.CharacterInventoryStats);

        inventory.selectedObject.gameObject.SetActive(true);

    }

	public GameObject setParent(GameObject character ) {
		var parent = Instantiate(StaticManager.UiInventory.ItemsInstance.Inventory);
        parent.transform.SetParent(StaticManager.UiInventory.ItemsInstance.Inventory.gameObject.transform.parent);
        parent.name = character.name + "Inventory";
        parent.GetComponent<Image>().enabled = false;
        parent.transform.position = StaticManager.UiInventory.ItemsInstance.Inventory.transform.position;
        parent.transform.localScale = new Vector3(1, 1, 1);

		return parent;
	}

	public GameObject setSendButton(Companion character ) {

        var send = Instantiate(StaticManager.UiInventory.ItemsInstance.SendCompanionButton);
        send.transform.position = StaticManager.UiInventory.ItemsInstance.SendCompanionButton.transform.position;
        send.transform.SetParent(StaticManager.UiInventory.ItemsInstance.SendCompanionButton.transform.parent);
        send.GetComponentInChildren<TextMeshProUGUI>().text = character.name;
        send.name = character.name;
        send.transform.localScale = new Vector3(1, 1, 1);
        send.SetActive(true);

		return send;
	}
}
