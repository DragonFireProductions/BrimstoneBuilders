using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class SwordType : WeaponObject {
   

    public override void Attack(BaseCharacter enemy)
    {

        AttachedCharacter.AnimationClass.Play(AnimationClass.states.AttackTrigger);
        AttachedCharacter.attachedWeapon.AnimationClass.Play(AnimationClass.weaponstates.EnabledTrigger);
    }

    protected override void Start()
    {
        AnimationClass = gameObject.GetComponent<AnimationClass>();
        stats = StaticManager.inventories.GetItemFromAssetList(objectName);
        Assert.IsNotNull(stats, "WeaponItem name not added in inspector " + gameObject.name);
        item = this.gameObject;
    }
}
