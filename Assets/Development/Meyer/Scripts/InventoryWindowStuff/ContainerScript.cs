using TMPro;

using UnityEngine;
using UnityEngine.EventSystems;

public class ContainerScript : MonoBehaviour /*IPointerDownHandler*/ {

    private string name;

    private bool selected;

    public void Start( ) {
        var l_trigger = GetComponent < EventTrigger >( );
        var l_entry = new EventTrigger.Entry {eventID = EventTriggerType.PointerDown};
        l_entry.callback.AddListener( _data => { OnPointerDownDelegate( ( PointerEventData )_data ); } );
        l_trigger.triggers.Add( l_entry );
    }

    public void SelectObj( ) { }

    public void OnPointerDownDelegate( PointerEventData data ) {
        if ( data.currentInputModule.input.GetMouseButton( 1 ) ){
            StaticManager.UiInventory.ItemsInstance.WeaponOptions.SetActive( false );
            name                                 = gameObject.transform.Find( "ItemName" ).GetComponentInChildren < TextMeshProUGUI >( ).text;
            StaticManager.Inventory.SelectedItem = StaticManager.Inventory.get_weapon( name );

        }
        else if ( data.currentInputModule.input.GetMouseButton( 0 ) ){
            StaticManager.UiInventory.IsMainInventory = true;
            StaticManager.UiInventory.Dragging        = true;
            StaticManager.UiInventory.ItemsInstance.PlayerUI.SetActive( false );
            Debug.Log( "OnPointerDownDelegate called." );
            name                                   = gameObject.transform.Find( "ItemName" ).GetComponentInChildren < TextMeshProUGUI >( ).text;
            StaticManager.UiInventory.SelectedItem = StaticManager.Inventory.get_weapon( name );
            selected                               = true;
        }
    }

}