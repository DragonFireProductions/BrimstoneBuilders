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
            name                                 = gameObject.transform.Find( "objectName" ).GetComponentInChildren < TextMeshProUGUI >( ).text;
            StaticManager.Inventory.selectedObject = StaticManager.Inventory.GetItemFromInventory( name );
            StaticManager.UiInventory.ItemsInstance.Equip.SetActive(true);
        }
        //else if ( data.currentInputModule.input.GetMouseButton( 0 ) ){
        //    name = gameObject.transform.Find("objectName").GetComponentInChildren<TextMeshProUGUI>().text;
        //    StaticManager.UiInventory.ItemsInstance.PlayerUI.SetActive( false );
        //    StaticManager.UiInventory.SelectItem( StaticManager.Inventory.GetItemFromInventory(name));

        //}
    }
    

}