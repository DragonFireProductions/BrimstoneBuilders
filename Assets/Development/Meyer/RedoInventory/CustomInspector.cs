using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CustomInspector : EditorWindow {
    
    public BaseItemList WeaponItemList;
    private int viewIndex = 1;
    private int weaponChoice = 0;
    private int equipChoice = 0;

    
    public enum WeaponType
    {
        Type1,
        Type2,
        Type3,
        Type4
    };

    [MenuItem ("Window/Inventory Editor %#e")]
    static void  Init () 
    {
        EditorWindow.GetWindow (typeof (CustomInspector));
    }
    
    void  OnEnable () {
        WeaponItemList = Resources.Load < BaseItemList >( "BaseItemList" );
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

                WeaponItemList.itemList[viewIndex - 1] = EditorGUILayout.ObjectField(WeaponItemList.itemList[viewIndex -1], typeof(BaseItems), true) as BaseItems;

                if ( WeaponItemList.itemList[ viewIndex - 1 ] ){


                    GUILayout.Label( "Object Name: " );
                    WeaponItemList.itemList[ viewIndex - 1 ].objectName = GUILayout.TextField( WeaponItemList.itemList[ viewIndex - 1 ].objectName , 17 );

                    WeaponItemList.itemList[ viewIndex - 1 ].stats.objectName = WeaponItemList.itemList[ viewIndex - 1 ].objectName;

                    GUILayout.Space( 40 );
                    GUILayout.BeginHorizontal( );

                    ///Sets icon
                    GUILayout.Label( "Object Icon:" );
                    WeaponItemList.itemList[ viewIndex - 1 ].stats.icon = EditorGUILayout.ObjectField( WeaponItemList.itemList[ viewIndex - 1 ].stats.icon , typeof( Texture2D ) , true ) as Texture2D;

                    GUILayout.EndHorizontal( );
                    GUILayout.Space( 40 );
                    GUILayout.BeginHorizontal( );
                    GUILayout.Space( 10 );

                    ///Sets weight
                    GUILayout.Label( "Weight: " , GUILayout.ExpandWidth( false ) );
                    WeaponItemList.itemList[ viewIndex - 1 ].stats.weight = EditorGUILayout.IntField( WeaponItemList.itemList[ viewIndex - 1 ].stats.weight , GUILayout.ExpandWidth( false ) );

                    GUILayout.Space( 30 );

                    ///Sets durability
                    GUILayout.Label( "Durability: " , GUILayout.ExpandWidth( false ) );
                    WeaponItemList.itemList[ viewIndex - 1 ].stats.durability = EditorGUILayout.IntField( WeaponItemList.itemList[ viewIndex - 1 ].stats.durability , GUILayout.ExpandWidth( false ) );

                    GUILayout.Space( 30 );

                    ///Sets value 
                    GUILayout.Label( "Value :" , GUILayout.ExpandWidth( false ) );
                    WeaponItemList.itemList[ viewIndex - 1 ].stats.value = EditorGUILayout.IntField( WeaponItemList.itemList[ viewIndex - 1 ].stats.value , GUILayout.ExpandWidth( false ) );

                    GUILayout.Space( 30 );

                    ///Sets base Damage
                    GUILayout.Label( "Base Damage :" , GUILayout.ExpandWidth( false ) );
                    WeaponItemList.itemList[ viewIndex - 1 ].stats.baseDamage = EditorGUILayout.IntField( WeaponItemList.itemList[ viewIndex - 1 ].stats.baseDamage , GUILayout.ExpandWidth( false ) );

                    GUILayout.EndHorizontal( );
                    GUILayout.Space( 40 );
                    GUILayout.BeginHorizontal( );
                    GUILayout.Space( 30 );

                    ///Sets speed
                    GUILayout.Label( "Speed: " , GUILayout.ExpandWidth( false ) );
                    WeaponItemList.itemList[ viewIndex - 1 ].stats.attackSpeed = EditorGUILayout.IntField( WeaponItemList.itemList[ viewIndex - 1 ].stats.attackSpeed , GUILayout.ExpandWidth( false ) );

                    GUILayout.Space( 30 );

                    ///Sets reach
                    GUILayout.Label( "Reach: " , GUILayout.ExpandWidth( false ) );
                    WeaponItemList.itemList[ viewIndex - 1 ].stats.reach = EditorGUILayout.IntField( WeaponItemList.itemList[ viewIndex - 1 ].stats.reach , GUILayout.ExpandWidth( false ) );

                    GUILayout.EndHorizontal( );
                    GUILayout.Space( 40 );

                    GUILayout.Space( 20 );
                    GUILayout.BeginHorizontal( );

                    ///Sets bool
                    GUILayout.Label( "Is Indestructible" , GUILayout.ExpandWidth( false ) );
                    WeaponItemList.itemList[ viewIndex - 1 ].stats.isIndestructible = EditorGUILayout.Toggle( WeaponItemList.itemList[ viewIndex - 1 ].stats.isIndestructible , GUILayout.ExpandWidth( false ) );

                    ///Sets bool
                    GUILayout.Label( "Is stackable" , GUILayout.ExpandWidth( false ) );
                    WeaponItemList.itemList[ viewIndex - 1 ].stats.isStackable = EditorGUILayout.Toggle( WeaponItemList.itemList[ viewIndex - 1 ].stats.isStackable , GUILayout.ExpandWidth( false ) );

                    ///Sets bool
                    GUILayout.Label( "Destroy on Use" , GUILayout.ExpandWidth( false ) );
                    WeaponItemList.itemList[ viewIndex - 1 ].stats.destroyOnUse = EditorGUILayout.Toggle( WeaponItemList.itemList[ viewIndex - 1 ].stats.destroyOnUse , GUILayout.ExpandWidth( false ) );
                    GUILayout.EndHorizontal( );
                    GUILayout.Space( 20 );

                    GUILayout.Label( "Audio Clip:" );
                    WeaponItemList.itemList[ viewIndex - 1 ].stats.clip = EditorGUILayout.ObjectField( WeaponItemList.itemList[ viewIndex - 1 ].stats.clip , typeof( AudioClip ) , true ) as AudioClip;


                    EditorUtility.SetDirty( WeaponItemList.itemList[ viewIndex - 1 ] );
                    UnityEditor.PrefabUtility.prefabInstanceUpdated( WeaponItemList.itemList[ viewIndex - 1 ].gameObject );
                    GUILayout.Space( 40 );
                    GUILayout.BeginHorizontal( );

                    WeaponItemList.itemList[ viewIndex - 1 ].IncreaseAmount = EditorGUILayout.FloatField( "Stat increase amount:" , WeaponItemList.itemList[ viewIndex - 1 ].IncreaseAmount );

                    WeaponItemList.itemList[ viewIndex - 1 ].Level = EditorGUILayout.FloatField( "Level: " , WeaponItemList.itemList[ viewIndex - 1 ].Level );

                    if ( WeaponItemList.itemList[ viewIndex - 1 ] is WeaponObject ){
                        var ob1 = WeaponItemList.itemList[ viewIndex - 1 ] as WeaponObject;

                        if ( WeaponItemList.itemList[ viewIndex - 1 ] is GunType ){
                            var ob = WeaponItemList.itemList[ viewIndex - 1 ] as GunType;

                            GUILayout.EndHorizontal();

                            GUILayout.BeginHorizontal();
                            ob.Ammo     = EditorGUILayout.IntField( "Ammo: " ,     ob.Ammo );
                            ob.Capacity = EditorGUILayout.IntField( "Capacity: " , ob.Capacity );
                            ob.FireRate = EditorGUILayout.FloatField( "Fire Rate" , ob.FireRate );
                            GUILayout.EndHorizontal();
                            GUILayout.BeginHorizontal();
                            ob.Range      = EditorGUILayout.FloatField( "Range" ,       ob.Range );
                            ob.ReloadTime = EditorGUILayout.FloatField( "Reload time" , ob.ReloadTime );

                            ob.projectile = EditorGUILayout.ObjectField( ob.projectile , typeof( Projectile ) , true ) as Projectile;
                            GUILayout.EndHorizontal();
                            GUILayout.BeginHorizontal();
                            ob.projectile.hitEffect = (ParticleManager.states)EditorGUILayout.EnumPopup("Particle", ob.projectile.hitEffect) ;
                            ob.projectile.y_pos = EditorGUILayout.IntField("Y Position", ob.projectile.y_pos);
                            ob.projectile.TimeToPlay = EditorGUILayout.IntField("Time To Play", ob.projectile.TimeToPlay);

                            GUILayout.EndHorizontal();
                            GUILayout.BeginHorizontal();
                            if ( ob.projectile ){
                                ob.projectile.doesDOT = GUILayout.Toggle( ob.projectile.doesDOT , "Does damage over time" );

                                if ( ob.projectile.doesDOT ){
                                    ob.projectile.hits = EditorGUILayout.IntField( "Hit amount: " , ob.projectile.hits );

                                    ob.projectile.interval = EditorGUILayout.FloatField( "Interval" , ob.projectile.interval );
                                }
                            }
                            GUILayout.EndHorizontal();

                        }
                        else if ( WeaponItemList.itemList[ viewIndex - 1 ] is SwordType ){
                            var ob = WeaponItemList.itemList[ viewIndex - 1 ] as SwordType;

                        }
                    }

                    if ( WeaponItemList.itemList[ viewIndex - 1 ] is Potions ){
                        var ob1 = WeaponItemList.itemList[ viewIndex - 1 ] as Potions;

                        GUILayout.EndHorizontal();
                        GUILayout.BeginHorizontal();
                        GUILayout.Label( "Cast effect" );
                        ob1.cast_effect = EditorGUILayout.ObjectField( ob1.cast_effect , typeof( ParticleSystem ) , true ) as ParticleSystem;
                        GUILayout.Label( "Hit effect" );
                        ob1.hit_effect = EditorGUILayout.ObjectField( ob1.hit_effect , typeof( ParticleSystem ) , true ) as ParticleSystem;


                        if ( WeaponItemList.itemList[ viewIndex - 1 ] is HealPotion ){
                            var ob = WeaponItemList.itemList[ viewIndex - 1 ] as HealPotion;
                            ob.HealAmount = EditorGUILayout.IntField( "Heal amount" , ob.HealAmount );
                        }
                        GUILayout.EndHorizontal();
                    }
                }

            }
            else 
            {
                GUILayout.Label ("This Inventory List is Empty.");
            }
        }
    }
    
    void OpenItemList () 
    {
        string absPath = EditorUtility.OpenFilePanel ("Select Inventory Item List", "", "");
        if (absPath.StartsWith(Application.dataPath)) 
        {
            string relPath = absPath.Substring(Application.dataPath.Length - "Assets".Length);
            WeaponItemList = AssetDatabase.LoadAssetAtPath (relPath, typeof(BaseItemList)) as BaseItemList;
            if (WeaponItemList != null && WeaponItemList.itemList == null)
                WeaponItemList.itemList = new List<BaseItems>();
            if (WeaponItemList) {
                EditorPrefs.SetString("ObjectPath", relPath);
            }
        }
    }

    void AddItem () 
    {

        BaseItems newItem = new BaseItems {objectName = "new item"};
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
        WeaponItemList = CreateInventoryList.Create();
        if (!WeaponItemList) return;
        WeaponItemList.itemList = new List<BaseItems>();
        string relPath = AssetDatabase.GetAssetPath(WeaponItemList);
        EditorPrefs.SetString("ObjectPath", relPath);
    }

}