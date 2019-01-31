using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    [SerializeField] public int companionPrice;
    [SerializeField] public int resaleWorth;
    

    public RawImage icon;

    public RawImage notIcon;

    public ShopContainer shopContainer;
    [Serializable]
    public struct CompanionStruct
    {

        public Companion companion;

        public int Melee;

        public int Range;

        public int Magic;

        public GameObject weapon;

        public int cost;

    }
    [SerializeField]
    public List <CompanionStruct> companions;

    public int ShopCompanions = 0;
    // Use this for initialization
    void Start()
    {
        StaticManager.currencyManager.shops.Add(gameObject );
        var _container = Instantiate( StaticManager.currencyManager.containerHolder.gameObject );
        shopContainer = _container.GetComponent < ShopContainer >( );

        StaticManager.UiInventory.ItemsInstance.GetLabel("CompanionSellError", StaticManager.UiInventory.ItemsInstance.ShopUI).text = " ";
        StartCoroutine( init( ) );
        StaticManager.map.Add(Map.Type.shop, icon);
        notIcon.enabled = true;
    }

    IEnumerator init( ) {
        yield return new WaitForSeconds(1);
        var shop = Instantiate(StaticManager.currencyManager.containerHolder);
        var container = shop.GetComponent<ShopContainer>();
        container.transform.SetParent(StaticManager.currencyManager.ShopUI.transform);
        container.gameObject.SetActive(false);
        container.shop = this;
        shopContainer = container;
        container.transform.position = StaticManager.currencyManager.containerHolder.transform.position;
        
        foreach (var l_companion in companions){
            ShopCompanions++;
            var companion = Instantiate( l_companion.companion );

            shopCompanions.Add(l_companion.companion.inventoryUI.CompanionSell);

            var c = Instantiate( StaticManager.currencyManager.container.gameObject );
            c.gameObject.SetActive(true);

            companion.inventoryUI.CompanionSell = c.GetComponent < CompanionContainer >( );
            companion.inventoryUI.CompanionSell.characterstats.FindLabels();
            companion.inventoryUI.CompanionSell.weaponstats.FindLabels();
            companion.inventoryUI.CompanionSell.companion = companion;

            c.GetComponent < CompanionContainer >( ).shop = this;

            companion.inventoryUI.CompanionSell.transform.SetParent(container.buy.transform);

            companion.inventoryUI.CompanionSell.transform.localScale = new Vector3(1, 1, 1);
            companion.mele.CurrentLevel = l_companion.Melee;
            companion.magic.CurrentLevel = l_companion.Magic;
            companion.range.CurrentLevel = l_companion.Range;
            companion.cost = l_companion.cost;
            companion.inventoryUI.UpdateCharacter(companion.inventoryUI.CompanionSell.characterstats);
            companion.startWeapon = l_companion.weapon;
            companion.inventoryUI.UpdateWeapon(companion.inventoryUI.CompanionSell.weaponstats, companion.startWeapon.GetComponent<WeaponObject>());


            
            companion.inventoryUI.Init(companion);
            companion.startWeapon = Instantiate(companion.startWeapon);
            companion.startWeapon.GetComponent<WeaponObject>().PickUp(companion);
            companion.startWeapon.GetComponent<WeaponObject>().Attach();

            companion.inventoryUI.sendToButton.gameObject.SetActive(false);
            companion.inventoryUI.tab.gameObject.SetActive(false);

            companion.gameObject.SetActive(false);
            StaticManager.RealTime.Companions.Remove( companion );
        }

        shop.gameObject.transform.position = StaticManager.currencyManager.containerHolder.transform.position;
        

        yield return new WaitForEndOfFrame();
    }
    public List <CompanionContainer> shopCompanions = new List < CompanionContainer >();
    
   
    // Update is called once per frame
    void Update() {
        shopCompanions.RemoveAll( items => items == null );
        if ( ShopCompanions <= 0 ){
            notIcon.enabled = false;
        }
        else{
            notIcon.enabled = true;
        }
    }
}
