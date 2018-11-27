using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShockProjectile : Projectile {

    public int initialDamage; //initial damage done to enemy on hit.
    private Vector3 aboveHead;
    private int animationTime = 1;
    public void Start()
    {
        DOT_interval = 1; //Time between DOT damage.

    }
    protected override void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy" || other.tag == "Companion" || other.tag == "Player")
        {
            base.OnTriggerEnter(other);
            //play particle.
            aboveHead = other.transform.position;
            aboveHead.y += 10;
            StartCoroutine(StaticManager.particleManager.Play(ParticleManager.states.Shock, aboveHead, animationTime));
        }


        //Play particle system for effects.
        //Set the position to the passed in 'other'.
    }
}
