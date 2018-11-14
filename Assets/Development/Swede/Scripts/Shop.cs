using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Shop : MonoBehaviour
{
    [SerializeField] public int companionPrice;
    [SerializeField] public int resaleWorth;
    // Use this for initialization
    void Start()
    {
        StaticManager.UiInventory.ItemsInstance.GetLabel("CompanionBuyPrice", StaticManager.UiInventory.ItemsInstance.ShopUI).text = companionPrice.ToString();
        StaticManager.UiInventory.ItemsInstance.GetLabel("CompanionSellPrice", StaticManager.UiInventory.ItemsInstance.ShopUI).text = resaleWorth.ToString();
        StaticManager.UiInventory.ItemsInstance.GetLabel("CompanionBuyError", StaticManager.UiInventory.ItemsInstance.ShopUI).text = " ";
        StaticManager.UiInventory.ItemsInstance.GetLabel("CompanionSellError", StaticManager.UiInventory.ItemsInstance.ShopUI).text = " ";
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
