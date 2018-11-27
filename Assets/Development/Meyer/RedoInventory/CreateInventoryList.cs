using System.Collections;
using System.Collections.Generic;

using UnityEditor;

using UnityEngine;

public class CreateInventoryList {
    public static BaseItemList Create()
    {
        BaseItemList asset = ScriptableObject.CreateInstance<BaseItemList>();

        AssetDatabase.CreateAsset(asset, "Assets/BaseItemList.asset");
        AssetDatabase.SaveAssets();
        return asset;
    }

}
