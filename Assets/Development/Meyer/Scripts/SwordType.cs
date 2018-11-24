using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class SwordType : WeaponObject {
   

    public override void Attack(BaseCharacter enemy)
    {

        AttacheBaseCharacter.AnimationClass.Play(AnimationClass.states.AttackTrigger);
        AttacheBaseCharacter.attachedWeapon.AnimationClass.Play(AnimationClass.weaponstates.EnabledTrigger);
    }

    protected override void Start()
    {
        AnimationClass = gameObject.GetComponent<AnimationClass>();
        weaponStats = StaticManager.inventories.GetItemFromAssetList(weaponName);
        Assert.IsNotNull(weaponStats, "WeaponItem name not added in inspector " + gameObject.name);
        weapon = this.gameObject;
    }
}
