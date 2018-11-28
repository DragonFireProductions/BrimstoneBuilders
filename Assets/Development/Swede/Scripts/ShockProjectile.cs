﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShockProjectile : Projectile
{
    public void Start()
    {
        DOT_interval = 1; //Time between DOT damage.
    }
    protected override void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy" || other.tag == "Companion" || other.tag == "Player")
        {
            base.OnTriggerEnter(other);//Calls Projectile's OnTriggerEnter
            
            //Particle effect gets played in the CustomInspector.cs 
        }
    }
    
}
