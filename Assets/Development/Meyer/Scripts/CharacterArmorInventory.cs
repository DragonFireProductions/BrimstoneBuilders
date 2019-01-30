using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

public class CharacterArmorInventory : PlayerInventory {

    public List < UIItemsWithLabels > armor;

    public GameObject ArmorInventory;

    public ArmorStuff Belt;

    public ArmorStuff Clothes;

    public Companion companion;

    public GameObject currentArmorTab;

    public ArmorStuff Head;

    [ HideInInspector ] public List < ArmorItem > pickedupArmor;

    public ArmorStuff Shoes;

    public ArmorStuff Shoulder;

    public Button prev_tab;

    // Use this for initialization
    public void Init( ) {
        armor = new List < UIItemsWithLabels >( );

        ArmorInventory = companion.inventoryUI.CharacterInventory.transform.Find( "ArmorInventory" ).gameObject;

        ArmorInventory.SetActive( false );
        Clothes  = new ArmorStuff( "Clothes" ,  "ClothesCount" , "ClothesB",  ArmorInventory );
        Shoulder = new ArmorStuff( "Shoulder" , "ShoulderCount", "ShoulderB" , ArmorInventory );
        Head     = new ArmorStuff( "Head" ,     "HeadCount" , "HeadB",     ArmorInventory );
        Belt     = new ArmorStuff( "Belt" ,     "BeltCount" , "BeltB" ,    ArmorInventory );
        Shoes    = new ArmorStuff( "Shoes" ,    "ShoeCount" , "ShoesB",     ArmorInventory );
    }

    public override void PickUp( BaseItems item ) {
        pickedupArmor.Add( item as ArmorItem );
        var i = item as ArmorItem;
        i.PickUp( companion );
        item.gameObject.SetActive( false );
    }

    public void UpdateArmor( ) {
        for ( var i = 0 ; i < armor.Count ; i++ ){
            for ( var j = 0 ; j < armor[ i ].Labels.Count ; j++ ){
                var a = armor[ i ].item.stats[ armor[ i ].Labels[ j ].name ];
                armor[ i ].Labels[ j ].labelText.text = a.ToString( );
            }
        }
    }

    // Update is called once per frame
    private void Update( ) { }

    public override void Close( ) {
        List <GameObject > window = new List < GameObject >();

        window.Add(StaticManager.inventories.playerCam.gameObject);
        window.Add(ArmorInventory.gameObject);
        window.Add(StaticManager.uiManager.PlayerImage);

        StaticManager.UiInventory.ItemsInstance.openedWindow.Add(window);
        StaticManager.inventories.playerCam.gameObject.SetActive(false);
        ArmorInventory.gameObject.SetActive(false);

        if ( StaticManager.inventories.prevPos != Vector3.zero ){
            character.transform.position = StaticManager.inventories.prevPos;
            StaticManager.uiManager.PlayerImage.SetActive(false);
        }
    }
}

public class ArmorStuff {

    public List < BaseItems > items = new List < BaseItems >( );

    public List < GameObject > labels = new List < GameObject >( );

    public GameObject parent;

    public Text text;

    public Button button;
    public ArmorStuff( string objName , string textName, string buttonName , GameObject armorInventory ) {
        text   = armorInventory.transform.Find( textName ).GetComponent < Text >( );
        parent = armorInventory.transform.Find( objName ).gameObject;
        parent.SetActive( false );
        button = armorInventory.transform.Find( buttonName ).GetComponentInChildren< Button >( );
    }

    public void Add( GameObject obj , BaseItems item ) {
        obj.transform.SetParent( parent.transform );
        items.Add( item );
        obj.transform.position   = StaticManager.uiManager.ArmorGrid[ items.Count - 1 ].transform.position;
        text.text                = items.Count.ToString( );
        obj.transform.localScale = new Vector3( 1 , 1 , 1 );
        labels.Add( obj );
    }

    public void Switch( ref GameObject current_game_object ) {
        parent.SetActive( true );
        current_game_object = parent;
        FixLayout( );
    }

    public void DeleteLabel( GameObject label ) {
        labels.Remove( label );
    }

    public void FixLayout( ) {
        for ( var i = 0 ; i < labels.Count ; i++ ){
            labels[ i ].gameObject.transform.position = StaticManager.uiManager.ArmorGrid[ i ].transform.position;
        }
    }

}