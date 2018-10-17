using System.Collections;
using System.Collections.Generic;

using Assets.Meyer.TestScripts.Player;

using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class ContainerScript : MonoBehaviour  /*IPointerDownHandler*/ {

    private string name;

    private bool selected = false;

    private WeaponObject SelectedItem;

    /// <summary>
    /// Is called when UI slot is selected
    /// </summary>
    /// <param name="data">Data captured by Unity API</param>

    public void SelectObj() {
        StaticManager.uiInventory.Dragging = true;
        Debug.Log("OnPointerDownDelegate called.");
        name = gameObject.transform.Find("ItemName").GetComponentInChildren<TextMeshProUGUI>().text;
        StaticManager.uiInventory.selectedItem = StaticManager.inventory.get_weapon( name );
        //StaticManager.uiInventory.selected( StaticManager.inventory.get_weapon( name ) );\
        SelectedItem = Instantiate(StaticManager.inventory.get_weapon(name), this.gameObject.transform.position, StaticManager.character.transform.rotation);

        selected = true;

    }




}
