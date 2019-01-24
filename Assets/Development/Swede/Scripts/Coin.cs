using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public int coinWorth;
    private float dir;
    private float TurnSpeed;
    private bool turn;

    public void Start()
    {
        TurnSpeed = Random.Range(0.5f, 2.0f); //Randomizes the turn speed of each coin.
        dir = Random.Range(0.0f, 100.0f); //Randomizes the direction each coin spins.

        if ((int)dir % 2 == 0)
        {
            turn = true;
        }
        else
            turn = false;
    }
    public void Update()
    {
        if (!turn)
        {
            this.transform.Rotate(Vector3.up * TurnSpeed);
        }
        else
            this.transform.Rotate(Vector3.down * TurnSpeed);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player"){
            StaticManager.uiManager.ShowNotification("Picked up coin", 5);
            StaticManager.Character.inventory.coinCount += coinWorth;
            Debug.Log("Hitting Trigger");
            Destroy(this.gameObject);
        }
    }
}
