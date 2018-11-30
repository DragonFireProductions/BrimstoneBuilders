using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class SwordType : WeaponObject {
    
    public override void Use()
    {
        base.Use();
        if (KnockBack)
        {
            AttachedCharacter.KnockBack(KnockBackAmount);

        }
        AttachedCharacter.AnimationClass.Play(AnimationClass.states.AttackTrigger);
        AttachedCharacter.attachedWeapon.AnimationClass.Play(AnimationClass.weaponstates.EnabledTrigger);
    }

    public override void IncreaseSubClass( float amount ) {
        
        base.IncreaseSubClass(amount);
        var character = AttachedCharacter as Companion;
        int level = (int)character.mele.CurrentLevel;
        character.mele.IncreaseLevel(amount);
        int currLevel = (int)character.mele.CurrentLevel;
        if (currLevel - level == 1)
            InstatiateFloatingText.InstantiateFloatingText("MELE++",character, Color.green);
    }

    public override void AssignDamage( ) {
        var a = AttachedCharacter as Companion;
        Damage = a.mele.CurrentLevel;
    }

    protected override void Start()
    {
        AnimationClass = gameObject.GetComponent<AnimationClass>();
        item = this.gameObject;
        type = SubClasses.Types.MELE;
    }
}
