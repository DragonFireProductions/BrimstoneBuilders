using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class SwordType : WeaponObject {

    private bool hit;
    public override void Use()
    {
        base.Use();
        
        AttachedCharacter.AnimationClass.Play(AnimationClass.states.AttackTrigger);
        AttachedCharacter.attachedWeapon.AnimationClass.Play(AnimationClass.weaponstates.EnabledTrigger);
    }

    public void NotHit( ) {
        hit = false;
    }

    public void CheckHit( ) {
        if (  hit == false ){
            InstatiateFloatingText.InstantiateFloatingText( "MISS" , AttachedCharacter , Color.grey, new Vector3(0.5f, 0.5f, 0.5f) );
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
            InstatiateFloatingText.InstantiateFloatingText("MELEE++",character, Color.magenta, new Vector3(1,1,1));
    }

    public override void AssignDamage( ) {
        var a = AttachedCharacter as Companion;
        Damage = a.mele.CurrentLevel;
    }

    protected override void Start()
    {
        AnimationClass = gameObject.GetComponent<AnimationClass>();
        item = this.gameObject;
        type = SubClasses.Types.MELEE;
    }
}
