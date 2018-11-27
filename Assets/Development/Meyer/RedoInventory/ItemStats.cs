using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ItemStats : MonoBehaviour {

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

    public object this[string propertyName]
    {
        get {
            if ( this.GetType().GetField(propertyName) != null ){
            return this.GetType().GetField(propertyName).GetValue(this);
            }
            else{
                return null;
            }
        }
        set { this.GetType().GetField(propertyName).SetValue(this, value); }
    }

}
