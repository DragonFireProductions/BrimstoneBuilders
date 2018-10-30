using UnityEngine;

public class Drag : MonoBehaviour {

    // Use this for initialization
    private void Update( ) {
        //If the left mouse button is clicked.
        if ( Input.GetMouseButton( 0 ) ){

            StaticManager.UiInventory.Dragging = true;
        }
    }

}