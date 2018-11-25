using System.Collections;
using System.Collections.Generic;
using System.Reflection;

using TMPro;

using UnityEngine;

public class UIInventoryManager : MonoBehaviour {

    public GameObject PotionsInventory;

    public GameObject CharacterInventory;

    public GameObject Potion;

    //public UIItemsWithLabels characterStats;

    public GameObject WeaponInventory;

    public GameObject Weapon;

    public GameObject Tab;

    public GameObject tabParent;

    public object this[string propertyName]
    {
        get { return this.GetType().GetField(propertyName).GetValue(this); }
        set { this.GetType().GetField(propertyName).SetValue(this, value); }
    }
    
}
