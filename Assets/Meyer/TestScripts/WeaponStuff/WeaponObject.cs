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
    protected Animation animator;


    protected virtual void Start()
    {
        animator = gameObject.GetComponent<Animation>();
        weaponStats = PlayerInventory.inventory.get_item(weaponName);
        Assert.IsNotNull(weaponStats, "WeaponItem name not added in inspector " + gameObject.name);
        weapon = this.gameObject;
    }


    /// <summary>
    /// Plays the animation attached to weapon
    /// </summary>
    public void PlayUsing()
    {
        if (animator)
        {
        animator.Play();

        }
    }

    public void StopUsing()
    {
    }

    //Function should be called when player wants to select this as their primary object
    public virtual void Select()
    {
        Debug.Log("Object has been selected!");
    }

    public virtual void Attack()
    {
          Debug.Log("Object has attacked!");
    }


    /// <summary>
    /// Picks up item
    /// </summary>
    /// <param name="collider">Item it collides with</param>
    protected virtual void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "Player")
        {
            PlayerInventory.inventory.add(this);
            if (PlayerInventory.attachedWeapon == null)
            {
                
                SelectItem();
            }
        }
    }


    /// <summary>
    /// Attaches weapon to player transform and adds it to UI
    /// </summary>
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
    
    /// <summary>
    /// Returns stats attached to this game object
    /// </summary>
    public WeaponItem WeaponStats
    {
        get { return weaponStats; }
    }
}
