﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonProjectile : Projectile {

    public int initialDamage; //initial damage done to enemy on hit.
    public int hits; //how many ticks of dot damage.
    public float interval; //amount of time between dot hits.
    public void Start()
    {
        DOT_interval = 1; //Time between DOT damage.

    }
    protected override void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy" || other.tag == "Companion" || other.tag == "Player")
        {
            other.GetComponent<BaseCharacter>().Damage(initialDamage); //Deal the initial damage.
            other.GetComponent<BaseCharacter>().DOT(Damage, interval, hits); //Apply the dot damage.
            //play particle.
        }


        //Play particle system for effects.
        //Set the position to passed in 'other'.
    }
}
