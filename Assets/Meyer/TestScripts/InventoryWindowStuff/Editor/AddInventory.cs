using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
public class WeaponWindow : EditorWindow
{
    WeaponList weaponList;
    int index = 0;
    private void OnEnable()
    {
        if (EditorPrefs.HasKey("ObjectPath"))
        {
            string objectPath = EditorPrefs.GetString("ObjectPath");
            weaponList = AssetDatabase.LoadAssetAtPath(objectPath, typeof(WeaponList)) as WeaponList;
        }
    }
    private void OnGUI()
    {
        GUILayout.BeginHorizontal();
        GUILayout.Label("Weapons Inventory Editor", EditorStyles.boldLabel);
        if (GUILayout.Button("Show Item List"))
        {
            EditorUtility.FocusProjectWindow();
            Selection.activeObject = weaponList;
        }
        if (GUILayout.Button("Open Existing inventory"))
        {
            string absPath = EditorUtility.OpenFilePanel("Select Inventory Item List", "", "");
            if (absPath.StartsWith(Application.dataPath))
            {
                string relPath = absPath.Substring(Application.dataPath.Length - "Assets".Length);
                weaponList = AssetDatabase.LoadAssetAtPath(relPath, typeof(WeaponList)) as WeaponList;
                if (weaponList.weaponList == null)
                    weaponList.weaponList = new List<Weapons>();
                if (weaponList)
                {
                    EditorPrefs.SetString("ObjectPath", relPath);
                }
            }
        }
        if (GUILayout.Button("Add Item"))
        {
            AddItem();
        }
        if (weaponList != null)
        {
            GUILayout.BeginHorizontal();
            GUILayout.Space(10);

        }
    }
    void AddItem()
    {

    }
}

public class ArmorWindow : EditorWindow
{

}
public class AddInventory : EditorWindow
{

    int tab = 0;
    int tab1 = 0;
    public Object icon;
    List<string> errorlist;
    public string[] strings = new string[] { "Add", "View", "Open" };
    public string[] strings1 = new string[] { "Weapon", "Armor" };

    public int weight = 1000, value = 1000, damage = 1000, reach = 1000, durability = 1000, speed = 1000, baseArmor = 1;
    public string _name = "newItem";
    public bool isIndestructible, isStackable = false, destroyOnUse = false;

    string[] weapontype = new string[] { "Type1", "Type2", "Type3", "Type4" };
    int weaponchoice = 0;

    string[] options = new string[] { "H1", "H2", "Shield", "Shoulder" };
    int option = 0;


    string[] equipmentSlot = new string[] { "Head", "Pauldrons", "Cuirass", "Gauntlets", "Greaves", "Boots", "BackorCape", "Rings", "Amulet" };
    string[] weightClass = new string[] { "Light", "Medium", "Heavy" };
    int slotchoice = 0;
    int classchoice = 0;

    WeaponList w_list;
    ArmorList a_list;

    [MenuItem("Window/Inventory Item Editor")]
    static void ShowWindow()
    {
        EditorWindow.GetWindow(typeof(AddInventory));
    }
    void OnEnable()
    {
        w_list = (WeaponList)AssetDatabase.LoadAssetAtPath("Assets/Meyer/TestScripts/InventoryWindowStuff/WeaponList.asset", typeof(WeaponList));
        a_list = (ArmorList)AssetDatabase.LoadAssetAtPath("Assets/Meyer/TestScripts/InventoryWindowStuff/ArmorList.asset", typeof(ArmorList));

    }
    void resetValues()
    {
        weight = 1000; value = 1000; damage = 1000; reach = 1000; durability = 1000; speed = 1000; baseArmor = 1;
        weaponchoice = 0;
        option = 0;
        classchoice = 0;
        slotchoice = 0;
        _name = "newItem";
    }
    void OnGUI()
    {
        tab1 = GUILayout.Toolbar(tab1, strings1);
        switch (tab1)
        {
            case 0:
                {
                    tab = GUILayout.Toolbar(tab, strings);

                    switch (tab)
                    {
                        case 0:
                            {
                                GUILayout.Space(40);

                                ///Sets name
                                GUILayout.Label("Object Name: ");
                                _name = GUILayout.TextField(_name, 17);

                                GUILayout.Space(40);
                                GUILayout.BeginHorizontal();

                                ///Sets icon
                                GUILayout.Label("Object Icon:");
                                icon = EditorGUILayout.ObjectField(icon, typeof(Texture2D), true) as Texture2D;

                                GUILayout.EndHorizontal();
                                GUILayout.Space(40);
                                GUILayout.BeginHorizontal();
                                GUILayout.Space(10);

                                ///Sets weight
                                GUILayout.Label("Weight: ", GUILayout.ExpandWidth(false));
                                weight = EditorGUILayout.IntField(weight, GUILayout.ExpandWidth(false));

                                GUILayout.Space(30);

                                ///Sets durability
                                GUILayout.Label("Durability: ", GUILayout.ExpandWidth(false));
                                durability = EditorGUILayout.IntField(durability, GUILayout.ExpandWidth(false));

                                GUILayout.Space(30);

                                ///Sets value 
                                GUILayout.Label("Value :", GUILayout.ExpandWidth(false));
                                value = EditorGUILayout.IntField(value, GUILayout.ExpandWidth(false));

                                GUILayout.Space(30);

                                ///Sets base Damage
                                GUILayout.Label("Base Damage :", GUILayout.ExpandWidth(false));
                                damage = EditorGUILayout.IntField(damage, GUILayout.ExpandWidth(false));

                                GUILayout.EndHorizontal();
                                GUILayout.Space(40);
                                GUILayout.BeginHorizontal();
                                GUILayout.Space(30);

                                ///Sets speed
                                GUILayout.Label("Speed: ", GUILayout.ExpandWidth(false));
                                speed = EditorGUILayout.IntField(speed, GUILayout.ExpandWidth(false));

                                GUILayout.Space(30);

                                ///Sets reach
                                GUILayout.Label("Reach: ", GUILayout.ExpandWidth(false));
                                reach = EditorGUILayout.IntField(reach, GUILayout.ExpandWidth(false));

                                ///Sets Equipment type
                                GUILayout.Label("Equipment type", GUILayout.ExpandWidth(false));
                                EditorGUILayout.Popup(option, options);

                                GUILayout.EndHorizontal();
                                GUILayout.Space(40);

                                ///Sets weapon Type
                                GUILayout.Label("Weapon type", GUILayout.ExpandWidth(false));
                                EditorGUILayout.Popup(weaponchoice, weapontype);

                                GUILayout.Space(20);
                                GUILayout.BeginHorizontal();

                                ///Sets bool
                                GUILayout.Label("Is Indestructible", GUILayout.ExpandWidth(false));
                                isIndestructible = EditorGUILayout.Toggle(isIndestructible, GUILayout.ExpandWidth(false));

                                ///Sets bool
                                GUILayout.Label("Is stackable", GUILayout.ExpandWidth(false));
                                isStackable = EditorGUILayout.Toggle(isStackable, GUILayout.ExpandWidth(false));

                                ///Sets bool
                                GUILayout.Label("Destroy on Use", GUILayout.ExpandWidth(false));
                                destroyOnUse = EditorGUILayout.Toggle(destroyOnUse, GUILayout.ExpandWidth(false));

                                GUILayout.EndHorizontal();
                                GUILayout.Space(20);
                                if (GUILayout.Button("Add Weapon"))
                                {
                                    string[] results;
                                    results = AssetDatabase.FindAssets(_name);
                                    if (results.Length == 0)
                                    {
                                        Weapons newWeapon = Weapons.Create(_name);
                                        newWeapon.objectName = _name;
                                        newWeapon.name = name;
                                        newWeapon.icon = icon as Texture2D;
                                        newWeapon.baseDamage = damage;
                                        newWeapon.durability = durability;
                                        newWeapon.reach = reach;
                                        newWeapon.value = value;
                                        newWeapon.weight = weight;
                                        newWeapon.attackSpeed = speed;
                                        newWeapon.equipType = (Weapons.EquipType)option;
                                        newWeapon.weaponType = (Weapons.WeaponType)weaponchoice;
                                        newWeapon.destroyOnUse = destroyOnUse;
                                        newWeapon.isStackable = isStackable;
                                        newWeapon.isIndestructible = isIndestructible;
                                        if (newWeapon.icon != null)
                                            w_list.weaponList.Add(newWeapon);
                                        else
                                            AssetDatabase.DeleteAsset("Assets/Meyer/TestScripts/InventoryWindowStuff/WeaponAssets/" + _name + ".asset");
                                        resetValues();
                                    }
                                    else
                                        Debug.Log("Inventory manager: Object already exists");
                                }
                                break;
                            }
                        case 1:
                            {
                                Rect rec = new Rect();
                                rec.x = 10;
                                rec.y = 50;
                                rec.height = 60;
                                rec.width = 60;

                                Rect buttons = new Rect();
                                buttons.x = 10;
                                buttons.y = 110;
                                buttons.height = 40;
                                buttons.width = 60;
                                for (int i = 0; i < w_list.weaponList.Count; i++)
                                {
                                    if (w_list.weaponList[i] == null)
                                    {
                                        w_list.weaponList.RemoveAt(i);
                                    }
                                    else
                                    {
                                        EditorGUI.DrawPreviewTexture(rec, w_list.weaponList[i].icon);
                                        GUILayout.BeginArea(buttons);
                                        GUILayout.BeginVertical();

                                        if (GUILayout.Button("Edit"))
                                        {
                                            _name = w_list.weaponList[i].objectName;
                                            icon = w_list.weaponList[i].icon;
                                            weight = w_list.weaponList[i].weight;
                                            damage = w_list.weaponList[i].baseDamage;
                                            durability = w_list.weaponList[i].durability;
                                            reach = w_list.weaponList[i].reach;
                                            value = w_list.weaponList[i].value;
                                            speed = w_list.weaponList[i].attackSpeed;
                                            option = (int)w_list.weaponList[i].equipType;
                                            weaponchoice = (int)w_list.weaponList[i].weaponType;
                                            destroyOnUse = w_list.weaponList[i].destroyOnUse;
                                            isStackable = w_list.weaponList[i].isStackable;
                                            isIndestructible = w_list.weaponList[i].isIndestructible;
                                            AssetDatabase.DeleteAsset("Assets/Meyer/TestScripts/InventoryWindowStuff/WeaponAssets/" + w_list.weaponList[i].objectName + ".asset");
                                            w_list.weaponList.RemoveAt(i);
                                            tab = 0;
                                        }

                                        if (GUILayout.Button("Delete"))
                                        {
                                            AssetDatabase.DeleteAsset("Assets/Meyer/TestScripts/InventoryWindowStuff/WeaponAssets/" + w_list.weaponList[i].objectName + ".asset");
                                            w_list.weaponList.RemoveAt(i);
                                        }

                                        GUILayout.EndHorizontal();
                                        GUILayout.EndArea();
                                        if (buttons.x >= position.width - 30)
                                        {
                                            buttons.x = 10;
                                            buttons.y += 110;

                                            rec.x = 10;
                                            rec.y += buttons.height + 10 + rec.width;
                                        }
                                        buttons.x += 80;
                                        rec.x += 80;
                                    }
                                }
                                break;
                            }
                        case 2:
                            {

                                break;
                            }
                        default:
                            break;
                    }
                    break;
                }
            case 1:
                tab = GUILayout.Toolbar(tab, strings);

                switch (tab)
                {
                    case 0:
                        {
                            GUILayout.Space(20);

                            GUILayout.Label("Object Name: ");
                            _name = GUILayout.TextField(_name, 17);

                            GUILayout.Space(20);

                            GUILayout.BeginHorizontal();
                            GUILayout.Label("Object Icon:");
                            icon = EditorGUILayout.ObjectField(icon, typeof(Texture2D), true) as Texture2D;
                            GUILayout.EndHorizontal();

                            GUILayout.Space(20);

                            GUILayout.BeginHorizontal();
                            GUILayout.Space(10);
                            GUILayout.Label("Weight: ", GUILayout.ExpandWidth(false));
                            weight = EditorGUILayout.IntField(weight, GUILayout.ExpandWidth(false));

                            GUILayout.Space(30);

                            GUILayout.Label("Durability: ", GUILayout.ExpandWidth(false));
                            durability = EditorGUILayout.IntField(durability, GUILayout.ExpandWidth(false));

                            GUILayout.Space(30);

                            GUILayout.Label("Value :", GUILayout.ExpandWidth(false));
                            value = EditorGUILayout.IntField(value, GUILayout.ExpandWidth(false));

                            GUILayout.EndHorizontal();

                            GUILayout.Space(20);
                            GUILayout.BeginHorizontal();

                            GUILayout.BeginHorizontal();
                            GUILayout.Label("Base Armor :", GUILayout.ExpandWidth(false));
                            baseArmor = EditorGUILayout.IntField(baseArmor, GUILayout.ExpandWidth(false));
                            GUILayout.EndHorizontal();

                            GUILayout.Label("Equipment type", GUILayout.ExpandWidth(false));
                            EditorGUILayout.Popup(slotchoice, equipmentSlot);

                            GUILayout.EndHorizontal();
                            GUILayout.Space(20);


                            GUILayout.Label("Weight Class", GUILayout.ExpandWidth(false));
                            EditorGUILayout.Popup(classchoice, weightClass);

                            GUILayout.Space(20);

                            GUILayout.BeginHorizontal();

                            GUILayout.Label("Is Indestructible", GUILayout.ExpandWidth(false));
                            isIndestructible = EditorGUILayout.Toggle(isIndestructible, GUILayout.ExpandWidth(false));

                            GUILayout.Label("Is stackable", GUILayout.ExpandWidth(false));
                            isStackable = EditorGUILayout.Toggle(isStackable, GUILayout.ExpandWidth(false));

                            GUILayout.Label("Destroy on Use", GUILayout.ExpandWidth(false));
                            destroyOnUse = EditorGUILayout.Toggle(destroyOnUse, GUILayout.ExpandWidth(false));

                            GUILayout.EndHorizontal();
                            GUILayout.Space(20);

                            if (GUILayout.Button("Add Armor"))
                            {
                                string[] results;
                                results = AssetDatabase.FindAssets(_name);
                                if (results.Length == 0)
                                {
                                    Armor newWeapon = Armor.Create(_name);
                                    newWeapon.objectName = _name;
                                    newWeapon.name = name;
                                    newWeapon.weight = weight;

                                    newWeapon.icon = icon as Texture2D;
                                    newWeapon.value = value;
                                    newWeapon.destroyOnUse = destroyOnUse;
                                    newWeapon.isStackable = isStackable;
                                    newWeapon.isIndestructible = isIndestructible;
                                    newWeapon.equipSlot = (Armor.EquipSlot)slotchoice;
                                    newWeapon.weightClass = (Armor.WeightClass)classchoice;
                                    newWeapon.durability = durability;
                                    if (newWeapon.icon != null)
                                        a_list.armorList.Add(newWeapon);
                                    else
                                        AssetDatabase.DeleteAsset("Assets/Meyer/TestScripts/InventoryWindowStuff/ArmorAssets/" + _name + ".asset");
                                    resetValues();
                                }
                            }
                            break;
                        }
                    case 1:
                        {
                            Rect rec = new Rect();
                            rec.x = 10;
                            rec.y = 50;
                            rec.height = 60;
                            rec.width = 60;

                            Rect buttons = new Rect();
                            buttons.x = 10;
                            buttons.y = 110;
                            buttons.height = 40;
                            buttons.width = 60;
                            for (int i = 0; i < a_list.armorList.Count; i++)
                            {
                                if (a_list.armorList[i] == null)
                                {
                                    a_list.armorList.RemoveAt(i);
                                }
                                else
                                {
                                    EditorGUI.DrawPreviewTexture(rec, a_list.armorList[i].icon);
                                    GUILayout.BeginArea(buttons);
                                    GUILayout.BeginVertical();

                                    if(GUILayout.Button("Edit"))
                                        {
                                        _name = a_list.armorList[i].objectName;
                                        name = a_list.armorList[i].objectName;
                                        weight = a_list.armorList[i].weight;
                                        icon = a_list.armorList[i].icon;
                                        value = a_list.armorList[i].value;
                                        destroyOnUse = a_list.armorList[i].destroyOnUse;
                                        isStackable = a_list.armorList[i].isStackable;
                                        isIndestructible = a_list.armorList[i].isIndestructible;
                                        slotchoice = (int)a_list.armorList[i].equipSlot;
                                        classchoice = (int)a_list.armorList[i].weightClass;
                                        durability = a_list.armorList[i].durability;
                                    }
                                    if (GUILayout.Button("Delete"))
                                    {
                                        AssetDatabase.DeleteAsset("Assets/Meyer/TestScripts/InventoryWindowStuff/ArmorAssets/" + a_list.armorList[i].objectName + ".asset");
                                        a_list.armorList.RemoveAt(i);
                                    }

                                    GUILayout.EndVertical();

                                    GUILayout.EndArea();

                                    buttons.x += 80;
                                    rec.x += 80;

                                    if (buttons.x >= position.width - 30)
                                    {
                                        buttons.x = 10;
                                        buttons.y += 110;

                                        rec.x = 10;
                                        rec.y += buttons.height + 10 + rec.width;
                                    }
                                }
                            }
                            break;
                        }
                    case 2:
                        {

                            break;
                        }
                    default:
                        break;
                }
                break;
        }


    }
}

