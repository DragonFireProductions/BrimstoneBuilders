using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public int coinWorth;
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
