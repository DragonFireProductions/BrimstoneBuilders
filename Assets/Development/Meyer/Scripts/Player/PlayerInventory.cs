using System.Collections.Generic;
using System.Linq;

using UnityEngine;

public class PlayerInventory : MonoBehaviour {

    public static UIInventory uiInventory = StaticManager.UiInventory;

    [ HideInInspector ] public static List < WeaponItem > WeaponAssetList; //public list of weapons from WeaponListAsset 

    [ HideInInspector ] public List < WeaponObject > BackPackInventoryList;

    [ HideInInspector ] public bool isInventoryActive;

    [ SerializeField ] private WeaponItemList itemList; //WeaponListAsset set in inspector

    [ HideInInspector ] public List < WeaponObject > MainInventoryList;

    [ HideInInspector ] public List < WeaponObject > PickedUpWeapons; //Current list of items the player has picked up

    [ HideInInspector ] public WeaponObject selectedObject { get; set; }


    public void Awake( ) {
        WeaponAssetList = itemList.itemList;
    }
    //returns the first occurance of an item from the WeaponAsset list 
    public WeaponItem GetItemFromAssetList( string name ) {
        return WeaponAssetList.FirstOrDefault( _t => _t.objectName == name );
    }
    //returns 
    public WeaponObject GetItemFromInventory( string name ) {
        return PickedUpWeapons.FirstOrDefault( _t => _t.WeaponStats.objectName == name );
    }

    public void PickUp( WeaponObject weapon ) {
        PickedUpWeapons.Add( weapon );
        weapon.PickUp( );
    }

    public void MoveToBackPack( WeaponObject selectedItem ) {
        //StaticManager.UiInventory.ItemsInstance.WeaponOptions.SetActive( false );
        StaticManager.UiInventory.RemoveMainInventory( selectedItem );
        MainInventoryList.Remove( selectedItem );
        BackPackInventoryList.Add( selectedItem );
        StaticManager.UiInventory.AddBackpackSlot( selectedItem );
        selectedItem.MoveToBackPack( );
    }

    private void Update( ) {
        if ( Input.GetButtonDown( "Inventory" ) ){
            isInventoryActive = !isInventoryActive;

            if ( isInventoryActive ){
                StaticManager.UiInventory.ItemsInstance.PlayerUI.SetActive( true );
            }

            if ( isInventoryActive == false ){
                StaticManager.UiInventory.ItemsInstance.PlayerUI.SetActive( false );
            }
        }
    }

}