using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterWeaponsInventory : PlayerInventory
{
    [HideInInspector] public List<WeaponObject> PickedUpWeapons; //Current list of items the player has picked up

    public List<WeaponObject> AttachedWeapons = new List<WeaponObject>();

    [HideInInspector] public WeaponObject selectedObject { get; set; }

    public List<UIItemsWithLabels> weapons = new List<UIItemsWithLabels>();

    public GameObject inventoryObj;
    // Use this for initialization
    void Start()
    {

    }

    public void RemoveObject(WeaponObject item ) {
       
            for (var i = 0; i < weapons.Count; i++)
            {
                if (weapons[i].item == item)
                {
                    weapons[i].obj.SetActive(false);
                }
            }
    }

    public void DeleteObject(WeaponObject item ) {
        for (var i = 0; i < weapons.Count; i++)
        {
            if (weapons[i].item == item)
            {
                var a = weapons[i].obj;
                weapons.RemoveAt(i);
                Destroy(a);
            }
        }
    }
    public void UpdateItem()
    {
        foreach (var l_t in weapons)
        {
            for (var j = 0; j < l_t.Labels.Count; j++)
            {
                l_t.obj.SetActive(l_t.item != character.attachedWeapon);
                var a = l_t.item.stats[l_t.Labels[j].name];
                l_t.Labels[j].labelText.text = a.ToString();

            }
            if (l_t.GetComponent<Tab>().imageContainer && l_t.item.stats.icon)
            {
                l_t.GetComponent<Tab>().imageContainer.texture = l_t.item.stats.icon;
            }
        }
    }
    public void UpdateWeapon(UIItemsWithLabels item, WeaponObject weapon)
    {
        for (int i = 0; i < item.Labels.Count; i++)
        {
            item.Labels[i].labelText.text = weapon[item.Labels[i].name].ToString();
        }
    }
    public void EnableContainer(WeaponObject item)
    {
        foreach (var l_uiItemsWithLabelse in weapons)
        {
            if (l_uiItemsWithLabelse.item == item)
            {
                l_uiItemsWithLabelse.obj.SetActive(true);
                item.gameObject.SetActive(false);
            }
        }
    }
    public void Init( ) {
        inventoryObj = character.inventoryUI.CharacterInventory.transform.Find("WeaponsInventory").gameObject;

        inventoryObj.SetActive(true);
    }

    public override void PickUp(BaseItems item)
    {
        base.PickUp(item);

        var newlabel = Instantiate(StaticManager.uiManager.Weapon.gameObject);
        newlabel.GetComponent<Tab>().companion = character;
        newlabel.GetComponent<Tab>().item = item;



        var l = newlabel.GetComponent<UIItemsWithLabels>();

        l.obj = newlabel;

        l.item = item;

        PickedUpWeapons.Add(l.item as WeaponObject);

        l.obj.transform.SetParent(inventoryObj.transform);

        l.obj.transform.position = StaticManager.uiManager.Grid[PickedUpWeapons.Count - 1].transform.position;

        l.obj.transform.localScale = new Vector3(1, 1, 1);

        l.FindLabels();

        newlabel.SetActive(true);

        var i = item as WeaponObject;

        i.label = l.obj;

        weapons.Add(l);

        item.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
public class WeaponsStuff
{

    public Text text;

    public GameObject parent;

    public List<BaseItems> items = new List<BaseItems>();

    public List<GameObject> labels = new List<GameObject>();

    public WeaponsStuff(string objName, string textName, GameObject armorInventory)
    {
        text = armorInventory.transform.Find(textName).GetComponent<Text>();
        parent = armorInventory.transform.Find(objName).gameObject;
        parent.SetActive(false);
    }

    public void Add(GameObject obj, BaseItems item)
    {
        obj.transform.SetParent(parent.transform);
        items.Add(item);
        obj.transform.position = StaticManager.uiManager.ArmorGrid[items.Count - 1].transform.position;
        text.text = items.Count.ToString();
        obj.transform.localScale = new Vector3(1, 1, 1);
        labels.Add(obj);

    }

    public void Switch(ref GameObject current_game_object)
    {
        parent.SetActive(true);
        current_game_object = parent;
        FixLayout();
    }

    public void DeleteLabel(GameObject label)
    {
        labels.Remove(label);
    }
    public void FixLayout()
    {

        for (int i = 0; i < labels.Count; i++)
        {
            labels[i].gameObject.transform.position = StaticManager.uiManager.ArmorGrid[i].transform.position;
        }
    }
}
