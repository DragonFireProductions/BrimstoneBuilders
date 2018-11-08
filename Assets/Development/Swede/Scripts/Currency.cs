using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Currency : MonoBehaviour
{


    //handle instantiation when an enemy dies (spawn in coins at his last location).
    //handle adding and removing coins from the player.

    public void AddCoins(int _coins)
    {
        StaticManager.Character.inventory.coinCount += _coins;
    }

    public void RemoveCoins(int _coins)
    {
        if (StaticManager.Character.inventory.coinCount <= _coins)
        {
            StaticManager.Character.inventory.coinCount = 0;
        }
        else
        {
            StaticManager.Character.inventory.coinCount -= _coins;
        }
    }
}
