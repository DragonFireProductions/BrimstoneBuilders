using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Currency : MonoBehaviour
{
    public int companionBuyCost;

    public int companionSellCost;

    public Shop _shop;

    //Used when conducting shop purchases.
    public void AddCoins(Shop shop)
    {
        StaticManager.Character.inventory.coinCount += shop.resaleWorth;
    }

    //Used when adding an actual coin object.
    public void AddCoins(int _coins)
    {
        StaticManager.Character.inventory.coinCount += _coins;
    }

    //Used when conducting shop purchases.
    public void RemoveCoins(Shop shop)
    {
        //if the player's coin total is less than or equal to the amount of coins
        if (StaticManager.Character.inventory.coinCount <= shop.companionPrice)
        {
            StaticManager.Character.inventory.coinCount = 0;
        }
        else
        {
            StaticManager.Character.inventory.coinCount -= shop.companionPrice;
        }
    }

    //checks to see whether or not the player has enough coins to complete the transaction.
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

    public void BuyCompanion(Shop shop)
    {
        //If the player still has slots available to hire a companion (max of five companions)
        if (StaticManager.Character.spawner.comp.Count < 5)
        {
            //If the player has enough to cover the cost, adds a companion to their service.
            if(RemoveCoins(shop.companionPrice)) 
                StartCoroutine(StaticManager.Character.spawner.CompSpawn());
        }
    }


    public void SellCompanion(Shop shop)
    {
        //If the player has companions available to get rid of...
        if(StaticManager.Character.spawner.comp.Count != 0)
        {
            //Refunds the player the ReSale value of a companion. Despawns the companion.
            AddCoins(shop.resaleWorth);
            StaticManager.Character.spawner.Kill();
        }
    }
}
