﻿using System.Collections;
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
            StaticManager.UiInventory.ItemsInstance.WeaponOptions.SetActive(false);
            name = gameObject.transform.Find("ItemName").GetComponentInChildren<TextMeshProUGUI>().text;
            StaticManager.Inventory.SelectedItem = StaticManager.Inventory.get_weapon(name);
           
        }
        else if (data.currentInputModule.input.GetMouseButton(0)){
            StaticManager.UiInventory.ItemsInstance.PlayerUI.SetActive(false);
            StaticManager.UiInventory.IsMainInventory = false;
            StaticManager.UiInventory.Dragging = true;

            Debug.Log("OnPointerDownDelegate called.");
            name = gameObject.transform.Find("ItemName").GetComponentInChildren<TextMeshProUGUI>().text;
            StaticManager.UiInventory.SelectedItem = StaticManager.Inventory.get_weapon(name);
            selected = true;
        }
    }
}
