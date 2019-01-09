using System.Collections;
using System.Collections.Generic;

using UnityEditor;

using UnityEngine;
using UnityEngine.UI;

public class CharacterArmorInventory : MonoBehaviour
    {

        public List<UIItemsWithLabels> armor;

        public GameObject ArmorInventory;

        public Companion companion;

        public ArmorStuff Shoes;

        public ArmorStuff Shoulder;

        public ArmorStuff Head;

        public ArmorStuff Belt;

        public ArmorStuff Clothes;

         public GameObject currentArmorTab;

        public Text Headcount;

        public Text shoulderCount;

        public Text ShoeCount;

        public Text beltCount;

        public Text clothesCount;
        // Use this for initialization
        public void Init()
        {
            armor = new List<UIItemsWithLabels>();
           
            ArmorInventory = companion.inventoryUI.CharacterInventory.transform.Find("ArmorInventory").gameObject;

            ArmorInventory.SetActive(false);
            Clothes = new ArmorStuff("Clothes", "ClothesCount", ArmorInventory);
            Shoulder = new ArmorStuff("Shoulder", "ShoulderCount", ArmorInventory);
            Head = new ArmorStuff("Head", "HeadCount", ArmorInventory);
            Belt = new ArmorStuff("Belt", "BeltCount", ArmorInventory);
            Shoes = new ArmorStuff("Shoes", "ShoeCount", ArmorInventory);

    }
    public void AddArmor(BaseItems item)
        {
            var newlabel = Instantiate(StaticManager.uiManager.Armor.gameObject);
            newlabel.GetComponent<Tab>().companion = companion;
            newlabel.GetComponent<Tab>().item = item;

            var i = item as ArmorItem;
            i.tab = newlabel.GetComponent<Tab>();

            var l = newlabel.GetComponent<UIItemsWithLabels>();

            l.obj = newlabel;

            l.item = item;

            l.obj.transform.position = StaticManager.uiManager.Armor.gameObject.transform.position;

            l.obj.transform.SetParent(ArmorInventory.transform);

            l.obj.transform.localScale = new Vector3(1, 1, 1);

            l.FindLabels();

            item.AttachedCharacter = companion;

            newlabel.SetActive(true);

            armor.Add(l);

            switch (i.type)
            {
                case ArmorItem.Type.Belt:
                    Belt.Add(l.obj, item);
                    break;
                case ArmorItem.Type.Clothes:
                    Clothes.Add(l.obj, item);
                    break;
                case ArmorItem.Type.Head:
                  Head.Add(l.obj, item);
                    break;
                case ArmorItem.Type.Shoe:
                    Shoes.Add(l.obj, item);
                    break;
                case ArmorItem.Type.Shoulder:
                    Shoulder.Add(l.obj, item);
                    break;
            }

           

        }

        public void UpdateArmor()
        {
            for (var i = 0; i < armor.Count; i++)
            {
                for (var j = 0; j < armor[i].Labels.Count; j++)
                {
                    var a = armor[i].item.stats[armor[i].Labels[j].name];
                    armor[i].Labels[j].labelText.text = a.ToString();
                }
            }
        }
        // Update is called once per frame
        void Update()
        {

        }
    }

public class ArmorStuff {

    public Text text;

    public GameObject parent;

    public List < BaseItems > items = new List < BaseItems >();

    public List <GameObject> labels = new List < GameObject >();

    public ArmorStuff(string objName , string textName, GameObject armorInventory) {
        text = armorInventory.transform.Find( textName ).GetComponent < Text >( );
        parent = armorInventory.transform.Find( objName ).gameObject;
        parent.SetActive(false);
    }

    public void Add(GameObject obj, BaseItems item ) {
        obj.transform.SetParent(parent.transform);
        items.Add(item);
        obj.transform.position = StaticManager.uiManager.ArmorGrid[items.Count - 1].transform.position;
        text.text = items.Count.ToString();
        labels.Add(obj);
    }

    public void Switch(ref GameObject current_game_object) {
        parent.SetActive(true);
        current_game_object = parent;
    }

    public void Equip( ) {
       
        for ( int i = 0 ; i < labels.Count ; i++ ){
            
        }
    }
}