using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public int coinWorth;


    //Checks the trigger around the coin. If the tag matches the player, the coin gets added to the player's inventory and destroyed from the game world.
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            StaticManager.Character.inventory.PickUpCoin(coinWorth);
            Debug.Log("Hitting Trigger");
            Destroy(this.gameObject);
        }
    }
}
