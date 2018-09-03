using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
   [HideInInspector]
    public InventoryItem weapon;

    public string weaponName;
	// Use this for initialization
	void Start ()
	{
	    gameObject.tag = "Weapon";
	    weapon = InventoryPlayer.inventory.get_item(weaponName);

	}
	
	// Update is called once per frame
	void Update () {
		
	}

}
