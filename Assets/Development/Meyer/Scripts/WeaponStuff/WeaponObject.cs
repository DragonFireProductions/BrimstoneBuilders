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

    public GameObject mesh;

    public GameObject label;

    public AudioSource audio;

    public AudioClip[] clips;
    protected override void Start() {
        base.Start();
        AnimationClass = gameObject.GetComponent < AnimationClass >( );
        item = this.gameObject;
        audio = gameObject.GetComponent<AudioSource>();
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
    public virtual void Deactivate()
    {

    }
    public override void Attach( ) {
        if ( AttachedCharacter.attachedWeapon ){

            AttachedCharacter.attachedWeapon.leftHand.SetActive( false );
            AttachedCharacter.attachedWeapon.rightHand.SetActive( false );
        }
        leftHand.SetActive(true);
        rightHand.SetActive(true);
        StaticManager.inventories.inventory.character.inventoryUI.UpdateWeapon(StaticManager.uiManager.WeaponInventoryStats.GetComponent<UIItemsWithLabels>(), this);
        var c = AttachedCharacter as Companion;

        StaticManager.UiInventory.RemoveMainInventory(this as WeaponObject, c.inventory);

        c.inventoryUI.EnableContainer(AttachedCharacter.attachedWeapon);
        c.inventoryUI.RemoveObject(this);

        gameObject.SetActive(true);

        var prev_attached = AttachedCharacter.attachedWeapon;

        AttachedCharacter.attachedWeapon = this as WeaponObject;

        AttachedCharacter.attachedWeapon.gameObject.layer = AttachedCharacter.gameObject.layer;

        AttachedCharacter.attachedWeapon.tag = "Weapon";

        AttachedCharacter.AnimationClass.SwitchWeapon(this);

        mesh.SetActive(false);

        if ( AttachedCharacter is Companion ){
            var a = AttachedCharacter as Companion;
            a.inventory.WeaponInventory.Attach(this);

            if ( prev_attached ){
                a.inventory.WeaponInventory.labels.Add(prev_attached.label);
                a.inventory.WeaponInventory.UpdateGrid();
            }
           
        }

    }
    public void Attach(Enemy enemy)
    {
        if (AttachedCharacter.attachedWeapon)
        {

            AttachedCharacter.attachedWeapon.leftHand.SetActive(false);
            AttachedCharacter.attachedWeapon.rightHand.SetActive(false);
        }
        leftHand.SetActive(true);
        rightHand.SetActive(true);

        gameObject.SetActive(true);

        AttachedCharacter.attachedWeapon = this as WeaponObject;

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
        //StaticManager.audioManager.PlaySound(stats.clip.name.ToString());
    }

    public void PickUp(BaseCharacter character ) {
        if (tag == "PickUp")
        {
            StaticManager.uiManager.ShowNotification("Picked up weapon", 2);
            if ( mesh ){
                 mesh.SetActive(false);
            }

            var a = character as Companion;
            a.inventory.WeaponInventory.PickUp(this);
            this.GetComponent<BoxCollider>().enabled = false;
            AttachedCharacter = a;
            leftHand = Instantiate( leftHand );
            rightHand = Instantiate( rightHand );

            leftHand.transform.position = AttachedCharacter.leftHand.transform.position;
            rightHand.transform.position = AttachedCharacter.rightHand.transform.position;

            leftHand.transform.localScale = AttachedCharacter.transform.localScale;
            rightHand.transform.localScale = AttachedCharacter.transform.localScale;

            leftHand.transform.rotation = AttachedCharacter.leftHand.transform.rotation;
            rightHand.transform.rotation = AttachedCharacter.rightHand.transform.rotation;

            leftHand.transform.SetParent(AttachedCharacter.leftHand.transform, true);
            rightHand.transform.SetParent(AttachedCharacter.rightHand.transform, true);

            if ( leftHand.GetComponent<WeaponCollision>() ){
                leftHand.GetComponent < WeaponCollision >( ).obj = this;
            }

            if ( rightHand.GetComponent<WeaponCollision>() ){
                rightHand.GetComponent < WeaponCollision >( ).obj = this;
            }

            leftHand.SetActive(false);
            rightHand.SetActive(false);

            leftHand.layer = character.gameObject.layer;
            rightHand.layer = character.gameObject.layer;

            gameObject.layer = character.gameObject.layer;

            if ( type == SubClasses.Types.MELEE ){
                Damage = a.mele.CurrentLevel;
            }

            else if ( type == SubClasses.Types.RANGE ){
                Damage = a.range.CurrentLevel;
            }

           else if ( type == SubClasses.Types.MAGIC ){
                Damage = a.range.CurrentLevel;
            }


        }
    }
    public void PickUp(Enemy character)
    {
        if (tag == "PickUp")
        {
            if (mesh)
            {
                mesh.SetActive(false);
            }
            
            this.GetComponent<BoxCollider>().enabled = false;
            AttachedCharacter = character;
            leftHand = Instantiate(leftHand);
            rightHand = Instantiate(rightHand);

            leftHand.transform.position = AttachedCharacter.leftHand.transform.position;
            rightHand.transform.position = AttachedCharacter.rightHand.transform.position;

            leftHand.transform.localScale = AttachedCharacter.transform.localScale;
            rightHand.transform.localScale = AttachedCharacter.transform.localScale;

            leftHand.transform.rotation = AttachedCharacter.leftHand.transform.rotation;
            rightHand.transform.rotation = AttachedCharacter.rightHand.transform.rotation;

            leftHand.transform.SetParent(AttachedCharacter.leftHand.transform, true);
            rightHand.transform.SetParent(AttachedCharacter.rightHand.transform, true);

            if (leftHand.GetComponent<WeaponCollision>())
            {
                leftHand.GetComponent<WeaponCollision>().obj = this;
            }

            if (rightHand.GetComponent<WeaponCollision>())
            {
                rightHand.GetComponent<WeaponCollision>().obj = this;
            }

            leftHand.SetActive(false);
            rightHand.SetActive(false);

            leftHand.layer = character.gameObject.layer;
            rightHand.layer = character.gameObject.layer;

            gameObject.layer = character.gameObject.layer;

        }
    }
    public void DamageCollider(Collider collider)
    {
        if ((collider.tag == "Enemy" || collider.tag == "Companion" || collider.tag == "Player") && tag == "Weapon")
        {

            if (AttachedCharacter.tag == "Companion" && collider.tag == "Player")
            {
                return;
            }
            collider.gameObject.GetComponent<BaseCharacter>().Damage((int)Damage, this);
        }
    }
    protected virtual void OnTriggerEnter(Collider collider)
    {
        if ( collider.tag == "Player" ){
            WeaponObject weaponObject = this;
            if (!StaticManager.inventories.audio.isPlaying)
            {
                if (weaponObject is GunType)
                {
                    StaticManager.inventories.audio.PlayOneShot(StaticManager.inventories.clips[2], 0.25f);
                }
                else if (weaponObject is SwordType)
                {
                    StaticManager.inventories.audio.PlayOneShot(StaticManager.inventories.clips[1], 0.25f);
                }
            }
            PickUp(collider.GetComponent<BaseCharacter>());
        }
       
    }

}
