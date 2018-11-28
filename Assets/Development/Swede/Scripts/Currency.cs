using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Currency : MonoBehaviour
{
    public int companionBuyCost;

    public int companionSellCost;

    public Shop _shop;

    public GameObject buyContainer;

    public GameObject sellContainer;

    public List < GameObject > shops;

    public void AddCoins(Shop shop)
    {
        StaticManager.Character.inventory.coinCount += shop.resaleWorth;
    }
    public void AddCoins(int _coins)
    {
        StaticManager.Character.inventory.coinCount += _coins;
    }

    public void SwitchToBuy( ) {
        
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
    
    public void BuyCompanion(Tab container) {
        StaticManager.Character.spawner.comp.RemoveAll( item => item == null );
        if (StaticManager.Character.spawner.comp.Count < 5)
        {
            if ( RemoveCoins( container.companion.ResaleWorth ) ){
                StaticManager.Character.spawner.CompanionSpawn( container.companion );
                container.companion.inventoryUI.SellButton.SetActive(true);
                container.companion.inventoryUI.BuyButton.SetActive(false);
            }
        }
    }
    public void SellCompanion( Tab container)
    {
        AddCoins(container.companion.ResaleWorth);
        Destroy(container.companion);
        _shop.shopCompanions.Remove( container );
        Destroy(container.gameObject);
    }
}
