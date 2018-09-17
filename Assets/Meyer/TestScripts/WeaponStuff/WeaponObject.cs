using System.Collections;
using System.Collections.Generic;
using Assets.Meyer.TestScripts.Player;
using UnityEngine;
using UnityEngine.Assertions;
public class WeaponObject : MonoBehaviour
{
    /// Variables should be protected NOT public or private
    
    protected GameObject weapon; // gameObject this is attached too
    [SerializeField] protected WeaponItem weaponStats; // contains inventory information

    [SerializeField] protected string weaponName; // references InventoryManager items
    protected Animator animator;


    protected virtual void Start()
    {
        animator = gameObject.GetComponent<Animator>();
        weaponStats = PlayerInventory.inventory.get_item(weaponName);
        Assert.IsNotNull(weaponStats, "WeaponItem name not added in inspector " + gameObject.name);
        weapon = this.gameObject;
    }

    public void PlayUsing()
    {
        animator.SetBool("Attacking", true);
    }

    public void StopUsing()
    {
        animator.SetBool("Attacking", false);
    }
    //TODO:2 Add function for selection in UI
    //Function should be called when player wants to select this as their primary object
    public virtual void Select()
    {
        Debug.Log("Object has been selected!");
    }

    public virtual void Attack()
    {
          Debug.Log("Object has attacked!");
    }

    protected virtual void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "Player")
        {
            PlayerInventory.inventory.add(this);
        }
    }

    public void SelectItem()
    {
        if (PlayerInventory.attachedWeapon != null)
        {
            UIInventory.instance.AddSlot(PlayerInventory.attachedWeapon);
            PlayerInventory.attachedWeapon.gameObject.SetActive(false);
            PlayerInventory.attachedWeapon = null;
        }
        PlayerInventory.attachedWeapon = this;

        UIInventory.instance.Remove(this);
        gameObject.SetActive(true);
        gameObject.GetComponent<BoxCollider>().enabled = false;

        gameObject.transform.position = Character.instance.weaponAttach.transform.position;
        gameObject.transform.rotation = Character.instance.weaponAttach.transform.rotation;

        gameObject.transform.parent = Character.player.transform;
    
    }
    
    public WeaponItem WeaponStats
    {
        get { return weaponStats; }
    }
}
