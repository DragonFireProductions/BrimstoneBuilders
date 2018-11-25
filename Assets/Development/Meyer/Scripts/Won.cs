using System.Collections;
using System.Collections.Generic;

using Kristal;

using UnityEngine;

public class Won : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerStay(Collider collider ) {
		if ( collider.tag == "Player" ){
            var objs = FindObjectsOfType<Enemy>();

            if (objs.Length == 0)
            {
                //StaticManager.UiInventory.ItemsInstance.GameWon.SetActive(true);
            }
        }
		
	}
}
