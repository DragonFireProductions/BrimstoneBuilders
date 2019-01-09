using System.Collections;
using System.Collections.Generic;

using UnityEditor;

using UnityEngine;
using UnityEngine.UI;

public class CharacterArmorInventory : PlayerInventory
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

          [ HideInInspector ] public List < ArmorItem > pickedupArmor;
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
    public override void PickUp(BaseItems item) {

        pickedupArmor.Add(item as ArmorItem);
        var i = item as ArmorItem;
        i.PickUp(companion);
          item.gameObject.SetActive(false);


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
        obj.transform.localScale = new Vector3(1,1,1);
        labels.Add(obj);
       
    }

    public void Switch(ref GameObject current_game_object) {
        parent.SetActive(true);
        current_game_object = parent;
         FixLayout( );
    }

    public void DeleteLabel(GameObject label ) {
        labels.Remove( label );
    }
    public void FixLayout( ) {
       
        for ( int i = 0 ; i < labels.Count ; i++ ){
            labels[ i ].gameObject.transform.position = StaticManager.uiManager.ArmorGrid[ i ].transform.position;
        }
    }
}