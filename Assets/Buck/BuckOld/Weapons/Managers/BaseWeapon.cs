using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseWeapon : MonoBehaviour
{
    [Header("WeaponStats")]

    public int maxClipSize;

    [HideInInspector]
    public int curClipSize;

    public float fireRate;

    [HideInInspector]
    public float fireTimer;

    public float range;

    public int damagePerShot;

    public float effectDisplayTime;

    [Header("UnitySettings")]

    public Transform firePoint;

    public LineRenderer gunLR;

    public Light gunLight;

    [HideInInspector]
    public Ray shootRay;

    [HideInInspector]
    public RaycastHit shootHit;

    [HideInInspector]
    public int shootableMask;

    //ParticleSystem

    //Light

    //AudioSource

    [HideInInspector]
    public bool noAmmo;


    public void Fire()
    {
        fireTimer = 0;

        curClipSize--;

        gunLR.enabled = true;
        gunLight.enabled = true;

        gunLR.SetPosition(0, firePoint.position);

        shootRay.origin = firePoint.position;
        shootRay.direction = firePoint.forward;

        if (Physics.Raycast(shootRay, out shootHit, range, shootableMask))
        {
            BaseEnemy enemyHealth = shootHit.collider.GetComponent<BaseEnemy>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(damagePerShot, shootHit.point);
            }
            gunLR.SetPosition(1, shootHit.point);
        }
        else
        {
            gunLR.SetPosition(1, shootRay.origin + shootRay.direction * range);
        }

        if (curClipSize == 0)
        {
            noAmmo = true;
            return;
        }
    }

    public void Reload()
    {
        if (curClipSize <= maxClipSize)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                curClipSize = maxClipSize;
                noAmmo = false;
                return;
            }

            if (curClipSize <= 0)
            {
                curClipSize = 0;
                return;
            }
        }
    }

    public void DisableEffects()
    {
        gunLR.enabled = false;
        //Particles
        //Audio
        gunLight.enabled = false;
    }
}
