using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Meyer.TestScripts.Player;

using Kristal;

using TMPro;

using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

public abstract class WeaponObject : BaseItems
{
    /// Variables should be protected NOT public or private
    public AnimationClass AnimationClass;

    public float Damage;

    public bool RunAwayOnUse = false;

    public bool KnockBack = false;

    public SubClasses.Types type;

    public float KnockBackAmount;

    public GameObject leftHand;

    public GameObject rightHand;

    protected override void Start() {
        base.Start();
        AnimationClass = gameObject.GetComponent < AnimationClass >( );
        item = this.gameObject;
        
    }
    public override object this[string propertyName]
    {
        get
        {
            if (this.GetType().GetField(propertyName) != null)
            {
                return this.GetType().GetField(propertyName).GetValue(this);
            }
            else if (base[propertyName] != null){
                return base[ propertyName ];
            }
            else{
                return null;
            }
        }
        set { this.GetType().GetField(propertyName).SetValue(this, value); }
    }

    public virtual void Activate( ) {

    }

    public override void Attach( ) {
        if ( AttachedCharacter.attachedWeapon ){


            AttachedCharacter.attachedWeapon.leftHand.SetActive( false );
            AttachedCharacter.attachedWeapon.rightHand.SetActive( false );
        }

        StaticManager.inventories.inventory.character.inventoryUI.UpdateItem(StaticManager.uiManager.WeaponInventoryStats.GetComponent<UIItemsWithLabels>(), this);
        var hand = leftHand;
        var rhand = rightHand;
        rightHand = rhand;
        rightHand.SetActive(true);
        item.SetActive(true);

        hand.transform.position = AttachedCharacter.leftHand.transform.position;
        rhand.transform.position = AttachedCharacter.rightHand.transform.position;

        hand.transform.localScale = new Vector3(1, 1, 1);
        rhand.transform.localScale = new Vector3(1, 1, 1);

        var c = AttachedCharacter as Companion;


        StaticManager.UiInventory.RemoveMainInventory(this as WeaponObject, c.inventory);

        c.inventoryUI.EnableContainer(AttachedCharacter.attachedWeapon);
        c.inventoryUI.RemoveObject(this);

        gameObject.SetActive(true);

        AttachedCharacter.attachedWeapon = this as WeaponObject;

        hand.transform.rotation = AttachedCharacter.leftHand.transform.rotation;
        rhand.transform.rotation = AttachedCharacter.rightHand.transform.rotation;

        this.transform.SetParent(AttachedCharacter.leftHand.transform, true);
        rhand.transform.SetParent(AttachedCharacter.rightHand.transform, true);

        AttachedCharacter.attachedWeapon.gameObject.layer = AttachedCharacter.gameObject.layer;

        AttachedCharacter.attachedWeapon.tag = "Weapon";

        AttachedCharacter.AnimationClass.SwitchWeapon(this);


    }

    public override void IncreaseSubClass(float amount ) {
        if ( Damage <= 10 ){
        Damage += amount;
        }
    }

    public override void AssignDamage( ) {
        
    }

    public override void Use( ) {
        
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
                collider.gameObject.GetComponent<BaseCharacter>().Damage((int)Damage, this);
        }
    }

}
