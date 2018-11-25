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

    public virtual void Attack(BaseCharacter enemy = null ) {
       
        AttachedCharacter.AnimationClass.Play(AnimationClass.states.AttackTrigger);
        AttachedCharacter.attachedWeapon.AnimationClass.Play(AnimationClass.weaponstates.EnabledTrigger);
    }
    protected override void Start() {
        base.Start();
        AnimationClass = gameObject.GetComponent < AnimationClass >( );
        item = this.gameObject;
    }

    public virtual object this[ string propertyName ] {
        get { return this.GetType().GetField(propertyName).GetValue(this); }
        set { this.GetType().GetField(propertyName).SetValue(this, value); }
    }

    public virtual void PickUp( ) {
        if ( AttachedCharacter == null ){

        }
        
    }

    public void MoveToBackPack( ) {
        StaticManager.UiInventory.ItemsInstance.BackPackUI.GetComponentInChildren<RawImage>().texture = stats.icon;
        StaticManager.UiInventory.ItemsInstance.BackPackUI.GetComponentInChildren<TextMeshProUGUI>().text = stats.objectName;

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
            if ( collider.tag != AttachedCharacter.tag){
                if ( AttachedCharacter.tag == "Companion" && collider.tag == "Player" ){
                    return;
                }
                collider.gameObject.GetComponent<BaseCharacter>().Attack(AttachedCharacter);
            }
        }
    }
   
}
