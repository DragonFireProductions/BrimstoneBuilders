﻿using System.Collections.Generic;
using System.Linq;

using Assets.Meyer.TestScripts.Player;

using TMPro;

using UnityEngine;
using UnityEngine.UI;

public class PlayerInventory : MonoBehaviour {

    public static UIInventory uiInventory = StaticManager.UiInventory;


    [ HideInInspector ] public List < WeaponObject > BackPackInventoryList;

    [ HideInInspector ] public bool isInventoryActive;

    [ HideInInspector ] public List < WeaponObject > PickedUpWeapons; //Current list of items the player has picked up

    [ HideInInspector ] public WeaponObject selectedObject { get; set; }

    public GameObject parent;

    public List<UIItemsWithLabels> Slots;

    public BaseCharacter character;

    public List < WeaponObject > AttachedWeapons;

    public GameObject Tab;


    public void Awake( ) {
        AttachedWeapons = new List < WeaponObject >();
        AttachedWeapons.Add(gameObject.transform.Find("Cube/Sword").gameObject.GetComponent<WeaponObject>());
        
        character = GetComponent < BaseCharacter >( );
        
        Slots = new List < UIItemsWithLabels >();
    }
    
    public void Start( ) {
        PickedUpWeapons.Add(gameObject.transform.Find("Cube/Sword").gameObject.GetComponent<WeaponObject>());
        parent = StaticManager.inventories.setParent( gameObject );
        StaticManager.inventories.alllables.Add(this);
        StaticManager.inventories.setSendButton( character as Companion );
        Tab = StaticManager.inventories.AddCompanionInventory( character as Companion );

        if ( character is Character ){
            StaticManager.inventories.SwitchInventory(Tab);
        }
    }
    //returns the first occurance of an item from the WeaponAsset list 
   
    //returns 
    public WeaponObject GetItemFromInventory( string name ) {
        return PickedUpWeapons.FirstOrDefault( _t => _t.WeaponStats.objectName == name );
    }

    public void PickUp( WeaponObject weapon ) {
        PickedUpWeapons.Add( weapon );
        weapon.PickUp( );
    }
    
    private void Update( ) {
        if ( Input.GetButtonDown( "Inventory" ) ){
            isInventoryActive = !isInventoryActive;

            if ( isInventoryActive ){
              StaticManager.UiInventory.ShowWindow(StaticManager.UiInventory.ItemsInstance.PlayerUI);
                StaticManager.inventories.SetPosOfCam();
                Time.timeScale = 0;
            }
            if ( isInventoryActive == false ){
                StaticManager.UiInventory.CloseWindow();
                Time.timeScale = 1;
            }
        }
    }
   

}