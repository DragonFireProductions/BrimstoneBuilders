using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunType : WeaponObject {

	// Use this for initialization

    // TODO:1 Finish script
    //------------------------
    // Script references base class of Weapon Object
    // Add variables as needed to either script
    // Add functions
    // All variables from previous script can be referenced
    // Script should allow for shooting. Is attached to weapon object in scene
    
    //TODO:2 See WeaponObject.cs
    public override void Select()
    {
        
    }

    //shoots gun
    public virtual void Shoot()
    {

    }

    ///Recommended functions
    // - Damage
    // - Increase / decrease strength
    // - Set transform (Move gameobject to player's hand)
}
