using System.Collections;
using System.Collections.Generic;
using Assets.Meyer.TestScripts.Player;

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
    protected Animation animator;

    public bool isMainInventory = true;
    protected virtual void Start()
    {
        animator = gameObject.GetComponent<Animation>();
        weaponStats = StaticManager.Inventory.GetItemFromAssetList( weaponName );
        Assert.IsNotNull(weaponStats, "WeaponItem name not added in inspector " + gameObject.name);
        weapon = this.gameObject;
    }

    public virtual object this[ string propertyName ] {
        get { return this.GetType().GetField(propertyName).GetValue(this); }
        set { this.GetType().GetField(propertyName).SetValue(this, value); }
    }

    public void PickUp( ) {
        StaticManager.UiInventory.AddSlot(this);
        StaticManager.Inventory.MainInventoryList.Add(this);
    }

    public void MoveToBackPack( ) {
        StaticManager.UiInventory.ItemsInstance.BackPackUI.GetComponentInChildren<RawImage>().texture = WeaponStats.icon;
        StaticManager.UiInventory.ItemsInstance.BackPackUI.GetComponentInChildren<TextMeshProUGUI>().text = WeaponStats.objectName;

    }
    public virtual void Attack()
    {
          Debug.Log("Object has attacked!");
    }
    
    protected void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "Player" && !StaticManager.UiInventory.Dragging)
        {
            //StaticManager.Inventory.Add(this);
            this.GetComponent<BoxCollider>().enabled = false;
   
        }

        else if ( collider.tag == "Player" && StaticManager.UiInventory.Dragging && StaticManager.UiInventory.IsMainInventory){
            StaticManager.UiInventory.Dragging = false;
            gameObject.SetActive(true);

            if ( StaticManager.UiInventory.AttachedWeapons.Count > 0 ){
                StaticManager.UiInventory.AttachedWeapons.Add(this);
                var ob = StaticManager.UiInventory.AttachedWeapons[0];
                StaticManager.UiInventory.AttachedWeapons.RemoveAt(0);
                if (ob.isMainInventory)
                {
                    StaticManager.UiInventory.AddSlot(ob);
                }
                else
                {
                    StaticManager.UiInventory.AddBackpackSlot(ob);
                }

                ob.gameObject.SetActive(false);
            }
           
            this.GetComponent<BoxCollider>().enabled = false;

            StaticManager.UiInventory.RemoveMainInventory(this);
            this.gameObject.transform.position = StaticManager.Character.Cube.transform.position;
            this.gameObject.transform.rotation = StaticManager.Character.Cube.transform.rotation;
            this.gameObject.transform.SetParent(StaticManager.Character.Cube.transform);
            
            //StaticManager.Inventory.IncreaseStats(this.WeaponStats);
            collider.gameObject.GetComponent<Stat>().AdjustScale(collider.gameObject.GetComponent<Stat>().Strength);
        }
    }
    
    public WeaponItem WeaponStats
    {
        get { return weaponStats; }
    }
}
