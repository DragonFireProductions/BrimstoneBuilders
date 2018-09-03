using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

public class InventoryItemEditor : EditorWindow {
    
    public InventoryItemList inventoryItemList;
    private int viewIndex = 1;
    
    [MenuItem ("Window/Inventory Item Editor %#e")]
    static void  Init () 
    {
        EditorWindow.GetWindow (typeof (InventoryItemEditor));
    }
    
    void  OnEnable () {
        if(EditorPrefs.HasKey("ObjectPath")) 
        {
            string objectPath = EditorPrefs.GetString("ObjectPath");
            inventoryItemList = AssetDatabase.LoadAssetAtPath (objectPath, typeof(InventoryItemList)) as InventoryItemList;
        }
        
    }
    
    void  OnGUI () {
        GUILayout.BeginHorizontal ();
        GUILayout.Label ("Inventory Item Editor", EditorStyles.boldLabel);
        if (inventoryItemList != null) {
            if (GUILayout.Button("Show Item List")) 
            {
                EditorUtility.FocusProjectWindow();
                Selection.activeObject = inventoryItemList;
            }
        }
        if (GUILayout.Button("Open Item List")) 
        {
                OpenItemList();
        }
        if (GUILayout.Button("New Item List")) 
        {
            EditorUtility.FocusProjectWindow();
            Selection.activeObject = inventoryItemList;
        }
        GUILayout.EndHorizontal ();
        
        if (inventoryItemList == null) 
        {
            GUILayout.BeginHorizontal ();
            GUILayout.Space(10);
            if (GUILayout.Button("Create New Item List", GUILayout.ExpandWidth(false)))
            {
                CreateNewItemList();
            }
            if (GUILayout.Button("Open Existing Item List", GUILayout.ExpandWidth(false))) 
            {
                OpenItemList();
            }
            GUILayout.EndHorizontal ();
        }
            
            GUILayout.Space(20);
            
        if (inventoryItemList != null) 
        {
            GUILayout.BeginHorizontal ();
            
            GUILayout.Space(10);
            
            if (GUILayout.Button("Prev", GUILayout.ExpandWidth(false))) 
            {
                if (viewIndex > 1)
                    viewIndex --;
            }
            GUILayout.Space(5);
            if (GUILayout.Button("Next", GUILayout.ExpandWidth(false))) 
            {
                if (viewIndex < inventoryItemList.itemList.Count) 
                {
                    viewIndex ++;
                }
            }
            
            GUILayout.Space(60);
            
            if (GUILayout.Button("Add Item", GUILayout.ExpandWidth(false))) 
            {
                AddItem();
            }
            if (GUILayout.Button("Delete Item", GUILayout.ExpandWidth(false))) 
            {
                DeleteItem(viewIndex - 1);
            }
            
            GUILayout.EndHorizontal ();
            if (inventoryItemList.itemList == null)
                Debug.Log("wtf");
            if (inventoryItemList.itemList.Count > 0) 
            {
                GUILayout.BeginHorizontal ();
                viewIndex = Mathf.Clamp (EditorGUILayout.IntField ("Current Item", viewIndex, GUILayout.ExpandWidth(false)), 1, inventoryItemList.itemList.Count);
                //Mathf.Clamp (viewIndex, 1, inventoryItemList.itemList.Count);
                EditorGUILayout.LabelField ("of   " +  inventoryItemList.itemList.Count.ToString() + "  items", "", GUILayout.ExpandWidth(false));
                GUILayout.EndHorizontal ();

                GUILayout.Label("Object Name: ");
                inventoryItemList.itemList[viewIndex - 1].objectName = GUILayout.TextField(inventoryItemList.itemList[viewIndex - 1].objectName, 17);

                GUILayout.Space(40);
                GUILayout.BeginHorizontal();

                ///Sets icon
                GUILayout.Label("Object Icon:");
                inventoryItemList.itemList[viewIndex - 1].icon = EditorGUILayout.ObjectField(inventoryItemList.itemList[viewIndex - 1].icon, typeof(Texture2D), true) as Texture2D;

                GUILayout.EndHorizontal();
                GUILayout.Space(40);
                GUILayout.BeginHorizontal();
                GUILayout.Space(10);

                ///Sets weight
                GUILayout.Label("Weight: ", GUILayout.ExpandWidth(false));
                inventoryItemList.itemList[viewIndex - 1].weight = EditorGUILayout.IntField(inventoryItemList.itemList[viewIndex - 1].weight, GUILayout.ExpandWidth(false));

                GUILayout.Space(30);

                ///Sets durability
                GUILayout.Label("Durability: ", GUILayout.ExpandWidth(false));
                inventoryItemList.itemList[viewIndex - 1].durability = EditorGUILayout.IntField(inventoryItemList.itemList[viewIndex - 1].durability, GUILayout.ExpandWidth(false));

                GUILayout.Space(30);

                ///Sets value 
                GUILayout.Label("Value :", GUILayout.ExpandWidth(false));
                inventoryItemList.itemList[viewIndex - 1].value = EditorGUILayout.IntField(inventoryItemList.itemList[viewIndex - 1].value, GUILayout.ExpandWidth(false));

                GUILayout.Space(30);

                ///Sets base Damage
                GUILayout.Label("Base Damage :", GUILayout.ExpandWidth(false));
                inventoryItemList.itemList[viewIndex - 1].baseDamage = EditorGUILayout.IntField(inventoryItemList.itemList[viewIndex - 1].baseDamage, GUILayout.ExpandWidth(false));

                GUILayout.EndHorizontal();
                GUILayout.Space(40);
                GUILayout.BeginHorizontal();
                GUILayout.Space(30);

                ///Sets speed
                GUILayout.Label("Speed: ", GUILayout.ExpandWidth(false));
                inventoryItemList.itemList[viewIndex - 1].attackSpeed = EditorGUILayout.IntField(inventoryItemList.itemList[viewIndex - 1].attackSpeed, GUILayout.ExpandWidth(false));

                GUILayout.Space(30);

                ///Sets reach
                GUILayout.Label("Reach: ", GUILayout.ExpandWidth(false));
                inventoryItemList.itemList[viewIndex - 1].reach = EditorGUILayout.IntField(inventoryItemList.itemList[viewIndex - 1].reach, GUILayout.ExpandWidth(false));

                ///Sets Equipment type
                //GUILayout.Label("Equipment type", GUILayout.ExpandWidth(false));
                //EditorGUILayout.Popup(option, options);

                //GUILayout.EndHorizontal();
                //GUILayout.Space(40);

                /////Sets weapon Type
                //GUILayout.Label("Weapon type", GUILayout.ExpandWidth(false));
                //EditorGUILayout.Popup(weaponchoice, weapontype);

                GUILayout.Space(20);
                GUILayout.BeginHorizontal();

                ///Sets bool
                GUILayout.Label("Is Indestructible", GUILayout.ExpandWidth(false));
                inventoryItemList.itemList[viewIndex - 1].isIndestructible = EditorGUILayout.Toggle(inventoryItemList.itemList[viewIndex - 1].isIndestructible, GUILayout.ExpandWidth(false));

                ///Sets bool
                GUILayout.Label("Is stackable", GUILayout.ExpandWidth(false));
                inventoryItemList.itemList[viewIndex - 1].isStackable = EditorGUILayout.Toggle(inventoryItemList.itemList[viewIndex - 1].isStackable, GUILayout.ExpandWidth(false));

                ///Sets bool
                GUILayout.Label("Destroy on Use", GUILayout.ExpandWidth(false));
                inventoryItemList.itemList[viewIndex - 1].destroyOnUse = EditorGUILayout.Toggle(inventoryItemList.itemList[viewIndex - 1].destroyOnUse, GUILayout.ExpandWidth(false));
                GUILayout.EndHorizontal();
                GUILayout.Space(20);

            } 
            else 
            {
                GUILayout.Label ("This Inventory List is Empty.");
            }
        }
        if (GUI.changed) 
        {
            EditorUtility.SetDirty(inventoryItemList);
        }
    }
    
    void OpenItemList () 
    {
        string absPath = EditorUtility.OpenFilePanel ("Select Inventory Item List", "", "");
        if (absPath.StartsWith(Application.dataPath)) 
        {
            string relPath = absPath.Substring(Application.dataPath.Length - "Assets".Length);
            inventoryItemList = AssetDatabase.LoadAssetAtPath (relPath, typeof(InventoryItemList)) as InventoryItemList;
            if (inventoryItemList.itemList == null)
                inventoryItemList.itemList = new List<InventoryItem>();
            if (inventoryItemList) {
                EditorPrefs.SetString("ObjectPath", relPath);
            }
        }
    }

    void AddItem () 
    {
       
        InventoryItem newItem = new InventoryItem();
        newItem.objectName = "new item";
        inventoryItemList.itemList.Add (newItem);
        viewIndex = inventoryItemList.itemList.Count;
    }
    
    void DeleteItem (int index) 
    {
        inventoryItemList.itemList.RemoveAt (index);
    }
    void CreateNewItemList()
    {
        // There is no overwrite protection here!
        // There is No "Are you sure you want to overwrite your existing object?" if it exists.
        // This should probably get a string from the user to create a new name and pass it ...
        viewIndex = 1;
        inventoryItemList = CreateInventoryItemList.Create();
        if (inventoryItemList)
        {
            inventoryItemList.itemList = new List<InventoryItem>();
            string relPath = AssetDatabase.GetAssetPath(inventoryItemList);
            EditorPrefs.SetString("ObjectPath", relPath);
        }
    }

}