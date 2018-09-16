using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Experimental.UIElements;
using UnityEngine.UI;
using Object = System.Object;



public class PlayerInventory : MonoBehaviour
{
    public static PlayerInventory inventory = null; //Global variable
    public List<WeaponObject> weapons; //Contains list of picked up WeaponObjects
    public WeaponItemList item; // Holder for Unity defined weapons list

    public static List<WeaponItem> list; //List of weapon stats defined by unity

    public static GameObject UI; // UI stuff
    private GameObject itemSlot; // Slots for items
    private List<GameObject> uiList; // List of weaponItems currently picketUp

    private bool isActive = false;
    public List<WeaponObject> objects;

    [SerializeField]
    public static WeaponObject attachedWeapon; // Primary weapon holder
    // Use this for initialization
    void Awake()
    {
        if (inventory == null)
        {
            inventory = this;
        }
        else if (inventory != this)
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);

        list = item.itemList;
        
        uiList = new List<GameObject>();
    }

    void Start()
    {
        UIInventory.instance.playerUI.SetActive(isActive);
    }
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
            if (isActive == true)
            {
                UIInventory.instance.playerUI.SetActive(true);
            }

            if (isActive == false)
            {
               UIInventory.instance.playerUI.SetActive(false);
            }
        }

        //calls select() on wanted weapon
        if (Input.GetKeyDown(KeyCode.L))
        {
            attachedWeapon.GetComponent<Collider>().enabled = false;
            attachedWeapon.gameObject.SetActive(true);
            attachedWeapon.transform.position = GameObject.FindGameObjectWithTag("Player").transform.position;
            attachedWeapon.transform.rotation = GameObject.FindGameObjectWithTag("Player").transform.rotation;

            attachedWeapon.gameObject.transform.parent = GameObject.FindGameObjectWithTag("Player").transform;
        }
    }
    
    public void add(WeaponObject item)
    {
        //weapons.Add(item);
        //attachedWeapon = item;
        item.gameObject.SetActive(false);
        objects.Add(item);
        UIInventory.instance.AddSlot(item);
        Debug.Log("Item: " + item.WeaponStats.objectName + " has been added!");
    }

    public WeaponObject GetWeapon()
    {
        return attachedWeapon;
    }
}
