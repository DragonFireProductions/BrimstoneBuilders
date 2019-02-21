using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class SwordType : WeaponObject
{

    private bool hit;
    public override void Use()
    {
        base.Use();
        if (KnockBack)
        {
            AttachedCharacter.KnockBack(KnockBackAmount);

        }

        if (AttachedCharacter)
        {
            AttachedCharacter.AnimationClass.Play(AnimationClass.states.Attack);
        }

    }

    public override void Activate()
    {
        int ind = Random.Range(0, clips.Length - 1);
        DamageCollider(AttachedCharacter.enemy.GetComponent<Collider>());
        audio.clip = clips[ind];
        if (!audio.isPlaying)
        {
            audio.Play();
        }
    }

    public override void Deactivate()
    {
        //AttachedCharacter.leftHand.GetComponentInChildren < BoxCollider >( ).enabled = false;
    }

    public override void IncreaseSubClass(float amount)
    {

        base.IncreaseSubClass(amount);
        var character = AttachedCharacter as Companion;
        int level = (int)character.mele.CurrentLevel;
        character.mele.IncreaseLevel(amount);
        int currLevel = (int)character.mele.CurrentLevel;
        if (currLevel - level == 1)
            InstatiateFloatingText.InstantiateFloatingText("MELE++", character, Color.green, new Vector3(1, 1, 1), 0.2f);
    }

    public override void AssignDamage()
    {
        var a = AttachedCharacter as Companion;
        Damage = a.mele.CurrentLevel;
    }

    protected override void Start()
    {
        AnimationClass = gameObject.GetComponent<AnimationClass>();
        item = this.gameObject;
        type = SubClasses.Types.MELEE;
        if (AttachedCharacter)
        {
            AttachedCharacter.AnimationClass.SwitchWeapon(this);
        }
    }
}
