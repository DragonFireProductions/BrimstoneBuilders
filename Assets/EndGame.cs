using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGame : MonoBehaviour
{
    [SerializeField] private GameObject Player;
    [SerializeField] private GameObject CreditsScreen;
    // Start is called before the first frame update
    void Start()
    {
        CreditsScreen.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            CreditsScreen.SetActive(true);

            StaticManager.map.UseCamera = true;
            StaticManager.map.UseCamera1 = false;
            StaticManager.map.UseCamera2 = false;

            StaticManager.map.mapCamera.enabled = false;
            StaticManager.map.mapCamera1.enabled = false;
            StaticManager.map.mapCamera2.enabled = false;


            StaticManager.KeyboardInput = false;
        }
    }
}
