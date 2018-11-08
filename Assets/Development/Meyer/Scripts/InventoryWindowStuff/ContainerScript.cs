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
            var inventory = StaticManager.inventories.GetInventory( transform.parent.parent.name );
            StaticManager.inventories.inventory = inventory;
            name                                 = gameObject.transform.Find( "objectName" ).GetComponentInChildren < TextMeshProUGUI >( ).text;
            inventory.selectedObject = inventory.GetItemFromInventory( name );
            StaticManager.UiInventory.ItemsInstance.Equip.SetActive(true);
        }
        
    }
    

}