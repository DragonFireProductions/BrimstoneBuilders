using System;
using System.Collections;
using System.Collections.Generic;

using TMPro;

using UnityEngine;

namespace Kristal {

    public class Enemy : BaseCharacter {

        [ SerializeField ] private GameObject attachedWeapon;

        public List <Companion> enemies { get; set; }
        // Use this for initialization
        protected void Awake( ) {
            base.Awake( );
        }
        

        protected void Start( ) {
            material.color = BaseColor;
            Nav            = gameObject.GetComponent < EnemyNav >( );
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

    }

}