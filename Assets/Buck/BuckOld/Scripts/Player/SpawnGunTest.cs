using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnGunTest : MonoBehaviour
{

    [Header("UnitySettings")]

    [SerializeField]
    Transform itemSlot;

    [SerializeField]
    GameObject basicGun;

    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        SpawnGun();
	}

    //Debugging for gun placement
    void SpawnGun()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {

            GameObject gun = Instantiate(basicGun, itemSlot.position, itemSlot.rotation) as GameObject;

            gun.transform.parent = itemSlot;
        }
    }
}
