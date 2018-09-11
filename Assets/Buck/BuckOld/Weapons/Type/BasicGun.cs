using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BasicGun : BaseWeapon
{

    void Awake()
    {
        shootableMask = LayerMask.GetMask("Shootable");
        gunLR = GetComponentInChildren<LineRenderer>();
        gunLight = GetComponentInChildren<Light>();
    }

    void Start()
    {
        noAmmo = false;

        curClipSize = maxClipSize;
    }

    void Update()
    {
        fireTimer += Time.deltaTime;

        if (Input.GetButton("Fire1") && fireTimer >= fireRate && noAmmo == false)
        {
            //if (EventSystem.current.IsPointingOverGameObject())
            //{

            //}
            Fire();
        }

        if (fireTimer >= fireRate * effectDisplayTime)
        {
            DisableEffects();
        }

        Reload();
    }


}
