using UnityEngine;
using System.Collections;

[System.Serializable] //  Our Representation of an InventoryItem
public class InventoryItem
{
    public enum EquipType
    {
        _H1,
        H2,
        Shield,
        Shoulder
    };

    public enum WeaponType
    {
        Type1,
        Type2,
        Type3,
        Type4
    };
    
        public string objectName;
        public int weight;
        public int durability;
        public int value;
        public int baseDamage;
        public int attackSpeed;
        public int reach;
        public EquipType equipType;
        public WeaponType weaponType;
        public Texture2D icon;
        public bool isIndestructible;
        public bool isStackable;
        public bool destroyOnUse;
        public AudioClip clip;
}