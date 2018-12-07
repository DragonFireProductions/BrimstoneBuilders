﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class SwordType : WeaponObject {

    private bool hit;
    public override void Use()
    {
        base.Use();
        if (KnockBack)
        {
            AttachedCharacter.KnockBack(KnockBackAmount);

        }
        AttachedCharacter.AnimationClass.Play(AnimationClass.states.Attack);

    }

    public void NotHit( ) {
        hit = false;
    }

    public override void Activate( ) {
        StartCoroutine( collider( ) );
    }

    public IEnumerator collider( ) {
        AttachedCharacter.leftHand.GetComponentInChildren < BoxCollider >( ).enabled = true;
        yield return new WaitForSeconds(1);
        AttachedCharacter.leftHand.GetComponentInChildren < BoxCollider >( ).enabled = false;
    }
    public void CheckHit( ) {
        if (  hit == false ){
            InstatiateFloatingText.InstantiateFloatingText( "MISS" , AttachedCharacter , Color.grey, new Vector3(0.5f, 0.5f, 0.5f), 0.2f );
        }
    }
    protected override void OnTriggerEnter(Collider collider)
    {
        base.OnTriggerEnter(collider);

        if ( ( collider.tag == "Enemy" || collider.tag == "Companion" || collider.tag == "Player" ) && tag == "Weapon" ){
            hit = true;
        }
    }
    public override void IncreaseSubClass( float amount ) {
        
        base.IncreaseSubClass(amount);
        var character = AttachedCharacter as Companion;
        int level = (int)character.mele.CurrentLevel;
        character.mele.IncreaseLevel(amount);
        int currLevel = (int)character.mele.CurrentLevel;
        if (currLevel - level == 1)
            InstatiateFloatingText.InstantiateFloatingText("MELE++",character, Color.green, new Vector3(1,1,1), 0.2f);
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
