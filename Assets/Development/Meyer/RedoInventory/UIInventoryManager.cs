using System.Collections;
using System.Collections.Generic;
using System.Reflection;

using TMPro;

using UnityEngine;

public class UIInventoryManager : MonoBehaviour {

    public GameObject PotionsInventory;

    public GameObject CharacterInventory;

    public Tab Potion;

    //public UIItemsWithLabels characterStats;

    public GameObject WeaponInventory;

    public Tab Weapon;

    public GameObject Tab;

    public GameObject tabParent;

    public GameObject SendToWindow;

    public GameObject SendToButton;

    public GameObject WeaponInventoryStats;

    public GameObject WeaponWindow;

    public GameObject inventories;

    public GameObject inventoryCharacterStats;

    public GameObject comparedInventoryWeapons;

    public void Start( ) {
        WeaponInventoryStats.GetComponent<UIItemsWithLabels>().FindLabels();
        inventoryCharacterStats.GetComponentInChildren<UIItemsWithLabels>().FindLabels();
        comparedInventoryWeapons.GetComponentInChildren<UIItemsWithLabels>().FindLabels();
      
    }

    public object this[string propertyName]
    {
        get { return this.GetType().GetField(propertyName).GetValue(this); }
        set { this.GetType().GetField(propertyName).SetValue(this, value); }
    }
    
}
