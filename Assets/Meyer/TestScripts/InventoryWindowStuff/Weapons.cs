using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CreateAssetMenu(fileName = "Weapon", menuName = "Inventory/Weapon", order = 1)]
public class Weapons : ScriptableObject
{
    public enum EquipType { _H1, H2, Shield, Shoulder};
    public enum WeaponType { Type1, Type2, Type3, Type4};
    public string objectName = "New Weapon";
    public int weight = 0;
    public int durability = 0;
    public int value = 0;
    public int baseDamage = 0;
    public int attackSpeed = 0;
    public int reach = 0;
    public EquipType equipType;
    public WeaponType weaponType;
    public Texture2D icon = null;
    public bool isIndestructible = false;
    public bool isStackable = false;
    public bool destroyOnUse = false;

    public static Weapons Create(string _objectName)
    {
        Weapons asset = ScriptableObject.CreateInstance<Weapons>();
        AssetDatabase.CreateAsset(asset, "Assets/Meyer/TestScripts/InventoryWindowStuff/WeaponAssets/" + _objectName + ".asset" );
        AssetDatabase.SaveAssets();
        return asset;
    }
    // Use this for initialization

}

