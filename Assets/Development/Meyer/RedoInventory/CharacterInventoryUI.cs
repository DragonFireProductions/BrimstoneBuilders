using System.Collections;
using System.Collections.Generic;
using System.Reflection;

using TMPro;

using UnityEngine;

public class CharacterInventoryUI : MonoBehaviour {

	public GameObject PotionsInventory;

	public GameObject CharacterInventory;

	public GameObject WeaponInventory;

	public List < UIItemsWithLabels > weapons;

	public List < UIItemsWithLabels > potions;

	public Companion companion;

	public Tab tab;

    public void Init(Companion _companion ) {
	    companion = _companion;
		weapons = new List < UIItemsWithLabels >();
		potions = new List < UIItemsWithLabels >();
	    CharacterInventory = Instantiate( StaticManager.uiManager.CharacterInventory );

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
		
        newlabel.SetActive(true);
        potions.Add(l);
    }

	public void RemoveObject(BaseItems item ) {
		if ( item is WeaponObject ){
            for (int i = 0; i < weapons.Count; i++)
            {
                if (weapons[i] == item)
                {
	                Destroy(weapons[i].obj);

                    weapons.RemoveAt(i);
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


		        var a = companion.inventory.PickedUpWeapons[i][ weapons[i].Labels[ j ].name ];
		        weapons[i].Labels[ j ].labelText.text = a.ToString( );
	        }
        }
    }

	public void UpdatePotions( ) {
        for (int i = 0; i < potions.Count; i++)
        {
            for (int j = 0; j < potions[i].Labels.Count; j++)
            {
                var a = companion.inventory.PickedUpPotions[i][potions[i].Labels[i].name];
                potions[i].Labels[i].labelText.text = a.ToString();
            }
        }
    }
}
