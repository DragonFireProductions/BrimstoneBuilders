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

    public List<companionBehaviors> behaviors;

    public void Awake( ) {
		WeaponAssetList = itemList.itemList;
		alllables       = new List < PlayerInventory >( );
		playerCam       = GameObject.Find( "PlayerCamera" ).GetComponent<Camera>();
	}

	public void Start( ) {
		behaviors = new List < companionBehaviors >();
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
		StaticManager.uiManager.inventoryCharacterStats.SetActive(true);

		tab.companion.inventoryUI.UpdateCharacter( StaticManager.uiManager.inventoryCharacterStats.GetComponentInChildren < UIItemsWithLabels >( ) );
		tab.companion.inventoryUI.UpdateItem(StaticManager.uiManager.WeaponInventoryStats.GetComponent<UIItemsWithLabels>(), tab.companion.attachedWeapon);

		if ( prev_inventory ){

            prev_inventory.companion.inventoryUI.CharacterInventory.SetActive(false);
            prev_inventory.tab.GetComponent<Image>().color = Color.gray;
			prev_inventory.companion.transform.position = prevPos;
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

		SwitchToWeapons();

    }
    public BaseItems GetItemFromAssetList(string name)
    {
        return WeaponAssetList.FirstOrDefault(_t => _t.objectName == name);
    }

	public void SendToOther(Tab tab ) {
		var obj = selectedObj;
		obj.AttachedCharacter = tab.companion;
		var cha = obj.AttachedCharacter as Companion;

		if ( obj is WeaponObject ){
			cha.inventoryUI.AddWeapon( obj );
		}

		if ( obj is Potions ){
			cha.inventoryUI.AddPotion(obj);
		}
		inventory.character.inventoryUI.DeleteObject(selectedObj);
		SwitchToWeapons();
        SwitchInventory(tab);
		SwitchToPotionsTab();


    }
    public void Use( ) {
		StaticManager.inventories.inventory.character.inventoryUI.UpdateItem(StaticManager.uiManager.WeaponInventoryStats.GetComponent<UIItemsWithLabels>(), selectedObj);
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
		inventory.character.inventoryUI.tab.gameObject.SetActive(false);
		Destroy(inventory.character.inventoryUI.tab);
		Destroy(inventory.parent.gameObject);
		if ( inventory == this.inventory ){
			SwitchInventory(StaticManager.tabManager.GetTab(StaticManager.Character));
		}
	}
	
}
