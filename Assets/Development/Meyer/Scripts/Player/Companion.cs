using System;
using System.Collections;
using System.Collections.Generic;

using Kristal;

using TMPro;

using UnityEngine;

public class Companion : BaseCharacter {
    public List<Enemy> enemies { get; set; }

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
            StaticManager.RealTime.Companions.Remove( this );
            Destroy(gameObject);
        }
    }

    public void Remove( BaseCharacter _chara ) { }

}