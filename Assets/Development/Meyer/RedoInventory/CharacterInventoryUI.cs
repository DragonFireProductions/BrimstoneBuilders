using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

using Assets.Meyer.TestScripts.Player;

using TMPro;

using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Experimental.UIElements;

public class CharacterInventoryUI : MonoBehaviour {

	public GameObject PotionsInventory;

	public GameObject CharacterInventory;

	public GameObject WeaponInventory;

	public Tab CompanionSell;

	public GameObject SellButton;

	public GameObject BuyButton;


	public List < UIItemsWithLabels > weapons;

	public List < UIItemsWithLabels > potions;

	public UIItemsWithLabels ShopWeaponsText;

	public UIItemsWithLabels ShopCharacterText;
     
	public Companion companion;

	public Tab sendToButton;

	public Tab tab;

    public void Init(Companion _companion ) {

	    companion = _companion;
		weapons = new List < UIItemsWithLabels >();
	    potions = new List < UIItemsWithLabels >();
	    CharacterInventory = Instantiate( StaticManager.uiManager.CharacterInventory );

	    var a = Instantiate( StaticManager.uiManager.SendToButton, StaticManager.uiManager.SendToButton.transform.position, Quaternion.identity );

	    sendToButton = a.GetComponent < Tab >( );

	    sendToButton.companion = companion;

	    sendToButton.transform.Find( "Name" ).GetComponent < TextMeshProUGUI >( ).text = name;

	    CharacterInventory.transform.position = StaticManager.uiManager.CharacterInventory.transform.position;

		CharacterInventory.transform.SetParent(StaticManager.UiInventory.ItemsInstance.PlayerUI.transform);

		CharacterInventory.transform.localScale = new Vector3( 1,1,1);

	    CharacterInventory.name = companion.name + "Inventory";

	    PotionsInventory = CharacterInventory.transform.Find( "PotionsInventory" ).gameObject;

		PotionsInventory.SetActive(false);

	    WeaponInventory = CharacterInventory.transform.Find( "WeaponsInventory" ).gameObject;

		WeaponInventory.SetActive(true);

	    tab = Instantiate( StaticManager.uiManager.Tab ).GetComponent < Tab >( );

	    tab.gameObject.SetActive( false );

		tab.transform.position = StaticManager.uiManager.Tab.transform.position;

		tab.transform.SetParent(StaticManager.uiManager.tabParent.transform);

		tab.transform.localScale = new Vector3(1,1,1);

		tab.gameObject.SetActive(true);

	    tab.companion = companion;

	    sendToButton.companion = companion;

		sendToButton.gameObject.SetActive(true);

	    sendToButton.gameObject.transform.Find("Name").GetComponent < TextMeshProUGUI >( ).text = gameObject.name;
	
		sendToButton.transform.SetParent(StaticManager.uiManager.SendToWindow.transform);

	    sendToButton.transform.localScale = new Vector3(1, 1, 1);

        CharacterInventory.SetActive(false);

		CharacterInventory.transform.SetParent(StaticManager.uiManager.inventories.transform);

	   
    }

	public void AddToShop(Shop shop ) {

		gameObject.SetActive(false);
        var b = Instantiate(StaticManager.uiManager.CompanionSellStats);
       CompanionSell = b.GetComponent<Tab>();
       CompanionSell.companion = companion;
       CompanionSell.transform.position = StaticManager.uiManager.CompanionSellStats.transform.position;
       BuyButton = CompanionSell.gameObject.transform.Find("BuyButton").gameObject;
       SellButton = CompanionSell.gameObject.transform.Find("SellButton").gameObject;
	   CompanionSell.transform.SetParent(shop.ShopContainer.transform);
	   ShopCharacterText = CompanionSell.transform.Find( "CharacterStats" ).GetComponent < UIItemsWithLabels >( );
	   ShopWeaponsText = CompanionSell.transform.Find( "WeaponStats" ).GetComponent < UIItemsWithLabels >( );
	   ShopCharacterText.FindLabels();
	   ShopWeaponsText.FindLabels();
	   BuyButton.SetActive(true);
	   SellButton.SetActive(false);
	   CompanionSell.gameObject.SetActive(true);
	    StaticManager.RealTime.Companions.Remove(companion);
	}

	public void Enable( ) {
		tab.gameObject.SetActive(true);
		StaticManager.tabManager.tabs.Add(tab);
	}
	public void AddWeapon(BaseItems item ) {
        GameObject newlabel = Instantiate(StaticManager.uiManager.Weapon.gameObject);
		newlabel.GetComponent < Tab >( ).companion = companion;
		newlabel.GetComponent < Tab >( ).item = item;

		var l = newlabel.GetComponent < UIItemsWithLabels >( );

		l.obj = newlabel;

        l.obj.transform.position = StaticManager.uiManager.Weapon.gameObject.transform.position;

        l.obj.transform.SetParent(WeaponInventory.transform);

        l.obj.transform.localScale = new Vector3(1, 1, 1);

		l.item = item;

		l.FindLabels();

		companion.inventory.PickedUpWeapons.Add(l.item as WeaponObject);
        newlabel.SetActive(true);
        weapons.Add(l);
    }

	public void AddPotion(BaseItems item ) {
        GameObject newlabel = Instantiate(StaticManager.uiManager.Potion.gameObject);
		newlabel.GetComponent < Tab >( ).companion = companion;
		newlabel.GetComponent < Tab >( ).item = item;
        var l = newlabel.GetComponent<UIItemsWithLabels>();

		l.obj = newlabel;

		l.item = item;

        l.obj.transform.position = StaticManager.uiManager.Potion.gameObject.transform.position;

        l.obj.transform.SetParent(PotionsInventory.transform);

        l.obj.transform.localScale = new Vector3(1, 1, 1);

		l.FindLabels();

		item.AttachedCharacter = companion;
		
        newlabel.SetActive(true);
        potions.Add(l);
    }

	public void RemoveObject(BaseItems item ) {
		if ( item is WeaponObject ){
            for (int i = 0; i < weapons.Count; i++)
            {
                if (weapons[i].item == item)
                {
	                weapons[i].obj.SetActive(false);
                }
            }
        }
        else if (item is Potions)
        {
            for (int i = 0; i < potions.Count; i ++)
            {
                if (potions[i].item == item)
                {
					Destroy(potions[i].obj);
                    potions.RemoveAt(i);
                }
            }
        }

    }

	public void DeleteObject(BaseItems item ) {
        if (item is WeaponObject)
        {
            for (int i = 0; i < weapons.Count; i++)
            {
                if (weapons[i].item == item){
	                var a = weapons[ i ].obj;
					weapons.RemoveAt(i);
                    Destroy(a);
                }
            }
        }
        else if (item is Potions)
        {
            for (int i = 0; i < potions.Count; i++)
            {
                if (potions[i].item == item)
                {
                    Destroy(potions[i].obj);
                    potions.RemoveAt(i);
                }
            }
        }
    }

	public void EnableContainer(BaseItems item ) {
		foreach ( var l_uiItemsWithLabelse in weapons ){
			if ( l_uiItemsWithLabelse.item == item ){
				l_uiItemsWithLabelse.obj.SetActive(true);
				item.gameObject.SetActive(false);
			}
		}
	}

	public void ShowInventory( ) {
		CharacterInventory.SetActive(true);
	}

    // Use this for initialization
    public void UpdateItem(UIItemsWithLabels instanceToUpdate, BaseItems _object)
    {
        for (int i = 0; i < instanceToUpdate.Labels.Count; i++)
        {
			Assert.IsNotNull(_object, _object.name + " is null ");
            var a = _object.stats[ instanceToUpdate.Labels[ i ].name ] ?? _object[ instanceToUpdate.Labels[ i ].name ];
			Assert.IsNotNull(a, instanceToUpdate.Labels[i].name + " is null for " + _object.name);
            instanceToUpdate.Labels[i].labelText.text = a.ToString();
			
        }
    }
    public void UpdateItem() {
	    foreach ( var l_t in weapons ){
		    for ( int j = 0 ; j < l_t.Labels.Count ; j++ ){
			    l_t.obj.SetActive( l_t.item != companion.attachedWeapon );
			    var a = l_t.item.stats[ l_t.Labels[ j ].name ];
			    l_t.Labels[ j ].labelText.text = a.ToString( );

			    if (l_t.GetComponent<Tab>().imageContainer && l_t.item.stats.icon ){
				    l_t.GetComponent<Tab>().imageContainer.texture = l_t.item.stats.icon;
			    }
		    }
	    }
    }
    public void UpdateCharacter(UIItemsWithLabels instance)
    {
	    for ( int i = 0 ; i < instance.Labels.Count ; i++ ){

		    var a = companion.stats[instance.Labels[i].name] ?? companion[ instance.Labels[ i ].name ];
		    float l_num;
		    float.TryParse( a.ToString( ) , out l_num );
		    int p = ( int )l_num;
            instance.Labels[i].labelText.text = p.ToString();
        }
    }
    public void UpdatePotions( ) {
        for (int i = 0; i < potions.Count; i++)
        {
            for (int j = 0; j < potions[i].Labels.Count; j++)
            {
                var a = potions[i].item.stats[potions[i].Labels[j].name];
                potions[i].Labels[j].labelText.text = a.ToString();

                if (potions[i].GetComponent<Tab>().imageContainer && potions[i].item.stats.icon)
                {
                    potions[i].GetComponent<Tab>().imageContainer.texture = potions[i].item.stats.icon;
                }
            }
        }
    }
}
