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

    public override void Damage(int _damage)
    {
        if (stats.Health > 0){

            stats.Health -= _damage;
        }
        
        if ( stats.Health <= 0 ){
            if ( this == StaticManager.Character ){
               StaticManager.UiInventory.ItemsInstance.GameOverUI.SetActive(true);
                Time.timeScale = 0;
            }
            //enemies[0].damage -= (int)StaticManager.DamageCalc.CalcAttack(enemies[0].stats, stats);

            AnimationClass.Stop(AnimationClass.states.DamageText);
            AnimationClass.Stop(AnimationClass.states.Attacking);
            Destroy(gameObject);
        }
        damage -= _damage;
    }
    //runs when enemys's attack animation is half way over
    public void Attack( ) {
        //plays character damage animation
        StartCoroutine(StaticManager.particleManager.Play(ParticleManager.states.Damage, enemies[0].transform.position, 1));
        //gets the damage value
        float l_damage = StaticManager.DamageCalc.CalcAttack(enemies[0].stats, stats);
        //adds the value to the total damage
        enemies[0].damage += (int)l_damage;
        //sets the text value to the damage done
        enemies[0].damageText.text = enemies[0].damage.ToString();

        enemies[0].AnimationClass.Play(AnimationClass.states.DamageText);

        enemies[0].Damage((int)l_damage);

    }
    
    public void Remove( BaseCharacter _chara ) { }

}