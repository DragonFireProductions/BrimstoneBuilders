using UnityEngine;
using System.Collections;

[System.Serializable] //  Our Representation of an InventoryItem
public class WeaponItem
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
        Sword,
        Gun,
        Potion,
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
    
    public GameObject Projectile;

    public BaseItems item;

    public object this[string propertyName]
    {
        get { return this.GetType().GetField(propertyName).GetValue(this); }
        set { this.GetType().GetField(propertyName).SetValue(this, value); }
    }

}