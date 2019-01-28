using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;

using Assets.Meyer.TestScripts.Player;

using Boo.Lang.Environments;

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

	public GameObject mapCamera;

	public GameObject PlayerUI;

	public GameObject weaponsPanel;

	

    public void Awake( ) {
		WeaponAssetList = itemList.itemList;
		alllables       = new List < PlayerInventory >( );
		playerCam       = GameObject.Find( "PlayerCamera" ).GetComponent<Camera>();
	}

	public void Start( ) {
		behaviors = new List < companionBehaviors >();
		inventory = StaticManager.Character.GetComponent < PlayerInventory >( );
	}

	public void CloseAll( ) {
		StaticManager.UiInventory.CloseAll();

	}
	public void SwitchToPotionsTab( ) {
		CloseAll();
        List<GameObject> windows = new List<GameObject>();


        windows.Add( inventory.character.inventoryUI.PotionsInventory);
		windows.Add(StaticManager.uiManager.playerUI);
		windows.Add(inventory.character.inventoryUI.CharacterInventory);

        StaticManager.UiInventory.ShowWindow(windows);
		inventory.character.inventoryUI.UpdatePotions();
    }

    public void SwitchToWeapons( ) {
		CloseAll();
		Time.timeScale = 0;
        List<GameObject> windows = new List<GameObject>();

		windows.Add(inventory.character.inventoryUI.CharacterInventory);
        windows.Add(StaticManager.uiManager.inventoryCharacterStats);
        windows.Add(inventory.character.inventory.WeaponInventory.inventoryObj);
		windows.Add(StaticManager.uiManager.WeaponWindow);
		windows.Add(StaticManager.uiManager.playerUI);

        StaticManager.UiInventory.ShowWindow(windows);
		
	    inventory.character.transform.position = prevPos;
        inventory.character.inventoryUI.UpdateItem();
    }

	public void SwitchToQuest( ) {
		CloseAll();
		Time.timeScale = 0;
        List<GameObject> windows = new List<GameObject>();

		windows.Add( StaticManager.questManager.questWindow );
		windows.Add(StaticManager.uiManager.playerUI);

        StaticManager.UiInventory.ShowWindow(windows);
	}
    public void SwitchArmorTab(Tab obj)
    {
		playerCam.gameObject.SetActive(true);
        StaticManager.questManager.questWindow.SetActive(false);
		StaticManager.uiManager.PlayerImage.SetActive(true);
		 StaticManager.uiManager.WeaponWindow.SetActive(false);

        if (inventory.armorInventory.currentArmorTab)
        {
            inventory.armorInventory.currentArmorTab.SetActive(false);
        }
        Color color = new Color(94.0f/255.0f, 39.0f/255.0f, 39.0f/255.0f, 1.0f);
	    if (  inventory.armorInventory.prev_tab ){
            inventory.armorInventory.prev_tab.GetComponent<Text>().color = color;
	    }
	    
        switch (obj.type)
        {

            case ArmorItem.Type.Head:
                inventory.armorInventory.Head.button.GetComponent<Text>().color = Color.red;
                inventory.armorInventory.Head.Switch(ref inventory.armorInventory.currentArmorTab);
	            inventory.armorInventory.prev_tab = inventory.armorInventory.Head.button;

                break;
            case ArmorItem.Type.Shoulder:
				inventory.armorInventory.Shoulder.button.GetComponent<Text>().color = Color.red;
                inventory.armorInventory.Shoulder.Switch(ref inventory.armorInventory.currentArmorTab);
				inventory.armorInventory.prev_tab = inventory.armorInventory.Shoulder.button;

                break;
            case ArmorItem.Type.Clothes:
				inventory.armorInventory.Clothes.button.GetComponent<Text>().color = Color.red;
                inventory.armorInventory.Clothes.Switch(ref inventory.armorInventory.currentArmorTab);
				inventory.armorInventory.prev_tab = inventory.armorInventory.Clothes.button;

                break;
            case ArmorItem.Type.Shoe:
				inventory.armorInventory.Shoes.button.GetComponent<Text>().color = Color.red;
                inventory.armorInventory.Shoes.Switch(ref inventory.armorInventory.currentArmorTab);
				 inventory.armorInventory.prev_tab = inventory.armorInventory.Shoes.button;

                break;
            case ArmorItem.Type.Belt:
				inventory.armorInventory.Belt.button.GetComponent<Text>().color = Color.red;
                inventory.armorInventory.Belt.Switch(ref inventory.armorInventory.currentArmorTab);
				inventory.armorInventory.prev_tab = inventory.armorInventory.Belt.button;

                break;
        }
		
	   
    }
    public void SwitchArmorTab(ArmorItem.Type obj)
    {
        playerCam.gameObject.SetActive(true);

        StaticManager.uiManager.PlayerImage.SetActive(true);
	    StaticManager.uiManager.WeaponWindow.SetActive(false);
        StaticManager.questManager.questWindow.SetActive(false);
        if (inventory.armorInventory.currentArmorTab)
        {
           inventory.armorInventory.currentArmorTab.SetActive(false);
        }

        switch (obj)
        {

            case ArmorItem.Type.Head:
                inventory.armorInventory.Head.Switch(ref inventory.armorInventory.currentArmorTab);

                break;
            case ArmorItem.Type.Shoulder:
                inventory.armorInventory.Shoulder.Switch(ref inventory.armorInventory.currentArmorTab);

                break;
            case ArmorItem.Type.Clothes:
               inventory.armorInventory.Clothes.Switch(ref inventory.armorInventory.currentArmorTab);

                break;
            case ArmorItem.Type.Shoe:
               inventory.armorInventory.Shoes.Switch(ref inventory.armorInventory.currentArmorTab);

                break;
            case ArmorItem.Type.Belt:
                inventory.armorInventory.Belt.Switch(ref inventory.armorInventory.currentArmorTab);

                break;
        }

    }
    public void SwitchToArmor( ) {
		CloseAll();
		Time.timeScale = 0;
		List <GameObject> windows = new List < GameObject >();

		windows.Add(inventory.character.inventoryUI.CharacterInventory);
		windows.Add(playerCam.gameObject);
		windows.Add(inventory.armorInventory.ArmorInventory);
		windows.Add(StaticManager.uiManager.playerUI);
		windows.Add(StaticManager.uiManager.PlayerImage);

		StaticManager.UiInventory.ShowWindow(windows);
		
        prevPos = inventory.character.transform.position;
        Vector3 characterpos = new Vector3(inventory.character.transform.position.x, 30, inventory.character.transform.position.z);
        inventory.character.transform.position = characterpos;
        Vector3 pos = characterpos + (inventory.character.transform.forward * 4);
        pos.y = 30.77f;
        playerCam.transform.position = pos;
        playerCam.transform.LookAt(inventory.character.transform.position + (inventory.transform.up * 0.77f));
		inventory.armorInventory.UpdateArmor();
		SwitchArmorTab(ArmorItem.Type.Head);
		
    }
    public void SwitchInventory(Tab tab ) {
		CloseAll();
		Time.timeScale = 0;
		StaticManager.uiManager.inventoryCharacterStats.SetActive(true);

		tab.companion.inventoryUI.UpdateCharacter( StaticManager.uiManager.inventoryCharacterStats.GetComponentInChildren < UIItemsWithLabels >( ) );
		tab.companion.inventoryUI.UpdateSubClassBar();

		if ( tab.companion.attachedWeapon ){
		tab.companion.inventoryUI.UpdateWeapon(StaticManager.uiManager.WeaponInventoryStats.GetComponent<UIItemsWithLabels>(), tab.companion.attachedWeapon);

        }

        if ( prev_inventory ){

            prev_inventory.companion.inventoryUI.CharacterInventory.SetActive(false);
            prev_inventory.tab.GetComponent<Image>().color = Color.gray;
			prev_inventory.companion.transform.position = prevPos;
        }

		tab.companion.inventoryUI.CharacterInventory.SetActive(true);
		tab.GetComponent < Image >( ).color = Color.red;
		inventory = tab.companion.inventory;

	    if ( prev_inventory ){
		     prev_inventory.sendToButton.gameObject.SetActive(true);
	    }
       
        inventory.character.inventoryUI.sendToButton.gameObject.SetActive(false);
        
		prev_inventory = inventory.character.inventoryUI;
		

		tab.companion.inventoryUI.UpdateItem();

		SwitchToWeapons();

    }

	public void SwitchToMap( ) {
		CloseAll();
       Time.timeScale = 0;
        StaticManager.map.ShowMap();
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
			var a = obj as WeaponObject;
			a.PickUp(cha);
			inventory.character.inventory.WeaponInventory.RemoveObject(a);
			inventory.character.inventory.WeaponInventory.DeleteObject(a);
			inventory.character.inventory.WeaponInventory.labels.Remove( a.label );
			Debug.Log("Labels count for " + inventory.character.name + " : " + inventory.character.inventory.WeaponInventory.labels.Count );
			cha.inventory.WeaponInventory.UpdateGrid();
			inventory.character.inventory.WeaponInventory.UpdateGrid();
			 Debug.Log(inventory.character.name + "Has sent weapon to " + cha.name + " Weapon: " + obj.name);
			Debug.Log("Labels count for " + inventory.character.name + " : " + inventory.character.inventory.WeaponInventory.labels.Count );
			Debug.Log("Labels count for " + cha.name + " : " + cha.inventory.WeaponInventory.labels.Count );
		}

		if ( obj is Potions ){
			cha.inventoryUI.AddPotion(obj);
		}

		if ( obj is ArmorItem){
			inventory.armorInventory.PickUp(obj);
		}
		inventory.character.inventoryUI.DeleteObject(selectedObj);
		SwitchToWeapons();
        SwitchInventory(tab);
		SwitchToPotionsTab();


    }
    public void Use( ) {
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

	public void Update( ) {
        if (Input.GetKeyDown(KeyCode.I))
        {

            if (StaticManager.uiManager.playerUI.gameObject.activeSelf)
            {
                StaticManager.inventories.CloseAll();

            }
            else
            {
                StaticManager.inventories.prevPos = StaticManager.Character.transform.position;
                StaticManager.UiInventory.ShowWindow(StaticManager.UiInventory.ItemsInstance.PlayerUI);
                inventory.character.inventoryUI.UpdateItem();
                StaticManager.inventories.SwitchInventory(StaticManager.Character.inventoryUI.tab);
                StaticManager.inventories.inventory.character.projector.gameObject.SetActive(false);
                StaticManager.inventories.SwitchToWeapons();
            }

        }

        if (Input.GetKeyDown(KeyCode.M))
        {
            if (StaticManager.map.map.activeSelf)
            {
                StaticManager.inventories.CloseAll();
            }
            else
            {
                StaticManager.inventories.SwitchToMap();
            }
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            if (StaticManager.questManager.questWindow.activeSelf)
            {
                StaticManager.inventories.CloseAll();
            }
            else
            {
                StaticManager.inventories.SwitchToQuest();
            }
        }

        if (Input.GetKeyDown(KeyCode.H) && StaticManager.Character.inventory.potions.Count > 0)
        {
            StaticManager.Character.inventory.potions[0].Cast(StaticManager.Character);
        }
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
