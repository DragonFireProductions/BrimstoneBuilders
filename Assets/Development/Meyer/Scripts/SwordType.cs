using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class SwordType : WeaponObject {
   

    public override void Use(BaseCharacter enemy)
    {
        AttachedCharacter.AnimationClass.Play(AnimationClass.states.AttackTrigger);
        AttachedCharacter.attachedWeapon.AnimationClass.Play(AnimationClass.weaponstates.EnabledTrigger);
    }

    public override void IncreaseSubClass( float amount ) {
        
        base.IncreaseSubClass(amount);
        var a = AttachedCharacter as Companion;
        a.mele.IncreaseLevel(amount);
    }

    public override void AssignDamage( ) {
        var a = AttachedCharacter as Companion;
        Damage = a.mele.CurrentLevel;
    }

    protected override void Start()
    {
        AnimationClass = gameObject.GetComponent<AnimationClass>();
        item = this.gameObject;
    }
}
