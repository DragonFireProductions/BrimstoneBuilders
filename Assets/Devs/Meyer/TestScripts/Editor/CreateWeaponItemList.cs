using UnityEngine;
using System.Collections;
using UnityEditor;

public class CreateWeaponItemList
{
    [MenuItem("Assets/Create/Inventory Item List")]
    public static WeaponItemList Create()
    {
        WeaponItemList asset = ScriptableObject.CreateInstance<WeaponItemList>();

        AssetDatabase.CreateAsset(asset, "Assets/InventoryItemList.asset");
        AssetDatabase.SaveAssets();
        return asset;
    }
}