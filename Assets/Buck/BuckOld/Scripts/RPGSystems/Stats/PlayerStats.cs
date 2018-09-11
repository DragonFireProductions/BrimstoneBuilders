using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : CharacterStats
{
    public StatTest strength;
    public StatTest intelligence;
    public StatTest willpower;
    public StatTest agility;
    public StatTest speed;
    public StatTest endurance;
    public StatTest charisma;
    public StatTest luck;

    void Start ()
    {
        EquipmentManager.instance.onEquipmentChanged += OnEquipmentChanged;
	}

    void OnEquipmentChanged(Equipment newItem, Equipment oldItem)
    {
        if (newItem != null)
        {
            //armor.AddModifier(newItem.armorModifier);
            //damage.AddModifier(newItem.damageModifier);
        }

        if (oldItem != null)
        {
            //armor.RemoveModifier(oldItem.armorModifier);
            //damage.RemoveModifier(oldItem.damageModifier);
        }
    }

    public override void Die()
    {
        base.Die();
        PlayerManager.instance.KillPlayer();
    }
}
