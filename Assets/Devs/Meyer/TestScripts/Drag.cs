using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

using UnityEngine;

public class Drag : MonoBehaviour {

    
    // Use this for initialization
    void Update() {
        
        //If the left mouse button is clicked.
        if (Input.GetMouseButton(0))
        {
            //Get the mouse position on the screen and send a raycast into the game world from that position.
            Vector2 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(worldPoint, Vector2.zero);

            StaticManager.uiInventory.Dragging = true;
            //    name = gameObject.transform.Find("ItemName").GetComponentInChildren<TextMeshProUGUI>().text;
            //    StaticManager.uiInventory.selectedItem = Instantiate(StaticManager.inventory.get_weapon(name), this.gameObject.transform.position, StaticManager.character.transform.rotation);

        }
    }

}
