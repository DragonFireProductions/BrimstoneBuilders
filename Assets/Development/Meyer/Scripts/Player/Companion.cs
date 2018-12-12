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

    public Armor armor;



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


        if ( armor.currentShoulder ){
            armor.currentShoulder = Instantiate( armor.currentShoulder );
            armor.currentShoulder.OnTriggerEnter(GetComponent<Collider>());
            armor.currentShoulder.Attach();
        }

        if ( armor.currentBody ){
            armor.currentBody = Instantiate( armor.currentBody );
            armor.currentBody.OnTriggerEnter(GetComponent<Collider>());
            armor.currentBody.Attach();
        }

        if ( armor.currentGlove ){
            armor.currentGlove = Instantiate( armor.currentGlove );
            armor.currentGlove.OnTriggerEnter(GetComponent<Collider>());
            armor.currentGlove.Attach();
        }

        if ( armor.currentHead ){
            armor.currentHead = Instantiate( armor.currentHead );
            armor.currentHead.OnTriggerEnter(GetComponent<Collider>());
            armor.currentHead.Attach();
        }

        if ( armor.currentShoe ){
            armor.currentShoe = Instantiate( armor.currentShoe );
            armor.currentShoe.OnTriggerEnter(GetComponent<Collider>());
            armor.currentShoe.Attach();
        }

        if ( armor.currentBelt ){
            armor.currentBelt = Instantiate( armor.currentBelt );
            armor.currentBelt.OnTriggerEnter(GetComponent<Collider>());
            armor.currentBelt.Attach();
        }
       
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