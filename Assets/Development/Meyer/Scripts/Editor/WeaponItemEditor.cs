using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class WeaponItemEditor : EditorWindow {
    
    public WeaponItemList WeaponItemList;
    private int viewIndex = 1;

    private string[] weaponsType =
    {
        WeaponItem.WeaponType.Sword.ToString(), WeaponItem.WeaponType.Gun.ToString(), WeaponItem.WeaponType.Potion.ToString(),
        WeaponItem.WeaponType.Type4.ToString()
    };

    private string[] equipType =
    {
        WeaponItem.EquipType.H2.ToString(), WeaponItem.EquipType.H2.ToString(),
        WeaponItem.EquipType.Shield.ToString(), WeaponItem.EquipType.Shoulder.ToString()
    };

    private int weaponChoice = 0;
    private int equipChoice = 0;

    
    public enum WeaponType
    {
        Type1,
        Type2,
        Type3,
        Type4
    };

    [MenuItem ("Window/Inventory Item Editor %#e")]
    static void  Init () 
    {
        EditorWindow.GetWindow (typeof (WeaponItemEditor));
    }
    
    void  OnEnable () {
        if(EditorPrefs.HasKey("ObjectPath")) 
        {
            string objectPath = EditorPrefs.GetString("ObjectPath");
            WeaponItemList = AssetDatabase.LoadAssetAtPath (objectPath, typeof(WeaponItemList)) as WeaponItemList;
        }
        
    }
    
    void  OnGUI () {
        GUILayout.BeginHorizontal ();
        GUILayout.Label ("Inventory Item Editor", EditorStyles.boldLabel);
        if (WeaponItemList != null) {
            if (GUILayout.Button("Show Item List")) 
            {
                EditorUtility.FocusProjectWindow();
                Selection.activeObject = WeaponItemList;
            }
        }
        if (GUILayout.Button("Open Item List")) 
        {
                OpenItemList();
        }
        if (GUILayout.Button("New Item List")) 
        {
            EditorUtility.FocusProjectWindow();
            Selection.activeObject = WeaponItemList;
        }
        GUILayout.EndHorizontal ();
        
        if (WeaponItemList == null) 
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
            
        if (WeaponItemList != null) 
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
                if (viewIndex < WeaponItemList.itemList.Count) 
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
            if (WeaponItemList.itemList == null)
                Debug.Log("List is null");
            if (WeaponItemList.itemList != null && WeaponItemList.itemList.Count > 0) 
            {
                GUILayout.BeginHorizontal ();
                viewIndex = Mathf.Clamp (EditorGUILayout.IntField ("Current Item", viewIndex, GUILayout.ExpandWidth(false)), 1, WeaponItemList.itemList.Count);
                //Mathf.Clamp (viewIndex, 1, inventoryItemList.itemList.Count);
                EditorGUILayout.LabelField ("of   " +  WeaponItemList.itemList.Count.ToString() + "  items", "", GUILayout.ExpandWidth(false));
                GUILayout.EndHorizontal ();

                GUILayout.Label("Object Name: ");
                WeaponItemList.itemList[viewIndex - 1].objectName = GUILayout.TextField(WeaponItemList.itemList[viewIndex - 1].objectName, 17);

                GUILayout.Space(40);
                GUILayout.BeginHorizontal();

                ///Sets icon
                GUILayout.Label("Object Icon:");
                WeaponItemList.itemList[viewIndex - 1].icon = EditorGUILayout.ObjectField(WeaponItemList.itemList[viewIndex - 1].icon, typeof(Texture2D), true) as Texture2D;

                GUILayout.EndHorizontal();
                GUILayout.Space(40);
                GUILayout.BeginHorizontal();
                GUILayout.Space(10);

                ///Sets weight
                GUILayout.Label("Weight: ", GUILayout.ExpandWidth(false));
                WeaponItemList.itemList[viewIndex - 1].weight = EditorGUILayout.IntField(WeaponItemList.itemList[viewIndex - 1].weight, GUILayout.ExpandWidth(false));

                GUILayout.Space(30);

                ///Sets durability
                GUILayout.Label("Durability: ", GUILayout.ExpandWidth(false));
                WeaponItemList.itemList[viewIndex - 1].durability = EditorGUILayout.IntField(WeaponItemList.itemList[viewIndex - 1].durability, GUILayout.ExpandWidth(false));

                GUILayout.Space(30);

                ///Sets value 
                GUILayout.Label("Value :", GUILayout.ExpandWidth(false));
                WeaponItemList.itemList[viewIndex - 1].value = EditorGUILayout.IntField(WeaponItemList.itemList[viewIndex - 1].value, GUILayout.ExpandWidth(false));

                GUILayout.Space(30);

                ///Sets base Damage
                GUILayout.Label("Base Damage :", GUILayout.ExpandWidth(false));
                WeaponItemList.itemList[viewIndex - 1].baseDamage = EditorGUILayout.IntField(WeaponItemList.itemList[viewIndex - 1].baseDamage, GUILayout.ExpandWidth(false));

                GUILayout.EndHorizontal();
                GUILayout.Space(40);
                GUILayout.BeginHorizontal();
                GUILayout.Space(30);

                ///Sets speed
                GUILayout.Label("Speed: ", GUILayout.ExpandWidth(false));
                WeaponItemList.itemList[viewIndex - 1].attackSpeed = EditorGUILayout.IntField(WeaponItemList.itemList[viewIndex - 1].attackSpeed, GUILayout.ExpandWidth(false));

                GUILayout.Space(30);

                ///Sets reach
                GUILayout.Label("Reach: ", GUILayout.ExpandWidth(false));
                WeaponItemList.itemList[viewIndex - 1].reach = EditorGUILayout.IntField(WeaponItemList.itemList[viewIndex - 1].reach, GUILayout.ExpandWidth(false));

                ///Sets Equipment type
                GUILayout.Label("Equipment type", GUILayout.ExpandWidth(false));
                WeaponItemList.itemList[viewIndex - 1].equipType = (WeaponItem.EquipType)EditorGUILayout.EnumPopup("Weapon Type:", WeaponItemList.itemList[viewIndex - 1].equipType);

                GUILayout.EndHorizontal();
                GUILayout.Space(40);

                GUILayout.Space(20);
                GUILayout.BeginHorizontal();

                ///Sets bool
                GUILayout.Label("Is Indestructible", GUILayout.ExpandWidth(false));
                WeaponItemList.itemList[viewIndex - 1].isIndestructible = EditorGUILayout.Toggle(WeaponItemList.itemList[viewIndex - 1].isIndestructible, GUILayout.ExpandWidth(false));

                ///Sets bool
                GUILayout.Label("Is stackable", GUILayout.ExpandWidth(false));
                WeaponItemList.itemList[viewIndex - 1].isStackable = EditorGUILayout.Toggle(WeaponItemList.itemList[viewIndex - 1].isStackable, GUILayout.ExpandWidth(false));

                ///Sets bool
                GUILayout.Label("Destroy on Use", GUILayout.ExpandWidth(false));
                WeaponItemList.itemList[viewIndex - 1].destroyOnUse = EditorGUILayout.Toggle(WeaponItemList.itemList[viewIndex - 1].destroyOnUse, GUILayout.ExpandWidth(false));
                GUILayout.EndHorizontal();
                GUILayout.Space(20);

                GUILayout.Label("Audio Clip:");
                WeaponItemList.itemList[viewIndex - 1].clip = EditorGUILayout.ObjectField(WeaponItemList.itemList[viewIndex - 1].clip, typeof(AudioClip), true) as AudioClip;

                ///Sets item Type
                GUILayout.Label("Weapon type", GUILayout.ExpandWidth(false));
                WeaponItemList.itemList[viewIndex - 1].weaponType =(WeaponItem.WeaponType) EditorGUILayout.EnumPopup("Weapon Type", WeaponItemList.itemList[viewIndex - 1].weaponType);


                if ( WeaponItemList.itemList[ viewIndex - 1 ].weaponType == WeaponItem.WeaponType.Gun ){


                    ///Sets item object
                    WeaponItemList.itemList[ viewIndex - 1 ].item = EditorGUILayout.ObjectField( WeaponItemList.itemList[ viewIndex - 1 ].item , typeof( WeaponObject ) , true ) as WeaponObject;

                    if ( WeaponItemList.itemList[ viewIndex - 1 ].item ){


                        var weapon = ( GunType )WeaponItemList.itemList[ viewIndex - 1 ].item;
                        weapon.Ammo = EditorGUILayout.IntField( "Ammo amount: " , weapon.Ammo );

                        weapon.Capacity = EditorGUILayout.IntField( "Ammo capacity:" , weapon.Capacity );

                        weapon.FireRate = EditorGUILayout.FloatField( "Fire rate" , weapon.FireRate );

                        weapon.Range = EditorGUILayout.FloatField( "Range:" , weapon.Range );

                        weapon.ReloadTime = EditorGUILayout.FloatField( "Reload time" , weapon.ReloadTime );

                        ///Projectile gameObject
                        GUILayout.Label( "Gun Projectile" , GUILayout.ExpandWidth( false ) );
                        WeaponItemList.itemList[ viewIndex - 1 ].Projectile = EditorGUILayout.ObjectField( WeaponItemList.itemList[ viewIndex - 1 ].Projectile , typeof( GameObject ) , true ) as GameObject;

                        if ( WeaponItemList.itemList[ viewIndex - 1 ].Projectile != null ){
                            var projectile = WeaponItemList.itemList[ viewIndex - 1 ].Projectile.GetComponent < Projectile >( );

                            if ( projectile is IceProjectile ){
                                var projectile1 = ( IceProjectile )projectile;

                                projectile1.hits = EditorGUILayout.IntField( "Hits: " , projectile1.hits );

                                projectile1.interval = EditorGUILayout.FloatField( "Interval" , projectile1.interval );
                            }
                            else if ( projectile is FireProjectile ){
                                var projectile1 = ( FireProjectile )projectile;

                                projectile1.hits = EditorGUILayout.IntField( "Hits: " , projectile1.hits );

                                projectile1.interval = EditorGUILayout.FloatField( "Interval" , projectile1.interval );
                            }
                            else if ( projectile is ShockProjectile ){
                                var projectile1 = ( ShockProjectile )projectile;

                                projectile1.hits = EditorGUILayout.IntField( "Hits: " , projectile1.hits );

                                projectile1.interval = EditorGUILayout.FloatField( "Interval" , projectile1.interval );
                            }
                            else if ( projectile is PoisonProjectile ){
                                var projectile1 = ( PoisonProjectile )projectile;

                                projectile1.hits = EditorGUILayout.IntField( "Hits: " , projectile1.hits );

                                projectile1.interval = EditorGUILayout.FloatField( "Interval" , projectile1.interval );
                            }
                        }

                    }
                }

                /// if potion
                else if ( WeaponItemList.itemList[ viewIndex - 1 ].weaponType == WeaponItem.WeaponType.Potion ){
                    WeaponItemList.itemList[viewIndex - 1].item = EditorGUILayout.ObjectField(WeaponItemList.itemList[viewIndex - 1].item, typeof(Potions), true) as Potions;

                    if ( WeaponItemList.itemList[ viewIndex - 1 ].item ){

                        var item = WeaponItemList.itemList[ viewIndex - 1 ].item as Potions;

                        ///Sets potion object
                        WeaponItemList.itemList[ viewIndex - 1 ].item = EditorGUILayout.ObjectField( WeaponItemList.itemList[ viewIndex - 1 ].item , typeof( Potions ) , true ) as Potions;

                        ///cast effect
                        EditorGUILayout.LabelField( "Cast effect" );
                        item.cast_effect = EditorGUILayout.ObjectField( item.cast_effect , typeof( ParticleSystem ) , true ) as ParticleSystem;

                        ///hit effect
                        EditorGUILayout.LabelField( "Hit effect" );
                        item.hit_effect = EditorGUILayout.ObjectField( item.hit_effect , typeof( ParticleSystem ) , true ) as ParticleSystem;

                    }
                }
            }
            else 
            {
                GUILayout.Label ("This Inventory List is Empty.");
            }
        }
        if (GUI.changed) 
        {
            EditorUtility.SetDirty(WeaponItemList);
        }
    }
    
    void OpenItemList () 
    {
        string absPath = EditorUtility.OpenFilePanel ("Select Inventory Item List", "", "");
        if (absPath.StartsWith(Application.dataPath)) 
        {
            string relPath = absPath.Substring(Application.dataPath.Length - "Assets".Length);
            WeaponItemList = AssetDatabase.LoadAssetAtPath (relPath, typeof(WeaponItemList)) as WeaponItemList;
            if (WeaponItemList != null && WeaponItemList.itemList == null)
                WeaponItemList.itemList = new List<WeaponItem>();
            if (WeaponItemList) {
                EditorPrefs.SetString("ObjectPath", relPath);
            }
        }
    }

    void AddItem () 
    {

        WeaponItem newItem = new WeaponItem {objectName = "new item"};
        WeaponItemList.itemList.Add (newItem);
        viewIndex = WeaponItemList.itemList.Count;
    }
    
    void DeleteItem (int index) 
    {
        WeaponItemList.itemList.RemoveAt (index);
    }
    void CreateNewItemList()
    {
        viewIndex = 1;
        WeaponItemList = CreateWeaponItemList.Create();
        if (!WeaponItemList) return;
        WeaponItemList.itemList = new List<WeaponItem>();
        string relPath = AssetDatabase.GetAssetPath(WeaponItemList);
        EditorPrefs.SetString("ObjectPath", relPath);
    }

}