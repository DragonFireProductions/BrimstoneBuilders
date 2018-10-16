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
        weaponStats = StaticManager.inventory.get_item(weaponName);
        Assert.IsNotNull(weaponStats, "WeaponItem name not added in inspector " + gameObject.name);
        weapon = this.gameObject;
    }

    public virtual object this[ string propertyName ] {
        get { return this.GetType().GetField(propertyName).GetValue(this); }
        set { this.GetType().GetField(propertyName).SetValue(this, value); }
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
    protected void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "Player")
        {
            StaticManager.inventory.add(this);
            this.GetComponent < BoxCollider >( ).enabled = false;
        }
    }


    /// <summary>
    /// Attaches weapon to player transform and adds it to UI
    /// </summary>
    public void SelectItem()
    {
       
        PlayerInventory.attachedWeapon = this;

        StaticManager.uiInventory.Remove(this);
        gameObject.SetActive(true);

        gameObject.transform.position = StaticManager.character.cube.transform.position;
        gameObject.transform.rotation = StaticManager.character.cube.transform.rotation;
        gameObject.transform.parent = StaticManager.character.cube.transform;

    }
    
    /// <summary>
    /// Returns stats attached to this game object
    /// </summary>
    public WeaponItem WeaponStats
    {
        get { return weaponStats; }
    }
}
