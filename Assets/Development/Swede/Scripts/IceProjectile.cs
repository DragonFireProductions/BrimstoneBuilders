using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;

using UnityEngine;

public class IceProjectile : Projectile {

    public int initialDamage; //initial damage done to enemy on hit.

    public float SecondsFrozen; //Amount of time spent frozen.
    // Use this for initialization
    public void Start()
    {
        DOT_interval = 1; //Time between DOT damage.
    }
    protected override void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy" || other.tag == "Companion" || other.tag == "Player")
        {
            other.GetComponent<BaseCharacter>().Damage(initialDamage); //Deal the initial damage.
            //other.GetComponent<BaseCharacter>().DOT(Damage, interval, hits); //Apply the dot damage.

            StartCoroutine(Freeze(other, SecondsFrozen)); //Freezes whoever makes contact with the projectile.
            
            //play particle.
        }
        //Play particle system for effects.
        //Set the position to passed in 'other'.
    }

    public IEnumerator Freeze(Collider other, float seconds)
    {
        other.GetComponent<BaseCharacter>().Nav.SetState = BaseNav.state.FREEZE;
        yield return new WaitForSeconds(seconds);
        other.GetComponent<BaseCharacter>().Nav.SetState = BaseNav.state.ATTACKING;
    }
}
