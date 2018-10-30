using System;
using System.Collections;
using System.Collections.Generic;

using Kristal;

using TMPro;

using UnityEngine;

public class Companion : BaseCharacter {

    [ SerializeField ] public List < Enemy > enemies; // list of current enemys of this character

    // Use this for initialization
    private void Start( ) {
        Awake( );
        material.color = BaseColor;
        Nav            = gameObject.GetComponent < CompanionNav >( );
        StaticManager.RealTime.Companions.Add(this);
    }

    public override void Damage()
    {
        if (stats.Health > 0){

            stats.Health -= damage;
        }

        if ( stats.Health <= 0 ){
            
            Destroy(gameObject);
        }
    }
    //runs when enemys's attack animation is half way over
    public void Attack( ) {
        //plays damage text
        enemies[0].AnimationClass.Play(AnimationClass.states.AttackText);

        //gets the damage value
        float l_damage = StaticManager.DamageCalc.CalcAttack( enemies[0].stats, stats);

        //sets the text value to the damage done
        enemies[0].damageText.text = ((int)l_damage).ToString( );

        //adds the value to the total damage
        enemies[0].damage += ( int )l_damage;
    }
    
    public void Remove( BaseCharacter _chara ) { }

}