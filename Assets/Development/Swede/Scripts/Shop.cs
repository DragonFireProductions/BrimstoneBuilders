using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Shop : MonoBehaviour
{
    [SerializeField] public int companionPrice;
    [SerializeField] public int resaleWorth;

    public GameObject Buy;

    public GameObject Sell;

    public GameObject ShopContainer;

    [Serializable]
    public struct CompanionStruct
    {

        public Companion companion;

        public int Melee;

        public int Range;

        public int Magic;

        public GameObject weapon;


    }
    [SerializeField]
    public List <CompanionStruct> companions;

    public GameObject sellContainer;
    // Use this for initialization
    void Start()
    {
       StaticManager.UiInventory.ItemsInstance.GetLabel("CompanionSellError", StaticManager.UiInventory.ItemsInstance.ShopUI).text = " ";
        Buy = ShopContainer.transform.Find( "Buy" ).gameObject;
        Sell = ShopContainer.transform.Find( "Sell" ).gameObject;
        StartCoroutine( init( ) );
    }

    IEnumerator init( ) {

        foreach (var l_companion in companions)
        {
            l_companion.companion.inventoryUI.Init(l_companion.companion);
            l_companion.companion.inventoryUI.AddToShop(this);
            shopCompanions.Add(l_companion.companion.inventoryUI.CompanionSell);
            l_companion.companion.inventoryUI.CompanionSell.transform.SetParent(Buy.transform);
            l_companion.companion.inventoryUI.CompanionSell.transform.localScale = new Vector3(1, 1, 1);
            l_companion.companion.inventoryUI.UpdateCharacter(l_companion.companion.inventoryUI.ShopCharacterText);
            l_companion.companion.startWeapon = l_companion.weapon;
            l_companion.companion.inventoryUI.UpdateItem(l_companion.companion.inventoryUI.ShopWeaponsText, l_companion.weapon.GetComponent<WeaponObject>());
            l_companion.companion.inventoryUI.tab.gameObject.SetActive(false);
        }
            yield return new WaitForEndOfFrame();
    }
    public List <Tab> shopCompanions = new List < Tab >();
    
   
    // Update is called once per frame
    void Update()
    {
        
    }
}
