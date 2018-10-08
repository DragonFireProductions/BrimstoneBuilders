using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;

[System.Serializable]
public class WeaponItemList : ScriptableObject
{
    public List<WeaponItem> itemList;

    public WeaponItem get_item(string name)
    {
        for (int i = 0; i < itemList.Count; i++)
        {
            if (itemList[i].objectName.ToLower() == name.ToLower())
            {
                return itemList[i];
            }
        }

        return null;
    }
}