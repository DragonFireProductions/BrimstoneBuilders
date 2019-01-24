using System.Collections;
using System.Collections.Generic;
using System.Reflection;

using JetBrains.Annotations;

using TMPro;

using UnityEngine;
using UnityEngine.UI;

public class UIInventoryManager : MonoBehaviour {

    public GameObject PotionsInventory;

    public GameObject CharacterInventory;

    public Tab Potion;

    public Tab Armor;

    public Tab Weapon;

    //public UIItemsWithLabels characterStats;

    public GameObject WeaponInventory;

    public GameObject Tab;

    public GameObject tabParent;

    public GameObject SendToWindow;

    public GameObject SendToButton;

    public GameObject WeaponInventoryStats;

    public GameObject WeaponWindow;

    public GameObject inventories;

    public GameObject inventoryCharacterStats;

    public GameObject comparedInventoryWeapons;

    public GameObject CompanionSellStats;

    public Tab SubclassHub;

    public Image meleBar;

    public Image magicBar;

    public Image rangeBar;

    public GameObject pauseWindow;

    public GameObject PlayerImage;

    //add a new window
    public GameObject[] Grid;

    public GameObject[] ArmorGrid;

    public  QuestManager manager;

    public GameObject MessageWindow;

    public Text messageText;

    public Text NotificationText;

    public GameObject GameOverWindow;

    public GameObject notificationWindow;
    public void Start( ) {
        
        WeaponInventoryStats.GetComponent<UIItemsWithLabels>().FindLabels();
        inventoryCharacterStats.GetComponentInChildren < UIItemsWithLabels >( ).FindLabels( );
        comparedInventoryWeapons.GetComponent < UIItemsWithLabels >( ).FindLabels( );
        CompanionSellStats.transform.Find("CharacterStats").GetComponent<UIItemsWithLabels>().FindLabels();
        CompanionSellStats.transform.Find("WeaponStats").GetComponent<UIItemsWithLabels>().FindLabels();
        manager = gameObject.GetComponent < QuestManager >( );
    }

    public object this[string propertyName]
    {
        get { return this.GetType().GetField(propertyName).GetValue(this); }
        set { this.GetType().GetField(propertyName).SetValue(this, value); }
    }

    public void ShowNotification( string message , int time ) {
        StartCoroutine( notfication( message , time ) );
    }

    public IEnumerator notfication( string message , int time ) {
        notificationWindow.SetActive(true);
        NotificationText.text = message;
        yield return new WaitForSeconds(time);
        notificationWindow.SetActive(false);
        NotificationText.text = "";
    }
    public void Update() {
        if ( StaticManager.Character.attachedWeapon != null ){


            SubclassHub.SubClass.text = StaticManager.Character.attachedWeapon.type.ToString( );

            if ( StaticManager.Character.attachedWeapon is GunType ){
                int cur = ( int )StaticManager.Character.range.CurrentLevel;
                SubclassHub.bar.fillAmount = StaticManager.Character.range.CurrentLevel - cur;
                SubclassHub.Level.text     = cur.ToString( );
            }

            else if ( StaticManager.Character.attachedWeapon is SwordType ){
                int cur = ( int )StaticManager.Character.mele.CurrentLevel;
                SubclassHub.bar.fillAmount = StaticManager.Character.mele.CurrentLevel - cur;
                SubclassHub.Level.text     = cur.ToString( );
            }
        }
    }

    public void ShowMessage(string message, int time, bool questConfirmationl ) {
        StartCoroutine( show( message , time ) );

        if ( questConfirmationl ){
            StaticManager.questManager.questConfirmation.SetActive(true);
        }
        else{
            StaticManager.questManager.questConfirmation.SetActive(false);
        }
    }

    public IEnumerator show( string message , int time ) {
        StaticManager.UiInventory.ShowWindow(MessageWindow);
          messageText.text = message;
        yield return new WaitForSeconds(time);
        StaticManager.UiInventory.CloseWindow(MessageWindow);
      


    }
    
}
