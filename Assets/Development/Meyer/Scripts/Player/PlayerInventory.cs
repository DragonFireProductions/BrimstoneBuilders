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

    [ HideInInspector ] public WeaponObject selectedObject { get; set; }

    public GameObject parent;

    public List<UIItemsWithLabels> Slots;

    public List < UIItemsWithLabels > PotionSlots;

    public BaseCharacter character;

    public List < WeaponObject > AttachedWeapons;

    public GameObject potionsContainer;

    public GameObject weaponsContainer;


    public int coinCount;


    public void Awake( ) {
        AttachedWeapons = new List < WeaponObject >();
        AttachedWeapons.Add(gameObject.transform.Find("Cube").gameObject.GetComponentInChildren<WeaponObject>());
        
        character = GetComponent < BaseCharacter >( );
        
        Slots = new List < UIItemsWithLabels >();
    }
    
    public void Start( ) {
        PickedUpWeapons.Add(gameObject.transform.Find("Cube").gameObject.GetComponentInChildren<WeaponObject>());
        parent = StaticManager.inventories.setParent( gameObject );
        StaticManager.inventories.alllables.Add(this);
        StaticManager.inventories.setSendButton( character as Companion );
        StaticManager.inventories.AddCompanionInventory( character as Companion );
        coinCount = 0;
    }
    //returns the first occurrence of an item from the WeaponAsset list 
   
    //returns 
    public WeaponObject GetItemFromInventory( string name ) {
        return PickedUpWeapons.FirstOrDefault( _t => _t.stats.objectName == name );
    }

    public void PickUp( WeaponObject weapon ) {
        PickedUpWeapons.Add( weapon );
        weapon.PickUp( );
    }

    public void PickUp( Potions potions ) {
        PickedUpPotions.Add(potions);
        potions.PickUp(character);
    }
    public void PickUpCoin(int _coinWorth)
    {
        StaticManager.currencyManager.AddCoins(_coinWorth);
    }
    
    private void Update( ) {
        if ( Input.GetButtonDown( "Inventory" ) ){
            isInventoryActive = !isInventoryActive;

            if ( isInventoryActive ){
                StaticManager.inventories.prevPos = StaticManager.Character.transform.position;
                StaticManager.inventories.SwitchToWeapons();
              StaticManager.UiInventory.ShowWindow(StaticManager.UiInventory.ItemsInstance.PlayerUI);
                StaticManager.inventories.SwitchInventory(StaticManager.tabManager.GetTab(StaticManager.Character));
                StaticManager.inventories.inventory.character.projector.gameObject.SetActive(false);
                Time.timeScale = 0;
            }
            if ( isInventoryActive == false ){
                StaticManager.inventories.inventory.character.transform.position = StaticManager.inventories.prevPos;
                StaticManager.UiInventory.ItemsInstance.ComparedStats.obj.SetActive(false);
                Time.timeScale = 1;
                StaticManager.Character.projector.gameObject.SetActive(true);

                StaticManager.UiInventory.CloseWindow();
            }
        }
    }

}