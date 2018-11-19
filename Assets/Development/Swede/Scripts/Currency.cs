﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Currency : MonoBehaviour
{
    public int companionBuyCost;

    public int companionSellCost;

    public Shop _shop;

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

    public void BuyCompanion(Shop shop) {
        StaticManager.Character.spawner.comp.RemoveAll( item => item == null );
        if (StaticManager.Character.spawner.comp.Count < 5)
        {
            if(RemoveCoins(shop.companionPrice)) 
                StartCoroutine(StaticManager.Character.spawner.CompSpawn());
        }
    }
    public void SellCompanion(Shop shop)
    {
        StaticManager.Character.spawner.comp.RemoveAll(item => item == null);

        if (StaticManager.Character.spawner.comp.Count != 0)
        {
            AddCoins(shop.resaleWorth);
            StaticManager.Character.spawner.Kill();
        }
    }
}