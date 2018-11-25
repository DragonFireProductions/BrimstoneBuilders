using System.Collections;
using System.Collections.Generic;
using Kristal;
using UnityEngine;

public class FireProjectile : Projectile {
    
    public void Start()
    {
        DOT_interval = 1; //Time between DOT damage.

    }
    protected override void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Enemy" || other.tag == "Companion" || other.tag == "Player")
        {
            other.GetComponent<BaseCharacter>().DOT(Damage, interval, hits);
            //play particle.
        }


        //Play particle system for effects.
        //Set the position to passed in 'other'.
    }
}
