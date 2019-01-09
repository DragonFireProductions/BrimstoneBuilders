using System;
using System.Collections.Generic;

using TMPro;

using UnityEngine;
using UnityEngine.EventSystems;

public class ContainerScript : MonoBehaviour  {

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

   

    public virtual void OnPointerDownDelegate( PointerEventData data ) {
        if ( data.currentInputModule.input.GetMouseButton( 1 ) ){
        }
    }

    public virtual void OnPointerEnter(PointerEventData data ) {
        if ( gameObject.GetComponent < Tab >( )){
            var item =  gameObject.GetComponent < Tab >( ).item;
            StaticManager.uiManager.comparedInventoryWeapons.SetActive(true);
            StaticManager.inventories.inventory.character.inventoryUI.UpdateWeapon(StaticManager.uiManager.comparedInventoryWeapons.GetComponentInChildren<UIItemsWithLabels>(), item as WeaponObject);

        }
    }

    public virtual void OnPointerExit( PointerEventData data ) {
        StaticManager.uiManager.comparedInventoryWeapons.SetActive(false);
    }

}