using System;
using System.Collections.Generic;

using TMPro;

using UnityEngine;
using UnityEngine.EventSystems;

public class ContainerScript : MonoBehaviour /*IPointerDownHandler*/ {

    private string name;

    private bool selected;

    private List < UIItemsWithLabels.Label > labels;

    public void Start( ) {

        var l_trigger = GetComponent < EventTrigger >( );
        var l_entry = new EventTrigger.Entry {eventID = EventTriggerType.PointerDown};
        l_entry.callback.AddListener( _data => { OnPointerDownDelegate( ( PointerEventData )_data ); } );
        l_trigger.triggers.Add( l_entry );
        var l_entry1 = new EventTrigger.Entry {eventID = EventTriggerType.PointerEnter};
        l_entry1.callback.AddListener(_data => {OnPointerEnter((PointerEventData)_data);});
        l_trigger.triggers.Add(l_entry1);
        var l_entry2 = new EventTrigger.Entry {eventID = EventTriggerType.PointerExit};
        l_entry2.callback.AddListener(_data => {OnPointerExit((PointerEventData) _data);});
        l_trigger.triggers.Add(l_entry2);
    }

    public void SelectObj( ) { }

    public void OnPointerDownDelegate( PointerEventData data ) {
        if ( data.currentInputModule.input.GetMouseButton( 1 ) ){
            var inventory = StaticManager.inventories.GetInventory( transform.parent.parent.name );
            StaticManager.inventories.inventory = inventory;
            name                                 = gameObject.transform.Find( "objectName" ).GetComponentInChildren < TextMeshProUGUI >( ).text;
            inventory.selectedObject = inventory.GetItemFromInventory( name );
            StaticManager.UiInventory.ShowWindow(StaticManager.UiInventory.ItemsInstance.Equip);
        }
    }

    public void OnPointerEnter(PointerEventData data ) {
        labels = StaticManager.UiInventory.ItemsInstance.WeaponInventoryStats.Labels;
        name = gameObject.transform.Find("objectName").GetComponentInChildren<TextMeshProUGUI>().text;
        var weapon = StaticManager.inventories.inventory.GetItemFromInventory( name ).WeaponStats;
        var currentWeapon = StaticManager.inventories.inventory.character.attachedWeapon.WeaponStats;
        WeaponItem stats = new WeaponItem();

        for ( int i = 0 ; i < labels.Count ; i++ ){
            var weaponInfo = weapon[ labels[ i ].name ];
            var currenInfo = currentWeapon[ labels[ i ].name ];
            int diff = Convert.ToInt32( weaponInfo.ToString( ) ) - Convert.ToInt32( currenInfo.ToString( ) );

            StaticManager.UiInventory.ItemsInstance.WeaponInventoryStats.Labels[ i ].labelText.text = diff.ToString( );
        }

    }

    public void OnPointerExit( PointerEventData data ) {
        StaticManager.UiInventory.UpdateStats(StaticManager.inventories.inventory.character.attachedWeapon.WeaponStats, StaticManager.UiInventory.ItemsInstance.WeaponInventoryStats);
    }

}