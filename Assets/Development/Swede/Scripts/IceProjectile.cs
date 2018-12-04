﻿using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;

using UnityEngine;

public class IceProjectile : Projectile {
    
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
            base.OnTriggerEnter(other); //Calls Projectile's OnTriggerEnter
            other.GetComponent<BaseCharacter>().Freeze(6); //Freezes whoever makes contact with the projectile.
        }
    }

    public IEnumerator Freeze(Collider other, float seconds)
    {
        other.GetComponent<BaseCharacter>().Nav.SetState = BaseNav.state.FREEZE;
        yield return new WaitForSeconds(seconds);
        other.GetComponent<BaseCharacter>().Nav.SetState = BaseNav.state.ATTACKING;
    }
}