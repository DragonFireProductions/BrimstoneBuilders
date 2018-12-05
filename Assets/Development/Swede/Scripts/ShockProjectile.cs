using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShockProjectile : Projectile
{
    public float ChainRadius;
    public void Start()
    {
        DOT_interval = 1; //Time between DOT damage.
    }
    protected override void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy" || other.tag == "Companion" || other.tag == "Player")
        {
            base.OnTriggerEnter(other);//Calls Projectile's OnTriggerEnter

            Collider[] hitColliders = Physics.OverlapSphere(other.transform.position, ChainRadius);

            foreach (var Collider in hitColliders)
            {
                if (Collider.tag == "Enemy")
                {
                    base.OnTriggerEnter(Collider);
                }
            }
        }
    }

}
