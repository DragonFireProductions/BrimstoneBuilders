using System.Collections;
using System.Collections.Generic;
using Assets.Meyer.TestScripts.Player;

using Kristal;

using TMPro;

using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

public class WeaponObject : BaseItems
{
    /// Variables should be protected NOT public or private
    public AnimationClass AnimationClass;

    protected override void Start() {
        base.Start();
        AnimationClass = gameObject.GetComponent < AnimationClass >( );
        item = this.gameObject;
    }

    public virtual object this[ string propertyName ] {
        get { return this.GetType().GetField(propertyName).GetValue(this); }
        set { this.GetType().GetField(propertyName).SetValue(this, value); }
    }

    public override void Attach( ) {
        item.SetActive(true);

        item.transform.position = AttachedCharacter.cube.transform.position;

        item.transform.localScale = new Vector3(1, 1, 1);

        var c = AttachedCharacter as Companion;
        

        StaticManager.UiInventory.RemoveMainInventory(this as WeaponObject, c.inventory);

        c.inventoryUI.EnableContainer(AttachedCharacter.attachedWeapon);
        c.inventoryUI.RemoveObject(this);

        gameObject.SetActive(true);
        
        AttachedCharacter.attachedWeapon = this as WeaponObject;

        AttachedCharacter.attachedWeapon.transform.rotation = AttachedCharacter.cube.transform.rotation;

        AttachedCharacter.attachedWeapon.transform.SetParent(AttachedCharacter.cube.transform, true);

    }

    
    protected virtual void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "Player" && !StaticManager.UiInventory.Dragging && tag == "PickUp")
        {
            StaticManager.Character.inventory.PickUp(this);
            this.GetComponent<BoxCollider>().enabled = false;
            AttachedCharacter = StaticManager.Character;
        }

        if ((collider.tag == "Enemy"  || collider.tag == "Companion" || collider.tag == "Player") && tag == "Weapon"){

                if ( AttachedCharacter.tag == "Companion" && collider.tag == "Player" ){
                    return;
                }
                collider.gameObject.GetComponent<BaseCharacter>().Attack(AttachedCharacter);
        }
    }
   
}
