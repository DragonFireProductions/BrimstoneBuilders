using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.UIElements;

using Image = UnityEngine.UI.Image;

public class Currency : MonoBehaviour
{
    public int companionBuyCost;

    public int companionSellCost;

    public Shop _shop;

    public GameObject buyContainer;

    public GameObject sellContainer;

    public List < GameObject > shops;

    public GameObject sellButton;

    public GameObject buyButton;

    public void AddCoins(Shop shop)
    {
        StaticManager.Character.inventory.coinCount += shop.resaleWorth;
    }
    public void AddCoins(int _coins)
    {
        StaticManager.Character.inventory.coinCount += _coins;
    }
    
    public void RemoveCoins(Shop shop)
    {
        if (StaticManager.Character.inventory.coinCount <= shop.companionPrice)
        {
            StaticManager.Character.inventory.coinCount = 0;
        }
        else
        {
            StaticManager.Character.inventory.coinCount -= shop.companionPrice;
        }
    }
    public bool RemoveCoins(int coins)
    {
        if (StaticManager.Character.inventory.coinCount < coins)
        {
            return false;
        }
        else
        {
            StaticManager.Character.inventory.coinCount -= coins;
            return true;
        }
    }

    public void SwitchToBuy( ) {
        StaticManager.UiInventory.ItemsInstance
                        .GetLabel("CompanionBuyError", StaticManager.UiInventory.ItemsInstance.ShopUI).text = "";
        buyButton.GetComponent < Image >( ).color = Color.red;
         sellButton.GetComponent < Image >( ).color = Color.grey;
        _shop.Buy.SetActive(true);
        _shop.Sell.SetActive(false);
    }

    public void SwitchToSell( ) {
         buyButton.GetComponent < Image >( ).color = Color.grey;
         sellButton.GetComponent < Image >( ).color = Color.red;
        _shop.Buy.SetActive(false);
        _shop.Sell.SetActive(true);
    }
    
    public void BuyCompanion(Tab container) {
        StaticManager.Character.spawner.comp.RemoveAll( item => item == null );
        if (StaticManager.Character.spawner.comp.Count < 5)
        {
            StaticManager.UiInventory.ItemsInstance
                        .GetLabel("CompanionBuyError", StaticManager.UiInventory.ItemsInstance.ShopUI).text = "";
            if ( RemoveCoins( container.companion.ResaleWorth ) ){
                StaticManager.UiInventory.ItemsInstance
                            .GetLabel("CompanionBuyError", StaticManager.UiInventory.ItemsInstance.ShopUI).text = "";
                StaticManager.Character.spawner.CompanionSpawn( container.companion );
                container.companion.inventoryUI.SellButton.SetActive(true);
                container.companion.inventoryUI.BuyButton.SetActive(false);
                container.companion.inventoryUI.tab.gameObject.SetActive(true);
                container.gameObject.transform.SetParent(_shop.Sell.transform);
            }
            else{
               
                    StaticManager.UiInventory.ItemsInstance
                            .GetLabel("CompanionBuyError", StaticManager.UiInventory.ItemsInstance.ShopUI).text = "Not Enough Coins.";
            }
        }
        else{
            
                StaticManager.UiInventory.ItemsInstance
                        .GetLabel("CompanionBuyError", StaticManager.UiInventory.ItemsInstance.ShopUI).text = "Max Companions.";
        }
    }
    public void SellCompanion( Tab container) {
        var c = container.companion.Nav as CompanionNav;
        Destroy(c.behaviors.gameObject);
        StaticManager.inventories.Destroy(container.companion.inventory);
        AddCoins(container.companion.ResaleWorth);
        Destroy(container.companion.gameObject);
        _shop.shopCompanions.Remove( container );
        Destroy(container.gameObject);
    }
}
