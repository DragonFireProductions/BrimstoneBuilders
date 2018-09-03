using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public static PlayerInventory inventory = null;
    public List<WeaponItem> weapons;
    public WeaponItemList item;

    public static List<WeaponItem> list;
    // Use this for initialization
    void Awake() {
        if (inventory == null)
        {
            inventory = this;
        }
        else if (inventory != this)
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);

        list = item.itemList;
    }
    public WeaponItem get_item(string name)
    {
        for (int i = 0; i < list.Count; i++)
        {
            if (list[i].objectName.ToLower() == name.ToLower())
            {
                return list[i];
            }
        }
        Debug.Log(name + ": ASSET NOT FOUND");
        return null;
    }

    // Update is called once per frame
    void Update () {
		
	}

    public void add(WeaponItem item)
    {
        weapons.Add(item);
        Debug.Log("Item: " + item.objectName + " has been added!");
        Debug.Log("Item's: attack speed is: " + item.attackSpeed);
    }

    public void remove(string name)
    {
        for (int i = 0; i < weapons.Count; i++)
        {
            if (weapons[i].objectName == name)
            {
                weapons.RemoveAt(i);
            }
        }
    }
}
