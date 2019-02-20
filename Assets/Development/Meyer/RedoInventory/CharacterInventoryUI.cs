using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

public class CharacterInventoryUI : MonoBehaviour {

    public GameObject PotionsInventory;

    public GameObject CharacterInventory;

    public CompanionContainer CompanionSell;
  

    public List < UIItemsWithLabels > potions;

    public UIItemsWithLabels ShopWeaponsText;

    public UIItemsWithLabels ShopCharacterText;

    public Companion companion;

    public Tab sendToButton;

    public Tab tab;

    public Text HealCount;

    public Text currentHealth;

    public void Init( Companion _companion ) {
        companion          = _companion;

        potions            = new List < UIItemsWithLabels >( );
       
        CharacterInventory = Instantiate( StaticManager.uiManager.CharacterInventory );
        

        var a = Instantiate( StaticManager.uiManager.SendToButton , StaticManager.uiManager.SendToButton.transform.position , Quaternion.identity );

        sendToButton = a.GetComponent < Tab >( );

        sendToButton.companion = companion;

        sendToButton.transform.Find( "Name" ).GetComponent < Text >( ).text = name;

        CharacterInventory.transform.position = StaticManager.uiManager.CharacterInventory.transform.position;

        CharacterInventory.transform.SetParent( StaticManager.UiInventory.ItemsInstance.PlayerUI.transform );

        CharacterInventory.transform.localScale = new Vector3( 1 , 1 , 1 );

        CharacterInventory.name = companion.name + "Inventory";

        PotionsInventory = CharacterInventory.transform.Find( "PotionsInventory" ).gameObject;

        HealCount = PotionsInventory.transform.Find( "HealCount" ).GetComponent < Text >( );

        currentHealth = PotionsInventory.transform.Find( "CurrentHealth" ).GetComponent < Text >( );

        PotionsInventory.SetActive( false );

        companion.inventory.WeaponInventory.character = companion;

        companion.inventory.WeaponInventory.Init();
        
        tab = Instantiate( StaticManager.uiManager.Tab ).GetComponent < Tab >( );

        tab.gameObject.SetActive( false );

        tab.transform.position = StaticManager.uiManager.Tab.transform.position;

        tab.transform.SetParent( StaticManager.uiManager.tabParent.transform );

        tab.transform.localScale = new Vector3( 1 , 1 , 1 );

        tab.gameObject.SetActive( true );

        tab.companion = companion;

        tab.GetComponentInChildren < Text >( ).text = name;

        sendToButton.companion = companion;

        sendToButton.gameObject.SetActive( true );

        sendToButton.gameObject.transform.Find( "Name" ).GetComponent < Text >( ).text = gameObject.name;

        sendToButton.transform.SetParent( StaticManager.uiManager.SendToWindow.transform );

        sendToButton.transform.localScale = new Vector3( 1 , 1 , 1 );

        CharacterInventory.SetActive( false );

        CharacterInventory.transform.SetParent( StaticManager.uiManager.inventories.transform );
        companion.inventory.armorInventory.companion = companion;
        companion.inventory.armorInventory.Init( );

        

    }
    

    public void RemoveFromShop( ) {
        Destroy( CompanionSell.gameObject );
    }

    public void Enable( ) {
        tab.gameObject.SetActive( true );
        StaticManager.tabManager.tabs.Add( tab );
    }
    
    public void AddPotion( BaseItems item ) {
        var newlabel = Instantiate( StaticManager.uiManager.Potion.gameObject );
        newlabel.GetComponent < Tab >( ).companion = companion;
        newlabel.GetComponent < Tab >( ).item      = item;
        var l = newlabel.GetComponent < UIItemsWithLabels >( );

        l.obj = newlabel;

        l.item = item;

        l.obj.transform.position = StaticManager.uiManager.Potion.gameObject.transform.position;

        l.obj.transform.SetParent( PotionsInventory.transform );

        l.obj.transform.localScale = new Vector3( 1 , 1 , 1 );

        l.FindLabels( );

        item.AttachedCharacter = companion;

        newlabel.SetActive( true );
        potions.Add( l );
    }

    

    public virtual void RemoveObject( BaseItems item ) {
        if ( item is WeaponObject ){
            companion.inventory.WeaponInventory.RemoveObject(item as WeaponObject);
        }
        if ( item is Potions ){
            for ( var i = 0 ; i < potions.Count ; i++ ){
                if ( potions[ i ].item == item ){
                    Destroy( potions[ i ].obj );
                    potions.RemoveAt( i );
                }
            }
        }
    }

    public void DeleteObject( BaseItems item ) {
        if ( item is WeaponObject ){
            companion.inventory.WeaponInventory.DeleteObject(item as WeaponObject);
        }
        else if ( item is Potions ){
            for ( var i = 0 ; i < potions.Count ; i++ ){
                if ( potions[ i ].item == item ){
                    Destroy( potions[ i ].obj );
                    potions.RemoveAt( i );
                }
            }
        }
    }

    public void EnableContainer( BaseItems item ) {
        var l_o = item as WeaponObject;

        if ( l_o != null ){
           companion.inventory.WeaponInventory.EnableContainer(l_o);
        }
    }

    // Use this for initialization

    public void UpdateLevel( UIItemsWithLabels instanceToUpdate ) {
        for ( var i = 0 ; i < instanceToUpdate.Labels.Count ; i++ ){ }
    }

    public void UpdateItem( ) {
        companion.inventory.WeaponInventory.UpdateItem();
    }

    public void UpdateSubClassBar( ) {
        var a = companion.mele.CurrentLevel - ( int )companion.mele.CurrentLevel;
        StaticManager.uiManager.meleBar.fillAmount = a;

        StaticManager.uiManager.rangeBar.fillAmount = companion.range.CurrentLevel - ( int )companion.range.CurrentLevel;

        StaticManager.uiManager.magicBar.fillAmount = companion.magic.CurrentLevel - ( int )companion.magic.CurrentLevel;
    }

    public void UpdateCharacter
        ( UIItemsWithLabels instance ) {
        for ( var i = 0 ; i < instance.Labels.Count ; i++ ){
            float  a = 0;
            string b;
            ;

            if ( instance.Labels[ i ].name.ToLower( ) == "mele" ){
                a = companion.mele.CurrentLevel;
                var c = ( int )a;
                b = c.ToString( );
            }
            else if ( instance.Labels[ i ].name.ToLower( ) == "range" ){
                a = companion.range.CurrentLevel;
                var c = ( int )a;
                b = c.ToString( );
            }
            else if ( instance.Labels[ i ].name.ToLower( ) == "magic" ){
                a = companion.range.CurrentLevel;
                var c = ( int )a;
                b = c.ToString( );
            }
            else if ( instance.Labels[ i ].name.ToLower( ) == "charactername" ){
                b = companion.characterName;
            }
            else{
                float p;
                var   d = companion.stats[ instance.Labels[ i ].name ] ?? companion[ instance.Labels[ i ].name ];
                float.TryParse( d.ToString( ) , out p );
                a = ( int )p;
                var c = ( int )a;
                b = c.ToString( );
            }

            instance.Labels[ i ].labelText.text = b;
        }
    }

    public void UpdateWeapon( UIItemsWithLabels item , WeaponObject weapon ) {
        companion.inventory.WeaponInventory.UpdateWeapon(item, weapon);
    }
    public void UpdatePotions( ) {
        GameObject GO = GameObject.Find("PotionsUse (1)");
        Button send = GO.GetComponentInChildren<Button>();
        if (StaticManager.currencyManager.companions > 0)
        {
            send.interactable = true;
        }
        else
        {
            send.interactable = false;
        }
        for ( var i = 0 ; i < potions.Count ; i++ ){
            for ( var j = 0 ; j < potions[ i ].Labels.Count ; j++ ){
                var a = potions[ i ].item.stats[ potions[ i ].Labels[ j ].name ];
                potions[ i ].Labels[ j ].labelText.text = a.ToString( );

                if ( potions[ i ].GetComponent < Tab >( ).imageContainer && potions[ i ].item.stats.icon ){
                    potions[ i ].GetComponent < Tab >( ).imageContainer.texture = potions[ i ].item.stats.icon;
                }
            }
        }
    }

}