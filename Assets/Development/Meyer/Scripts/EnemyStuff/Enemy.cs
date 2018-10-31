using System;
using System.Collections;
using System.Collections.Generic;

using TMPro;

using UnityEngine;

namespace Kristal {

    public class Enemy : BaseCharacter {

        [ SerializeField ] private GameObject attachedWeapon;

        public List < Companion > enemies; //list of current enemies of this character
        // Use this for initialization
        protected void Awake( ) {
            base.Awake( );
        }
        

        protected void Start( ) {
            material.color = BaseColor;
            Nav            = gameObject.GetComponent < EnemyNav >( );
            threat_signal = gameObject.transform.Find("ThreatSignal").GetComponent<SpriteRenderer>();

            threat_signal.enabled = stats.Strength > 10;


        }
        

        public void Remove( BaseCharacter chara ) { }

        public override void Damage( ) {
            
            if ( stats.Health > 0){

                stats.Health -= damage;
            }
            else{
                Destroy(gameObject);
            }
        }
        public void ChooseEnemy()
        {
            //picks a random index for companions
            int range = UnityEngine.Random.Range(0, StaticManager.RealTime.Companions.Count);
            //if the main player has more than one enemy currently attacking
            //and the random index is the main player
            ///makes it so the main player can only have one enemy attacking
            while ((StaticManager.Character.enemies.Count > 1 && StaticManager.RealTime.Companions[range] == StaticManager.Character))
            {
                //then the enemy goes back to idle if there are companions left
                if ( StaticManager.RealTime.Companions.Count <= 1 ){
                    Nav.SetState = BaseNav.state.IDLE;

                    return;
                }
                //else it selects a new range
                range = UnityEngine.Random.Range(0, StaticManager.RealTime.Companions.Count);
            }
            //adds the random chosen companion to the companions enemy list
            Companion chosenCompanion = StaticManager.RealTime.Companions[range];

            chosenCompanion.enemies.Add(this);

            //adds the random chosen companion to current enemy list
            enemies.Add(chosenCompanion);
            //sets the state to attacking
            Nav.SetState = BaseNav.state.ATTACKING;
        }
        //runs when enemy's animation is half way through
        public void Attack(Companion enemy ) {
            //plays character damage animation
            enemies[0].AnimationClass.Play(AnimationClass.states.AttackText);
            //gets the damage value
            float l_damage = StaticManager.DamageCalc.CalcAttack(enemies[0].stats, stats);
            //sets the text value to the damage done
            enemies[0].damageText.text = ((int)l_damage).ToString();
            //adds the value to the total damage
            enemies[0].damage += (int)l_damage;

            
        }
    }

}