using System.Collections;
using System.Collections.Generic;

using TMPro;

using UnityEngine;
using UnityEngine.EventSystems;

public class BackpackContainerScript : MonoBehaviour {

    private string name;

    private bool selected = false;


    /// <summary>
    /// Is called when UI slot is selected
    /// </summary>
    /// <param name="data">Data captured by Unity API</param>
    public void Start()
    {
        EventTrigger trigger = GetComponent<EventTrigger>();
        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerDown;
        entry.callback.AddListener((data) => { OnPointerDownDelegate((PointerEventData)data); });
        trigger.triggers.Add(entry);
    }
   

    public void Update( ) {

    }
    public void OnPointerDownDelegate(PointerEventData data)
    {
        if (data.currentInputModule.input.GetMouseButton(1))
        {
            StaticManager.uiInventory.itemsInstance.WeaponOptions.SetActive(false);
            name = gameObject.transform.Find("ItemName").GetComponentInChildren<TextMeshProUGUI>().text;
            StaticManager.inventory.selectedItem = StaticManager.inventory.get_weapon(name);
            StaticManager.uiInventory.ShowWeaponOptions(true);
           
        }
        else if (data.currentInputModule.input.GetMouseButton(0)){
            StaticManager.uiInventory.itemsInstance.PlayerUI.SetActive(false);
            StaticManager.uiInventory.isMainInventory = false;
            StaticManager.uiInventory.Dragging = true;
            StaticManager.uiInventory.ShowBackPackInventory(false);

            Debug.Log("OnPointerDownDelegate called.");
            name = gameObject.transform.Find("ItemName").GetComponentInChildren<TextMeshProUGUI>().text;
            StaticManager.uiInventory.selectedItem = StaticManager.inventory.get_weapon(name);
            selected = true;
        }
    }
}
