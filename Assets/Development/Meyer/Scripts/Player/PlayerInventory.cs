using System.Collections.Generic;

using TMPro;

using UnityEngine;
using UnityEngine.UI;

public class PlayerInventory : MonoBehaviour {

    public static List < WeaponItem > List; //List of weapon stats defined by unity
    
    public static GameObject Ui; // UI stuff

    [ SerializeField ] public GameObject AttachedWeapon; // Primary weapon holder

    public List < WeaponObject > BackpackInventory;

    private bool isActive;
    
    public WeaponItemList Item; // Holder for Unity defined weapons list
    
    private GameObject itemSlot; // Slots for items

    public List < WeaponObject > Objects;

    public WeaponObject SelectedItem;

    private List < GameObject > uiList; // List of weaponItems currently pickedUp
    
    public List < WeaponObject > Weapons; //Contains list of picked up WeaponObjects

    // Use this for initialization
    private void Awake( ) {
        List = Item.itemList;

        uiList = new List < GameObject >( );
    }

    private void Start( ) {
       
    }
    
    public WeaponItem get_item( string _name ) {
        for ( var l_i = 0 ; l_i < List.Count ; l_i++ ){
            if ( List[ l_i ].objectName.ToLower( ) == _name.ToLower( ) ){
                return List[ l_i ];
            }
        }

        Debug.Log( _name + ": ASSET NOT FOUND" );

        return null;
    }
    
    public WeaponObject get_weapon( string _name ) {
        for ( var l_i = 0 ; l_i < Objects.Count ; l_i++ ){
            if ( Objects[ l_i ].WeaponStats.objectName == _name ){
                return Objects[ l_i ];
            }
        }

        return null;
    }
    
    private void Update( ) {
        if ( Input.GetButtonDown( "Inventory" ) ){
            isActive = !isActive;

            if ( isActive ){
                StaticManager.UiInventory.ItemsInstance.PlayerUI.SetActive( true );
            }

            if ( isActive == false ){
                StaticManager.UiInventory.ItemsInstance.PlayerUI.SetActive( false );
            }
        }
    }

    public void IncreaseStats( WeaponItem _item ) {
        var l_stat = StaticManager.Character.GetComponent < Stat >( );
        l_stat.Strength =  _item.durability;
        l_stat.Agility  += _item.attackSpeed;
        l_stat.Strength += _item.baseDamage;
    }
    public void Add( WeaponObject _item ) {
        _item.gameObject.SetActive( false );
        Objects.Add( _item );
        StaticManager.UiInventory.AddSlot( _item );
        Debug.Log( "Item: " + _item.WeaponStats.objectName + " has been added!" );
    }

    public void AddToBackpack( WeaponObject _item ) {
        _item.gameObject.SetActive( false );
        BackpackInventory.Add( _item );
        StaticManager.UiInventory.AddBackpackSlot( _item );
    }

    public void MoveToBackPack( ) {
        StaticManager.UiInventory.ItemsInstance.WeaponOptions.SetActive( false );
        StaticManager.UiInventory.Remove( SelectedItem );
        BackpackInventory.Add( SelectedItem );
        StaticManager.UiInventory.AddBackpackSlot( SelectedItem );
        StaticManager.UiInventory.ItemsInstance.BackPackUI.GetComponentInChildren < RawImage >( ).texture     = SelectedItem.WeaponStats.icon;
        StaticManager.UiInventory.ItemsInstance.BackPackUI.GetComponentInChildren < TextMeshProUGUI >( ).text = SelectedItem.WeaponStats.objectName;
    }

    public void ViewStats( ) {
        StaticManager.UiInventory.UpdateWeaponInventoryStats( SelectedItem.WeaponStats );
    }

}