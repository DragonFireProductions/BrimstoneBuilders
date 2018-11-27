using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseItemList : ScriptableObject {

    public List<BaseItems> itemList;

    public BaseItems get_item(string name)
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
