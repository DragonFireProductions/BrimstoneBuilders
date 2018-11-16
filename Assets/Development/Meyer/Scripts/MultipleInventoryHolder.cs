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

	public Camera playerCam;

	public Vector3 prevPos;

	public Tab previousInventory;
	public void Awake( ) {
		WeaponAssetList = itemList.itemList;
		alllables       = new List < PlayerInventory >( );
		playerCam       = GameObject.Find( "PlayerCamera" ).GetComponent<Camera>();
	}

	public void Start( ) {
		inventory = StaticManager.Character.GetComponent < PlayerInventory >( );
	}
	public void SwitchInventory(Tab tab ) {
		if ( previousInventory ){

            previousInventory.companion.inventory.parent.gameObject.SetActive(false);
            previousInventory.GetComponent<Image>().color = Color.gray;
			previousInventory.companion.transform.position = prevPos;
		}
        
		StaticManager.UiInventory.UpdateStats(inventory.character.attachedWeapon.WeaponStats, StaticManager.UiInventory.ItemsInstance.WeaponInventoryStats);
        StaticManager.UiInventory.UpdateStats(inventory.character.stats, StaticManager.UiInventory.ItemsInstance.CharacterInventoryStats, false);
        tab.companion.inventory.parent.SetActive(true);
		tab.GetComponent < Image >( ).color = Color.red;
		inventory = tab.companion.inventory;
		StaticManager.UiInventory.UpdateStats(inventory.character.attachedWeapon.WeaponStats, StaticManager.UiInventory.ItemsInstance.WeaponInventoryStats);
		StaticManager.UiInventory.UpdateStats(inventory.character.stats, StaticManager.UiInventory.ItemsInstance.CharacterInventoryStats, false );
		previousInventory = tab;
		prevPos = inventory.character.transform.position;

        Vector3 characterpos = new Vector3(inventory.character.transform.position.x, 30, inventory.character.transform.position.z);
        inventory.character.transform.position = characterpos;
        Vector3 pos = characterpos + (inventory.character.transform.forward * 4);
        pos.y = 30.77f;
        playerCam.transform.position = pos;
        playerCam.transform.LookAt(inventory.character.transform.position + (inventory.transform.up * 0.77f));

    }
    public WeaponItem GetItemFromAssetList(string name)
    {
        return WeaponAssetList.FirstOrDefault(_t => _t.objectName == name);
    }

    public GameObject AddCompanionInventory(Companion companion ) {
		var tab = Instantiate(StaticManager.UiInventory.ItemsInstance.Tab);
	    tab.GetComponent < Tab >( ).companion = companion;
		tab.transform.SetParent(StaticManager.UiInventory.ItemsInstance.Tabs.gameObject.transform);
	    tab.transform.localScale = new Vector3(1,1,1);
		tab.SetActive(true);
	    tab.GetComponentInChildren < TextMeshProUGUI >( ).text = companion.name;
		StaticManager.tabManager.tabs.Add(tab.GetComponent<Tab>());

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
	public void SendToCompanion(GameObject name) {
		string _name = name.name.ToString( );
		GetInventory(_name + "Inventory").PickedUpWeapons.Add(inventory.selectedObject);
		StaticManager.UiInventory.AddSlot(inventory.selectedObject, GetInventory(_name + "Inventory"));
		StaticManager.UiInventory.RemoveMainInventory(inventory.selectedObject, inventory);

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
			inventory.character.stats.ResetStats(ob.WeaponStats);
        }

        inventory.selectedObject.GetComponent<BoxCollider>().enabled = false;

        StaticManager.UiInventory.RemoveMainInventory(inventory.selectedObject, inventory);
        inventory.selectedObject.gameObject.transform.position = inventory.character.cube.transform.position;
        inventory.selectedObject.gameObject.transform.rotation = inventory.character.cube.transform.rotation;
        inventory.selectedObject.gameObject.transform.SetParent(inventory.character.cube.transform);

        inventory.character.stats.IncreaseStats(inventory.selectedObject.WeaponStats);
		StaticManager.UiInventory.UpdateStats(inventory.selectedObject.WeaponStats, StaticManager.UiInventory.ItemsInstance.WeaponInventoryStats);
	    StaticManager.UiInventory.UpdateStats(inventory.character.stats, StaticManager.UiInventory.ItemsInstance.CharacterInventoryStats, false);

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
