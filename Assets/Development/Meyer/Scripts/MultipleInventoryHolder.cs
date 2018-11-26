using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;

using TMPro;

using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

public class MultipleInventoryHolder : MonoBehaviour {

	public List < BaseInventory > inventoryList;

	public List < BaseItems > allItems;

	public List < PlayerInventory > alllables;
    [SerializeField] private BaseItemList itemList; //WeaponListAsset set in inspector

    [HideInInspector] public static List<BaseItems> WeaponAssetList; //public list of weapons from WeaponListAsset 

	public PlayerInventory inventory;

	public CharacterInventoryUI prev_inventory;

	public Camera playerCam;

	public Vector3 prevPos;

	public BaseItems selectedObj;
	public void Awake( ) {
		WeaponAssetList = itemList.itemList;
		alllables       = new List < PlayerInventory >( );
		playerCam       = GameObject.Find( "PlayerCamera" ).GetComponent<Camera>();
	}

	public void Start( ) {
		inventory = StaticManager.Character.GetComponent < PlayerInventory >( );
	}

	public void SwitchToPotionsTab( ) {
		inventory.character.inventoryUI.WeaponInventory.SetActive(false);
		inventory.character.inventoryUI.PotionsInventory.SetActive(true);
		inventory.character.inventoryUI.UpdatePotions();
		StaticManager.uiManager.WeaponWindow.SetActive(false);

    }

    public void SwitchToWeapons( ) {
		inventory.character.inventoryUI.WeaponInventory.SetActive(true);
		inventory.character.inventoryUI.PotionsInventory.SetActive( false );
		inventory.character.inventoryUI.UpdateItem();
		StaticManager.uiManager.WeaponWindow.SetActive(true);
    }
	public void SwitchInventory(Tab tab ) {
		tab.companion.inventoryUI.UpdateItem(StaticManager.uiManager.WeaponInventoryStats.GetComponent<UIItemsWithLabels>(), tab.companion.attachedWeapon.stats);
		if ( prev_inventory ){

            prev_inventory.companion.inventoryUI.CharacterInventory.SetActive(false);
            prev_inventory.tab.GetComponent<Image>().color = Color.gray;
			prev_inventory.companion.transform.position = prevPos;
        }

		if ( StaticManager.inventories.inventory.Slots.Count > 1 ){
          //  StaticManager.UiInventory.UpdateStats(inventory.character.attachedWeapon.stats, StaticManager.UiInventory.ItemsInstance.WeaponInventoryStats);
           // StaticManager.UiInventory.UpdateStats(inventory.character.stats, StaticManager.UiInventory.ItemsInstance.CharacterInventoryStats, false);
        }
		tab.companion.inventoryUI.CharacterInventory.SetActive(true);
		tab.GetComponent < Image >( ).color = Color.red;

		inventory = tab.companion.inventory;
		prevPos = inventory.character.transform.position;
		prev_inventory = inventory.character.inventoryUI;
        Vector3 characterpos = new Vector3(inventory.character.transform.position.x, 30, inventory.character.transform.position.z);
        inventory.character.transform.position = characterpos;
        Vector3 pos = characterpos + (inventory.character.transform.forward * 4);
        pos.y = 30.77f;
        playerCam.transform.position = pos;
        playerCam.transform.LookAt(inventory.character.transform.position + (inventory.transform.up * 0.77f));
		tab.companion.inventoryUI.UpdateItem();

    }
    public BaseItems GetItemFromAssetList(string name)
    {
        return WeaponAssetList.FirstOrDefault(_t => _t.objectName == name);
    }

	public void SendToOther(Tab tab ) {
		var obj = selectedObj;
		inventory.character.inventoryUI.DeleteObject(selectedObj);
		obj.AttachedCharacter = tab.companion;
		var cha = obj.AttachedCharacter as Companion;

		if ( obj is WeaponObject ){
			cha.inventoryUI.AddWeapon( obj );
		}

		if ( obj is Potions ){
			cha.inventoryUI.AddPotion(obj);
		}

		SwitchInventory(tab);

	}
	public void Use( ) {
		StaticManager.inventories.inventory.character.inventoryUI.UpdateItem(StaticManager.uiManager.WeaponInventoryStats.GetComponent<UIItemsWithLabels>(), selectedObj.stats);
		selectedObj.Attach();
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

	public void Destroy(PlayerInventory inventory ) {
		Destroy(StaticManager.tabManager.GetTab(inventory.character as Companion).gameObject);
		Destroy(inventory.parent.gameObject);
		if ( inventory == this.inventory ){
			SwitchInventory(StaticManager.tabManager.GetTab(StaticManager.Character));
		}
		
	}
    public void Equip()
    {
        inventory.selectedObject.name = "Sword";
         inventory.selectedObject.tag = "Weapon";
        inventory.character.attachedWeapon =  inventory.selectedObject;
         inventory.selectedObject.AttachedCharacter = inventory.character;
        if (inventory.AttachedWeapons.Count > 0)
        {
           inventory.AttachedWeapons.Add(inventory.selectedObject);
            var ob = inventory.AttachedWeapons[0];
            inventory.AttachedWeapons.RemoveAt(0);
            //StaticManager.UiInventory.AddSlot(ob, inventory);

            ob.transform.parent = GameObject.Find("Weapons").transform;
            ob.gameObject.name = ob.stats.objectName;
            ob.gameObject.SetActive(false);
			inventory.character.stats.ResetStats(ob.stats);
        }

        inventory.selectedObject.GetComponent<BoxCollider>().enabled = false;

        StaticManager.UiInventory.RemoveMainInventory(inventory.selectedObject, inventory);
        inventory.selectedObject.gameObject.transform.position = inventory.character.cube.transform.position;
        inventory.selectedObject.gameObject.transform.rotation = inventory.character.cube.transform.rotation;
        inventory.selectedObject.gameObject.transform.SetParent(inventory.character.cube.transform);

        inventory.character.stats.IncreaseStats(inventory.selectedObject.stats);
		//StaticManager.UiInventory.UpdateStats(inventory.selectedObject.stats, StaticManager.UiInventory.ItemsInstance.WeaponInventoryStats);
	    //StaticManager.UiInventory.UpdateStats(inventory.character.stats, StaticManager.UiInventory.ItemsInstance.CharacterInventoryStats, false);

        inventory.selectedObject.gameObject.SetActive(true);

    }
	
}
