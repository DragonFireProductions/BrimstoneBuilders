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

        UI = GameObject.FindGameObjectWithTag("UI");
        Assert.IsNotNull(UI, "Inventory UI not found");

        itemSlot = GameObject.Find("InventoryContainer");
        Assert.IsNotNull(itemSlot, "itemSlot = null");

        UI.SetActive(isActive);
        uiList = new List<GameObject>();
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
    private void displayInventoryUI()
    {
        itemSlot.SetActive(false);

        Vector3 pos = itemSlot.gameObject.transform.position;
        float posy = itemSlot.gameObject.transform.position.y;
        
        for (int i = 0; i < weapons.Count; i++)
        {
            GameObject _itemSlot = Instantiate(this.itemSlot);
            _itemSlot.transform.SetParent(itemSlot.transform.parent);
            _itemSlot.SetActive(true);

            _itemSlot.transform.localScale = itemSlot.transform.localScale;
            pos.y = posy;
            _itemSlot.gameObject.transform.position = pos;


            _itemSlot.GetComponentInChildren<RawImage>().texture = weapons[i].WeaponStats.icon;

            _itemSlot.GetComponentInChildren<TextMeshProUGUI>().text = weapons[i].WeaponStats.objectName;

            Transform container = _itemSlot.transform.Find("InventoryContainerPanel");
            TextMeshProUGUI[] transforms = container.GetComponentsInChildren<TextMeshProUGUI>();

            for (int j = 0; j < transforms.Length; j++)
            {
                switch (transforms[j].name)
                {
                    case "ItemName":
                        transforms[j].GetComponent<TextMeshProUGUI>().text = weapons[i].WeaponStats.objectName.ToString();
                        break;
                    case "ItemValue":
                        transforms[j].GetComponent<TextMeshProUGUI>().text = weapons[i].WeaponStats.value.ToString();
                        break;
                    case "ItemWeight":
                        transforms[j].GetComponent<TextMeshProUGUI>().text = weapons[i].WeaponStats.weight.ToString();
                        break;
                    case "ItemDurability":
                        transforms[j].GetComponent<TextMeshProUGUI>().text = weapons[i].WeaponStats.durability.ToString();
                        break;
                    default:
                        break;
                }
            }

            uiList.Add(_itemSlot);

        }


    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            isActive = !isActive;
            UI.SetActive(isActive);
            if (isActive == true)
            {
                displayInventoryUI();
            }

            if (isActive == false)
            {
                clearInventory();
            }
        }

        //calls select() on wanted weapon
        if (Input.GetKeyDown(KeyCode.L))
        {
            attachedWeapon.gameObject.SetActive(true);
            attachedWeapon.Select();
        }
    }

    public void clearInventory()
    {
        for (int i = 0; i < uiList.Count; i++)
        {
            UnityEngine.Object.Destroy(uiList[i].gameObject);
        }
        uiList.Clear();
    }

    public void add(WeaponObject item)
    {
        weapons.Add(item);
        attachedWeapon = item;
        item.gameObject.SetActive(false);

        Debug.Log("Item: " + item.WeaponStats.objectName + " has been added!");
    }

    public void remove(string name)
    {
        for (int i = 0; i < weapons.Count; i++)
        {
            if (weapons[i].WeaponStats.objectName == name)
            {
                weapons.RemoveAt(i);
            }
        }
    }
}
