using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
public class WeaponObject : MonoBehaviour
{
    /// Variables should be protected NOT public or private
    
    protected GameObject weapon; // gameObject this is attached too
    [SerializeField] protected WeaponItem weaponStats; // contains inventory information

    [SerializeField] protected string weaponName; // references InventoryManager items



    protected virtual void Start()
    {
        weaponStats = PlayerInventory.inventory.get_item(weaponName);
        Assert.IsNotNull(weaponStats, "WeaponItem name not added in inspector " + gameObject.name);
        weapon = this.gameObject;
    }

    //TODO:2 Add function for selection in UI
    //Function should be called when player wants to select this as their primary object
    public virtual void Select()
    {
        Debug.Log("Object has been selected!");
    }

    protected virtual void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "Player")
        {
            PlayerInventory.inventory.add(this);
        }
    }

    public WeaponItem WeaponStats
    {
        get { return weaponStats; }
    }
}
