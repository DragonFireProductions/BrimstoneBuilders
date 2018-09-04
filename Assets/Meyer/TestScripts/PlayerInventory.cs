using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Experimental.UIElements;
using Object = System.Object;

public class PlayerInventory : MonoBehaviour
{
    public static PlayerInventory inventory = null;
    public List<WeaponItem> weapons;
    public WeaponItemList item;

    public static List<WeaponItem> list;



    public static GameObject UI;
    private GameObject itemSlot;
    public List<GameObject> uiList;

    private bool isActive = false;
    // Use this for initialization
    void Awake() {
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
        Vector3 pos = itemSlot.gameObject.transform.position;
        float posy = itemSlot.gameObject.transform.position.y;
        for (int i = 0; i < weapons.Count; i++)
        {
            GameObject _itemSlot = Instantiate(this.itemSlot);
            _itemSlot.transform.SetParent(itemSlot.transform.parent);
            _itemSlot.transform.localScale = itemSlot.transform.localScale;
            pos.y = posy;
            _itemSlot.gameObject.transform.position = pos;
            _itemSlot.GetComponentInChildren<TextMeshProUGUI>().text = weapons[i].objectName;
            uiList.Add(_itemSlot);
        }
    }

    // Update is called once per frame
    void Update () {
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
    }

    public void clearInventory()
    {
        for (int i = 0; i < uiList.Count; i++)
        {
           UnityEngine.Object.Destroy(uiList[i].gameObject);
        }
        uiList.Clear();
    }

    public void add(WeaponItem item)
    {
        weapons.Add(item);
        Debug.Log("Item: " + item.objectName + " has been added!");
        Debug.Log("Item's: attack speed is: " + item.attackSpeed);
    }

    public void remove(string name)
    {
        for (int i = 0; i < weapons.Count; i++)
        {
            if (weapons[i].objectName == name)
            {
                weapons.RemoveAt(i);
            }
        }
    }
}
