using System.Collections;
using System.Collections.Generic;
using System.Reflection;

using TMPro;

using UnityEngine;
using UnityEngine.Experimental.UIElements;

public class CharacterInventoryUI : MonoBehaviour {

	public GameObject PotionsInventory;

	public GameObject CharacterInventory;

	public GameObject WeaponInventory;

	public List < UIItemsWithLabels > weapons;

	public List < UIItemsWithLabels > potions;

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
    public void UpdateItem(UIItemsWithLabels instanceToUpdate, ItemStats item)
    {
        for (int i = 0; i < instanceToUpdate.Labels.Count; i++)
        {
            var a = item[instanceToUpdate.Labels[i].name];
            instanceToUpdate.Labels[i].labelText.text = a.ToString();
        }
    }

    public void UpdateItem()
    {
        for (int i = 0; i < weapons.Count; i++)
        {
	        for ( int j = 0 ; j < weapons[ i ].Labels.Count ; j++ ){
		        if ( weapons[i].item == companion.attachedWeapon ){
			        weapons[ i ].obj.SetActive(false);
		        }
		        else{
			        weapons[ i ].obj.SetActive(true);
		        }
		        var a = weapons[i].item.stats[ weapons[i].Labels[ j ].name ];
		        weapons[i].Labels[ j ].labelText.text = a.ToString( );

		        if (weapons[i].GetComponent<Tab>().imageContainer  && weapons[i].item.stats.icon ){
			        weapons[i].GetComponent<Tab>().imageContainer.texture = weapons[ i ].item.stats.icon;
		        }
	        }
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
