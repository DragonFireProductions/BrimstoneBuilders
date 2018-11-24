using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionAssignment : MonoBehaviour {

	// Use this for initialization

	public Potions potion;
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Instantiate(Potions objects ) {
		potion = objects;
	}
}
