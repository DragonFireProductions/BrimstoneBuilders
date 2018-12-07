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
  
    [HideInInspector]public int cost;
  
    [HideInInspector]public Magic magic = new Magic();
    
    [HideInInspector]public Range range = new Range();
   
    [HideInInspector]public Mele mele = new Mele();
   
    [HideInInspector]public SubClasses CurrentSubClass;

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

        if ( tag == "Player" ){
             inventoryUI.Init(this);
        }
        else{
            inventoryUI.CharacterInventory.SetActive(false);
        }

        StaticManager.RealTime.Companions.Add(this);
        
       
    }

    public void OnTriggerEnter(Collider collider ) {

    }
    public override void Damage(int _damage, BaseItems item)
    {
        
        


        if (stats.Health > 0){
            base.Damage(_damage, item);
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