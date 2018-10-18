using System;
using System.Collections;
using System.Collections.Generic;

using Assets.Meyer.TestScripts.Player;

using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Experimental.UIElements;
using UnityEngine.UI;
using Object = System.Object;



public class PlayerInventory : MonoBehaviour
{
    /// <summary>
    /// Contains list of current weapons in player inventory
    /// </summary>
    public List<WeaponObject> weapons; //Contains list of picked up WeaponObjects


    /// <summary>
    /// Contains list of items created using Editor Window
    /// </summary>
    public WeaponItemList item; // Holder for Unity defined weapons list


    public static List<WeaponItem> list; //List of weapon stats defined by unity


    /// <summary>
    /// Static instance of UI game object
    /// </summary>
    public static GameObject UI; // UI stuff

    /// <summary>
    /// Slots to be instantiated when object is picked up
    /// </summary>
    private GameObject itemSlot; // Slots for items


    private List<GameObject> uiList; // List of weaponItems currently pickedUp

    private bool isActive = false;
    public List<WeaponObject> objects;

    public List < WeaponObject > backpackInventory;

    [SerializeField]
    public GameObject attachedWeapon; // Primary weapon holder

    public WeaponObject selectedItem;
    // Use this for initialization
    void Awake()
    {
        list = item.itemList;
        
        uiList = new List<GameObject>();
    }

    void Start()
    {
      StaticManager.uiInventory.attachedWeapons.Add(attachedWeapon.GetComponent<WeaponObject>());
      StaticManager.inventory.objects.Add(attachedWeapon.GetComponent<WeaponObject>());
      StaticManager.uiInventory.itemsInstance.PlayerUI.SetActive(isActive);
    }

    /// <summary>
    /// Gets the item from list created by the Editor Window
    /// </summary>
    /// <param name="name">String to find</param>
    /// <returns>Weapon item found</returns>
    public WeaponItem get_item(string name)
    {
        for (int i = 0; i < list.Count; i++)
        {
            if (list[i].objectName.ToLower() == name.ToLower())
            {
                return list[i];
            }
        }
        Debug.Log(name + ": ASSET NOT FOUND");
        return null;
    }
    

    /// <summary>
    /// Gets the weapon currently in player inventory while game is playing
    /// </summary>
    /// <param name="name">Weapon to find</param>
    /// <returns>Weapon object </returns>
    public WeaponObject get_weapon(string name)
    {
        for (int i = 0; i < objects.Count; i++)
        {
            if (objects[i].WeaponStats.objectName == name)
            {
                return objects[i];
            }
        }

        return null;
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Inventory"))
        {
            isActive = !isActive;
            StaticManager.character.controller.SetControlled(!isActive);

            if (isActive == true)
            {
              StaticManager.uiInventory.itemsInstance.PlayerUI.SetActive(true);
            }

            if (isActive == false)
            {
             StaticManager.uiInventory.itemsInstance.PlayerUI.SetActive(false);
            }
        }
        
    }

    /// <summary>
    /// Adds item picked up to inventory
    /// </summary>
    /// <param name="item">Name of item picked up</param>
    public void add(WeaponObject item)
    {
        item.gameObject.SetActive(false);
        objects.Add(item);
        StaticManager.uiInventory.AddSlot(item);
        Debug.Log("Item: " + item.WeaponStats.objectName + " has been added!");
    }

    public void addToBackpack( WeaponObject item ) {
        item.gameObject.SetActive(false);
        backpackInventory.Add(item);
        StaticManager.uiInventory.AddBackpackSlot(item);
    }

    public void moveToBackPack() {
        StaticManager.uiInventory.itemsInstance.WeaponOptions.SetActive(false);
        StaticManager.uiInventory.Remove(selectedItem);
        backpackInventory.Add(selectedItem);
        StaticManager.uiInventory.AddBackpackSlot(selectedItem);
        StaticManager.uiInventory.itemsInstance.BackPackUI.GetComponentInChildren<RawImage>().texture = selectedItem.WeaponStats.icon;
        StaticManager.uiInventory.itemsInstance.BackPackUI.GetComponentInChildren<TextMeshProUGUI>().text = selectedItem.WeaponStats.objectName;
        StaticManager.uiInventory.ShowBackPackInventory(true);
    }

    public void ViewStats( ) {
        StaticManager.uiInventory.ShowWeaponOptions(false);
        StaticManager.uiInventory.ShowInventoryWeaponStats(true);
        StaticManager.uiInventory.UpdateWeaponInventoryStats(selectedItem.WeaponStats);
    }

   
}
