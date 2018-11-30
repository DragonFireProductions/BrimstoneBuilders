using System.Collections;
using System.Collections.Generic;
using System.Reflection;

using TMPro;

using UnityEngine;
using UnityEngine.UI;

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

    public GameObject CompanionSellStats;

    public Tab SubclassHub;

    public Image meleBar;

    public Image magicBar;

    public Image rangeBar;
    public void Start( ) {
        
        WeaponInventoryStats.GetComponent<UIItemsWithLabels>().FindLabels();
        inventoryCharacterStats.GetComponentInChildren < UIItemsWithLabels >( ).FindLabels( );
        comparedInventoryWeapons.GetComponent < UIItemsWithLabels >( ).FindLabels( );
        CompanionSellStats.transform.Find("CharacterStats").GetComponent<UIItemsWithLabels>().FindLabels();
        CompanionSellStats.transform.Find("WeaponStats").GetComponent<UIItemsWithLabels>().FindLabels();
    }

    public object this[string propertyName]
    {
        get { return this.GetType().GetField(propertyName).GetValue(this); }
        set { this.GetType().GetField(propertyName).SetValue(this, value); }
    }

    public void Update() {
        SubclassHub.Level.text = ( ( int )StaticManager.Character.Level ).ToString( );
        SubclassHub.SubClass.text = StaticManager.Character.attachedWeapon.type.ToString( );
        SubclassHub.bar.fillAmount = StaticManager.Character.attachedWeapon.subClassLevel - ( int )StaticManager.Character.attachedWeapon.subClassLevel;
    }
    
}
