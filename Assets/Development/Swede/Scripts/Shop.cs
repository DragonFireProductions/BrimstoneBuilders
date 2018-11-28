using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Shop : MonoBehaviour
{
    [SerializeField] public int companionPrice;
    [SerializeField] public int resaleWorth;
    public GameObject CompanionShopStats;
    public List <Companion> companions;

    public List < GameObject > ShopCompanions;
    // Use this for initialization
    void Start()
    {
        ShopCompanions = new List < GameObject >();
        StaticManager.UiInventory.ItemsInstance.GetLabel("CompanionBuyPrice", StaticManager.UiInventory.ItemsInstance.ShopUI).text = companionPrice.ToString();
        StaticManager.UiInventory.ItemsInstance.GetLabel("CompanionSellPrice", StaticManager.UiInventory.ItemsInstance.ShopUI).text = resaleWorth.ToString();
        StaticManager.UiInventory.ItemsInstance.GetLabel("CompanionBuyError", StaticManager.UiInventory.ItemsInstance.ShopUI).text = " ";
        StaticManager.UiInventory.ItemsInstance.GetLabel("CompanionSellError", StaticManager.UiInventory.ItemsInstance.ShopUI).text = " ";


    }

    public struct ShopCompanion {

        public Companion companion;

        public UIItemsWithLabels characterStats;

        public UIItemsWithLabels weaponStats;

        public Tab Container;

    }
    List <ShopCompanion> shopCompanions = new List < ShopCompanion >();
    public void Init( ) {
        ClearList();
        foreach (var l_companion in companions){
            ShopCompanion newShop;

            var newStats = Instantiate(CompanionShopStats);
            Tab newtTab = newStats.GetComponent < Tab >( );
            newShop.Container = newtTab;
            newShop.characterStats = newtTab.characterStats.GetComponent<UIItemsWithLabels>();
            newShop.weaponStats = newtTab.WeaponStats.GetComponent < UIItemsWithLabels >( );

            newShop.characterStats.FindLabels();
            newShop.weaponStats.FindLabels();

            newShop.Container.gameObject.transform.position = CompanionShopStats.transform.position;
            newShop.Container.gameObject.SetActive(true);
            newShop.Container.gameObject.transform.SetParent(CompanionShopStats.transform.parent);
            newShop.Container.gameObject.transform.localScale = new Vector3(1, 1, 1);

            l_companion.inventoryUI = l_companion.gameObject.GetComponent<CharacterInventoryUI>();
            l_companion.stats = l_companion.GetComponent < Stat >( );
            
            newShop.companion = l_companion;
            newShop.Container.companion = l_companion;
            newShop.Container.item = l_companion.attachedWeapon;

            l_companion.inventoryUI.UpdateCharacter(newShop.characterStats);
            l_companion.inventoryUI.UpdateItem(newShop.weaponStats, newShop.companion.attachedWeapon);
            newShop.Container.shop = this;
            shopCompanions.Add(newShop);

        }
    }

    public void ClearList( ) {
        foreach ( var l_shopCompanion in shopCompanions ){
            var container = l_shopCompanion.Container;
            Destroy( container.gameObject );
        }
        shopCompanions.Clear();
    }

    // Update is called once per frame
    void Update()
    {
        StaticManager.Character.spawner.comp.RemoveAll(item => item == null);

        //SELLING
        if (StaticManager.Character.spawner.comp.Count == 0) //Player doesn't have any companions. SELLING
        {
            StaticManager.UiInventory.ItemsInstance
                    .GetLabel("CompanionSellError", StaticManager.UiInventory.ItemsInstance.ShopUI).text = "Can't Sell.";
        }
        else if(StaticManager.Character.spawner.comp.Count != 0) //Player has at least one companion. SELLING
        {
            StaticManager.UiInventory.ItemsInstance
                .GetLabel("CompanionSellError", StaticManager.UiInventory.ItemsInstance.ShopUI).text = " ";
        }


        //BUYING
        if (StaticManager.Character.inventory.coinCount < companionPrice) //Player cannot afford a companion. BUYING
        {
            StaticManager.UiInventory.ItemsInstance.GetLabel("CompanionBuyError", StaticManager.UiInventory.ItemsInstance.ShopUI).text = "Not Enough Coins.";
        }
        else if (StaticManager.Character.spawner.comp.Count == 5) //Player has all five companions active. BUYING
        {
            StaticManager.UiInventory.ItemsInstance.GetLabel("CompanionBuyError", StaticManager.UiInventory.ItemsInstance.ShopUI).text = "Can't Buy Any More.";
        }
        else if (StaticManager.Character.spawner.comp.Count < 5) //Player has less than 5 companions. BUYING
        {
            StaticManager.UiInventory.ItemsInstance.GetLabel("CompanionBuyError", StaticManager.UiInventory.ItemsInstance.ShopUI).text = " ";
        }
    }
}
