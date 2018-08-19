using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CreateAssetMenu(fileName = "Armor", menuName = "Inventory/Armor", order = 1)]
public class Armor : ScriptableObject
{
    public enum EquipSlot { Head, Pauldrons, Cuirass, Gauntlets, Greaves, Boots, BackorCape, Rings, Amulet };
    public enum WeightClass { Light, Medium, Heavy};
    public string objectName = "New Armor";
    public int weight = 0;
    public int durability = 0;
    public int value = 0;
    public int baseArmor = 0;
    public EquipSlot equipSlot;
    public WeightClass weightClass;
    public Texture2D icon = null;
    public bool isIndestructible = false;
    public bool isStackable = false;
    public bool destroyOnUse = false;

    public static Armor Create(string _objectName)
    {
        Armor asset = ScriptableObject.CreateInstance<Armor>();
        AssetDatabase.CreateAsset(asset, "Assets/Meyer/TestScripts/InventoryWindowStuff/ArmorAssets/" + _objectName + ".asset");
        AssetDatabase.SaveAssets();
        return asset;
    }
}
