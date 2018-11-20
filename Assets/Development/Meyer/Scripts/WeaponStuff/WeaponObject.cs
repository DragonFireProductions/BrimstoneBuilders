using System.Collections;
using System.Collections.Generic;
using Assets.Meyer.TestScripts.Player;

using Kristal;

using TMPro;

using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

public class WeaponObject : MonoBehaviour
{
    /// Variables should be protected NOT public or private
    
    protected GameObject weapon; // gameObject this is attached too
    [SerializeField] protected WeaponItem weaponStats; // contains inventory information

    [SerializeField] protected string weaponName; // references InventoryManager items

    public bool isMainInventory = true;

    public bool attached = false;
   
    public AnimationClass AnimationClass;

    public BaseCharacter AttacheBaseCharacter;

    public virtual void Attack(BaseCharacter enemy = null ) {
       
        AttacheBaseCharacter.AnimationClass.Play(AnimationClass.states.AttackTrigger);
        AttacheBaseCharacter.attachedWeapon.AnimationClass.Play(AnimationClass.weaponstates.EnabledTrigger);
    }
    protected virtual void Start() {
        AnimationClass = gameObject.GetComponent < AnimationClass >( );
        weaponStats = StaticManager.inventories.GetItemFromAssetList( weaponName );
        Assert.IsNotNull(weaponStats, "WeaponItem name not added in inspector " + gameObject.name);
        weapon = this.gameObject;
    }

    public virtual object this[ string propertyName ] {
        get { return this.GetType().GetField(propertyName).GetValue(this); }
        set { this.GetType().GetField(propertyName).SetValue(this, value); }
    }

    public virtual void PickUp( ) {
        if ( AttacheBaseCharacter == null ){
        StaticManager.UiInventory.AddSlot(this, StaticManager.Character.inventory);
        gameObject.SetActive(false);
        }
        
    }

    public void MoveToBackPack( ) {
        StaticManager.UiInventory.ItemsInstance.BackPackUI.GetComponentInChildren<RawImage>().texture = WeaponStats.icon;
        StaticManager.UiInventory.ItemsInstance.BackPackUI.GetComponentInChildren<TextMeshProUGUI>().text = WeaponStats.objectName;

    }
    
    protected virtual void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "Player" && !StaticManager.UiInventory.Dragging && !attached && tag == "PickUp")
        {
            StaticManager.Character.inventory.PickUp(this);
            this.GetComponent<BoxCollider>().enabled = false;
            AttacheBaseCharacter = StaticManager.Character;
        }

        if ((collider.tag == "Enemy"  || collider.tag == "Companion" || collider.tag == "Player") && tag == "Weapon"){
            if ( collider.tag != AttacheBaseCharacter.tag){
                if ( AttacheBaseCharacter.tag == "Companion" && collider.tag == "Player" ){
                    return;
                }
                collider.gameObject.GetComponent<BaseCharacter>().Attack(AttacheBaseCharacter);
            }
        }
    }
    
    public WeaponItem WeaponStats
    {
        get { return weaponStats; }
    }
}
