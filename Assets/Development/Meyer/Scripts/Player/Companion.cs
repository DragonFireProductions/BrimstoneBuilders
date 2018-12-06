using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;

using Kristal;

using TMPro;

using UnityEngine;
using UnityEngine.UI;

using Random = UnityEngine.Random;

public class Companion : BaseCharacter {
    // Use this for initialization

    public PlayerInventory inventory;

    public CharacterInventoryUI inventoryUI;

    public int cost;

    public Magic magic = new Magic();

    public Range range = new Range();

    public Mele mele = new Mele();

    public SubClasses CurrentSubClass;

    public override object this[string propertyName]
    {
        get
        {
            if (this.GetType().GetField(propertyName) != null)
            {
                return this.GetType().GetField(propertyName).GetValue(this);
            }
            else if (base[propertyName] != null)
            {
                return base[propertyName];
            }

            return null;
        }
        set { this.GetType().GetField(propertyName).SetValue(this, value); }
    }

    protected void Awake( ) {
        base.Awake();



    }

    protected virtual void Start( ) {
        
        magic.character = this;
        range.character = this;
        mele.character = this;

        inventoryUI = GetComponent<CharacterInventoryUI>();
        inventoryUI.Init(this);
        inventoryUI.companion = this;
        //material.color = BaseColor;
        Nav            = gameObject.GetComponent < CompanionNav >( );
        StaticManager.RealTime.Companions.Add(this);
        inventory = GetComponent < PlayerInventory >( );
        attachedWeapon = leftHand.GetComponent < BaseItems >( ) as WeaponObject;
        //attachedWeapon.AttachedCharacter = this;
        //inventoryUI.AddWeapon(attachedWeapon);
        //attachedWeapon.tag = "Weapon";
        //attachedWeapon.AssignDamage();
        if (startWeapon && tag != "Player")
        {
            startWeapon = Instantiate(startWeapon);
            startWeapon.GetComponent<WeaponObject>().PickUp(this);
            startWeapon.GetComponent<WeaponObject>().Attach();
        }
    }

    public void OnTriggerEnter(Collider collider ) {

    }
    public override void Damage(int _damage, BaseItems item)
    {

        Vector3 scale = new Vector3(1, 1, 1);

        var total = item.AttachedCharacter.stats.luck + stats.luck;

        var rand = Random.Range(1, total);

        if (rand > total - (item.AttachedCharacter.stats.luck * 0.3))
        {
            _damage += 5;
            scale = new Vector3(2, 2, 2);
        }


        if (stats.Health > 0){
            InstatiateFloatingText.InstantiateFloatingText(_damage.ToString(), this, Color.blue, scale);
            stats.Health -= _damage;


        }

        if ( stats.Health <= 0 ){
            if ( this == StaticManager.Character ){
               StaticManager.UiInventory.ItemsInstance.GameOverUI.SetActive(true);
                Time.timeScale = 0;
            }

            Destroy(GetComponent<CompanionNav>().behaviors.gameObject);
            StaticManager.RealTime.Companions.Remove( this );
            StaticManager.inventories.Destroy(inventory);
            Destroy(gameObject);
        }
    }

    public void Remove( BaseCharacter _chara ) { }

}