using System.Collections;
using System.Collections.Generic;
using Kristal;
using UnityEngine;

public class FireProjectile : Projectile
{
    public void Start()
    {
        DOT_interval = 1; //Time between DOT damage.

    }
    protected override void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Enemy" || other.tag == "Companion" || other.tag == "Player")
        {
            base.OnTriggerEnter(other); //Calls Projectile's OnTriggerEnter
        }
    }
}
