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

    /// <summary>
    /// InventoryContainer (The whole object containing variables of items)
    /// </summary>
    [SerializeField] GameObject container;

    /// <summary>
    /// The String variables to set in the UI
    /// </summary>
    [SerializeField] GameObject variables;

    /// <summary>
    /// Pause menu
    /// </summary>
    [SerializeField] private GameObject pause;

    /// <summary>
    /// Player UI
    /// </summary>
    [SerializeField] public GameObject PlayerUi;

    /// <summary>
    /// Dialog UI
    /// </summary>
    [ SerializeField ] public GameObject DialogUI;

    [ SerializeField ] private TextMeshProUGUI DialogText;
    /// <summary>
    /// Controls where the new UI item will be located
    /// </summary>
    private Vector3 pos;

    /// <summary>
    /// The list of current slots in the UI
    /// </summary>
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
        DialogWindowShow(false);
    }

    public void ShowNotification(string _message ) {
        UIInventory.instance.DialogWindowShow(true);

        DialogText.text = _message;
    }

    public void DialogWindowShow(bool active ) {
        DialogUI.SetActive(active);
    }
    /// <summary>
    /// Adds a new item slot to UI
    /// </summary>
    /// <param name="item">Weapon that is picked up</param>
    public void AddSlot(WeaponObject item)
    {
        GameObject newContainer = Instantiate(container);
        newContainer.SetActive(true);
        newContainer.transform.SetParent(container.transform.parent);
        newContainer.transform.position = pos;
        newContainer.transform.localScale = container.transform.localScale;
        Set(item, newContainer);
        slots.Add(newContainer);
    }

    /// <summary>
    /// Removes an item from the UI
    /// </summary>
    /// <param name="item"></param>
    public void Remove(WeaponObject item)
    {
        for (int i = 0; i < slots.Count; i++)
        {
            if (slots[i].name  == item.name + "Slot")
            {
                GameObject slot = slots[i].gameObject;
                slots.RemoveAt(i);
                Destroy(slot);
            }
        }
    }
    /// <summary>
    /// Sets the variables in the UI to the weapon stats passed in
    /// </summary>
    /// <param name="_weapons">Weapon added to UI</param>
    /// <param name="_container_p">The UI container</param>
    public void Set(WeaponObject _weapons, GameObject _container_p)
    {
        TextMeshProUGUI[] transforms = _container_p.GetComponentsInChildren<TextMeshProUGUI>();
        _container_p.name = _weapons.name + "Slot";
        for (int j = 0; j < transforms.Length; j++)
        {
            switch (transforms[j].name)
            {
                case "ItemName":
                    transforms[j].GetComponent<TextMeshProUGUI>().text = _weapons.WeaponStats.objectName.ToString();
                    break;
                case "ItemValue":
                    transforms[j].GetComponent<TextMeshProUGUI>().text = _weapons.WeaponStats.value.ToString();
                    break;
                case "ItemWeight":
                    transforms[j].GetComponent<TextMeshProUGUI>().text = _weapons.WeaponStats.weight.ToString();
                    break;
                case "ItemDurability":
                    transforms[j].GetComponent<TextMeshProUGUI>().text = _weapons.WeaponStats.durability.ToString();
                    break;
                default:
                    break;
            }
        }
        _container_p.GetComponentInChildren<RawImage>().texture = _weapons.WeaponStats.icon;
    }

    /// <summary>
    /// Shows the pause menu
    /// </summary>
    /// <param name="showPause">Bool that determines whether it's shown or not</param>
    public void ShowPauseMenu(bool showPause)
    {
        pause.SetActive(showPause);
    }

    // Update is called once per frame
    void Update () {
	}
    /// <summary>
    /// Selects the item in the UI to attach to player
    /// </summary>
    /// <param name="weapon"></param>
    public void selected(WeaponObject weapon)
    {
        Debug.Log(weapon.WeaponStats.objectName + "was clicked");
        weapon.SelectItem();
    }


}
