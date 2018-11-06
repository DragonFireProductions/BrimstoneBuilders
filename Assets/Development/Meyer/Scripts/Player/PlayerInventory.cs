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
                Time.timeScale = 0;
            }

            if ( isInventoryActive == false ){
                StaticManager.UiInventory.ItemsInstance.PlayerUI.SetActive( false );
                Time.timeScale = 1;
            }
        }
    }
    public void Equip()
    {
        selectedObject.gameObject.SetActive(true);
        selectedObject.name = "Sword";
        selectedObject.tag = "Weapon";
        StaticManager.Character.attachedWeapon = selectedObject;
        if (StaticManager.UiInventory.AttachedWeapons.Count > 0)
        {
            StaticManager.UiInventory.AttachedWeapons.Add(selectedObject);
            var ob = StaticManager.UiInventory.AttachedWeapons[0];
            StaticManager.UiInventory.AttachedWeapons.RemoveAt(0);
            if (ob.isMainInventory)
            {
                StaticManager.UiInventory.AddSlot(ob);
                ob.attached = false;
            }
            else
            {
                StaticManager.UiInventory.AddBackpackSlot(ob);
            }

            ob.transform.parent = GameObject.Find("Weapons").transform;
            ob.gameObject.name = ob.WeaponStats.objectName;
            ob.gameObject.SetActive(false);
        }

        selectedObject.GetComponent<BoxCollider>().enabled = false;

        StaticManager.UiInventory.RemoveMainInventory(selectedObject);
        selectedObject.gameObject.transform.position = StaticManager.Character.Cube.transform.position;
        selectedObject.gameObject.transform.rotation = StaticManager.Character.Cube.transform.rotation;
        selectedObject.gameObject.transform.SetParent(StaticManager.Character.Cube.transform);

        StaticManager.Character.stats.IncreaseStats(selectedObject.WeaponStats);
    }

}