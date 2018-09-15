using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Container
{
   
}

public class UIInventory : MonoBehaviour
{
    public static UIInventory instance;

    [SerializeField] GameObject container;
    [SerializeField] GameObject variables;
    [SerializeField] private GameObject pause;
    [SerializeField] public GameObject playerUI;

    private Vector3 pos;

    public List<GameObject> slots;
    // Use this for initialization
    void Awake () {
	    if (instance == null)
	    {
	        instance = this;
	    }
	    else if (instance != this)
	        Destroy(gameObject);
	    DontDestroyOnLoad(gameObject);
	}

    void Start()
    {
        pos = container.gameObject.transform.position;
    }
    public void AddSlot(WeaponObject item)
    {
        GameObject newContainer = Instantiate(container);
        newContainer.SetActive(true);
        newContainer.transform.SetParent(container.transform.parent);
        newContainer.transform.position = pos;
        newContainer.transform.localScale = container.transform.localScale;
        set(item, newContainer);
        slots.Add(newContainer);
    }

    public void Remove(WeaponObject item)
    {
        for (int i = 0; i < slots.Count; i++)
        {
            if (slots[i].name == item.WeaponStats.objectName)
            {
                GameObject slot = slots[i].gameObject;
                slots.RemoveAt(i);
                Destroy(slot);
            }
        }
    }
    
    public void set(WeaponObject weapons, GameObject containerP)
    {
        TextMeshProUGUI[] transforms = containerP.GetComponentsInChildren<TextMeshProUGUI>();
        containerP.name = weapons.name + "Slot";
        for (int j = 0; j < transforms.Length; j++)
        {
            switch (transforms[j].name)
            {
                case "ItemName":
                    transforms[j].GetComponent<TextMeshProUGUI>().text = weapons.WeaponStats.objectName.ToString();
                    break;
                case "ItemValue":
                    transforms[j].GetComponent<TextMeshProUGUI>().text = weapons.WeaponStats.value.ToString();
                    break;
                case "ItemWeight":
                    transforms[j].GetComponent<TextMeshProUGUI>().text = weapons.WeaponStats.weight.ToString();
                    break;
                case "ItemDurability":
                    transforms[j].GetComponent<TextMeshProUGUI>().text = weapons.WeaponStats.durability.ToString();
                    break;
                default:
                    break;
            }
        }
        containerP.GetComponentInChildren<RawImage>().texture = weapons.WeaponStats.icon;
    }

    
    public void ShowPauseMenu(bool showPause)
    {
        pause.SetActive(showPause);
    }

    // Update is called once per frame
    void Update () {
        ClickAndDrag();
	}

    public void selected(WeaponObject weapon)
    {
        Debug.Log(weapon.WeaponStats.objectName + "was clicked");
        PlayerInventory.attachedWeapon = weapon;
        weapon.SelectItem();
    }


    void ClickAndDrag()
    {

    }
}
