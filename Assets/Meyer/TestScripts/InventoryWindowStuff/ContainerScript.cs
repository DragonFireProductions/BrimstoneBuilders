﻿using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class ContainerScript : MonoBehaviour, IPointerDownHandler
{
    private string name;
    void Start()
    {
        
    }

    /// <summary>
    /// Is called when UI slot is selected
    /// </summary>
    /// <param name="data">Data captured by Unity API</param>
    public void OnPointerDown(PointerEventData data)
    {
        Debug.Log("OnPointerDownDelegate called.");
        name = gameObject.transform.Find("ItemName").GetComponentInChildren<TextMeshProUGUI>().text;
        UIInventory.instance.selected(PlayerInventory.inventory.get_weapon(name));
    }
}
