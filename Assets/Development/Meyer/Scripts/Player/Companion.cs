﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;

using Kristal;

using TMPro;

using UnityEngine;
using UnityEngine.UI;

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
        material.color = BaseColor;
        Nav            = gameObject.GetComponent < CompanionNav >( );
        StaticManager.RealTime.Companions.Add(this);
        inventory = GetComponent < PlayerInventory >( );
        cube = transform.Find("Cube").gameObject;
        attachedWeapon = cube.GetComponentInChildren < BaseItems >( ) as WeaponObject;
        attachedWeapon.AttachedCharacter = this;
        inventoryUI.AddWeapon(attachedWeapon);
        attachedWeapon.tag = "Weapon";
        attachedWeapon.AssignDamage();

    }

    public void OnTriggerEnter(Collider collider ) {

    }
    public override void Damage(int _damage, BaseItems item)
    {
        if (stats.Health > 0){
            InstatiateFloatingText.InstantiateFloatingText(_damage.ToString(), this, Color.blue);
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