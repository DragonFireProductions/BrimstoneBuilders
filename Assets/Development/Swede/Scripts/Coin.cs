using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public int coinWorth;
    private float dir;

    public float TurnSpeed = 1;
    private bool turn;

    private AudioSource audio;
    public void Start()
    {
        
        dir = Random.Range(0.0f, 100.0f); //Randomizes the direction each coin spins.

        if ((int)dir % 2 == 0)
        {
            turn = true;
        }
        else
            turn = false;

        audio = GetComponent<AudioSource>();
        audio.playOnAwake = false;
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
            audio.Play();
            StaticManager.uiManager.ShowNotification("Picked up coin", 2);
            StaticManager.Character.inventory.coinCount += coinWorth;

            Destroy(this.gameObject, audio.clip.length);
        }
    }
}
