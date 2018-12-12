using System.Collections;
using System.Collections.Generic;
using System.Linq;

using Assets.Meyer.TestScripts.Player;

using TMPro;

using UnityEngine;
using UnityEngine.UI;

public class PlayerInventory : MonoBehaviour {

    public static UIInventory uiInventory = StaticManager.UiInventory;


    [ HideInInspector ] public List < WeaponObject > BackPackInventoryList;

    [ HideInInspector ] public List < Potions > potions;

    [ HideInInspector ] public bool isInventoryActive;

    [ HideInInspector ] public List < WeaponObject > PickedUpWeapons; //Current list of items the player has picked up

    [ HideInInspector ] public List < Potions > PickedUpPotions;

    [ HideInInspector ] public List < ArmorItem > pickedupArmor;

    [ HideInInspector ] public WeaponObject selectedObject { get; set; }

    public GameObject parent;

    public List<UIItemsWithLabels> Slots;

    public List < UIItemsWithLabels > PotionSlots;

    public Companion character;

    public List < WeaponObject > AttachedWeapons;

    public GameObject potionsContainer;

    public int coinCount;


    public void Awake( ) {
        AttachedWeapons = new List < WeaponObject >();
       // AttachedWeapons.Add(gameObject.transform.Find("Cube").gameObject.GetComponentInChildren<WeaponObject>());
        character = GetComponent < Companion >( );
        Slots = new List < UIItemsWithLabels >();
    }
    
    public void Start( ) {
       // PickedUpWeapons.Add(gameObject.transform.Find("Cube").gameObject.GetComponentInChildren<WeaponObject>());
        coinCount = 0;
    }

    public void PickUp( WeaponObject weapon ) {
        PickedUpWeapons.Add( weapon );
        character.inventoryUI.AddWeapon(weapon);
        weapon.gameObject.SetActive(false);
    }

    public void PickUp( Potions potions ) {
        PickedUpPotions.Add(potions);
        character.inventoryUI.AddPotion(potions);
        potions.gameObject.SetActive(false);
    }

    public void PickUp( ArmorItem item ) {
        pickedupArmor.Add(item);
        character.inventoryUI.AddArmor(item);
        item.gameObject.SetActive(false);
    }
    public void PickUpCoin(int _coinWorth)
    {
        StaticManager.currencyManager.AddCoins(_coinWorth);
    }
    
    private void Update( ) {
        if ( Input.GetButtonDown( "Inventory" ) ){
            
                StaticManager.inventories.prevPos = StaticManager.Character.transform.position;
                StaticManager.UiInventory.ShowWindow( StaticManager.UiInventory.ItemsInstance.PlayerUI );
                character.inventoryUI.UpdateItem();
                StaticManager.inventories.SwitchInventory(StaticManager.Character.inventoryUI.tab);
                StaticManager.inventories.inventory.character.projector.gameObject.SetActive(false);
                StaticManager.inventories.SwitchToWeapons();
                Time.timeScale = 0;
        }
    }

}