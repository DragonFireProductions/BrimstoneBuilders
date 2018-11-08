using System.Collections.Generic;
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


    public void Awake( ) {
        AttachedWeapons = new List < WeaponObject >();
        AttachedWeapons.Add(gameObject.transform.Find("Cube/Sword").gameObject.GetComponent<WeaponObject>());
        
        character = GetComponent < BaseCharacter >( );
        
        Slots = new List < UIItemsWithLabels >();
     
    }

    public void Start( ) {
        PickedUpWeapons.Add(gameObject.transform.Find("Cube/Sword").gameObject.GetComponent<WeaponObject>());
        parent = Instantiate( StaticManager.UiInventory.ItemsInstance.Inventory );
        parent.transform.SetParent(StaticManager.UiInventory.ItemsInstance.Inventory.gameObject.transform.parent);
        parent.name = gameObject.name + "Inventory";
        parent.GetComponent < Image >( ).enabled = false;
        parent.transform.position = StaticManager.UiInventory.ItemsInstance.Inventory.transform.position;
        parent.transform.localScale = new Vector3(1,1,1);
        StaticManager.inventories.alllables.Add(this);
        var send = Instantiate( StaticManager.UiInventory.ItemsInstance.SendCompanionButton );
        send.transform.position = StaticManager.UiInventory.ItemsInstance.SendCompanionButton.transform.position;
        send.transform.SetParent(StaticManager.UiInventory.ItemsInstance.SendCompanionButton.transform.parent);
        send.GetComponentInChildren<TextMeshProUGUI>().text = character.name;
        send.name = character.name;
        send.transform.localScale = new Vector3(1,1,1);
        send.SetActive(true);
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
                StaticManager.UiInventory.ItemsInstance.PlayerUI.SetActive( true );
                Time.timeScale = 0;
            }

            if ( isInventoryActive == false ){
                StaticManager.UiInventory.ItemsInstance.PlayerUI.SetActive( false );
                Time.timeScale = 1;
            }
        }
    }
   

}