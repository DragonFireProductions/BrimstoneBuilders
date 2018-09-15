using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunType : WeaponObject
{

    // Use this for initialization

    // TODO:1 Finish script
    //------------------------
    // Script references base class of Weapon Object
    // Add variables as needed to either script
    // Add functions
    // All variables from previous script can be referenced
    // Script should allow for shooting. Is attached to weapon object in scene
    [SerializeField] GameObject Projectile;
    [SerializeField] protected float Range;
    [SerializeField] protected float FireRate;
    [SerializeField] protected float ReloadTime;
    [SerializeField] protected int Capacity;
    [SerializeField] protected int Ammo;

    bool CanFire = true;
    bool Reloading = false;

    //TODO:2 See WeaponObject.cs
    public override void Select()
    {

    }

    //shoots gun
    public override void Attack()
    {
        Debug.Log("Attacking");
        if (CanFire && Ammo > 0)
            StartCoroutine(Fire());
        else if (!Reloading && Ammo == 0)
            StartCoroutine(Reload());

    }

    IEnumerator Fire()
    {
        CanFire = false;

        Projectile projectile = Instantiate(Projectile, transform.position + transform.forward, transform.rotation).GetComponent<Projectile>();
        Destroy(projectile.gameObject, Range / projectile.GetSpeed());

        Ammo -= 1;

        yield return new WaitForSeconds(1 / FireRate);
        CanFire = true;
    }

    IEnumerator Reload()
    {
        Reloading = true;
        yield return new WaitForSeconds(ReloadTime);
        Ammo = Capacity;
        Reloading = false;
    }

    ///Recommended functions
    // - Damage
    // - Increase / decrease strength
    // - Set transform (Move gameobject to player's hand)
}