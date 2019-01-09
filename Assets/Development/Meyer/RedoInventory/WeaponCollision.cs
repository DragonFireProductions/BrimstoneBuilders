using System.Collections;
using System.Collections.Generic;

using TMPro;

using UnityEngine;

public class WeaponCollision : MonoBehaviour {

	public WeaponObject obj;
	// Use this for initialization
	void Start () {
		
	}
	

	public void OnTriggerEnter( Collider collider ) {
		if ( collider.tag == "Player" || collider.tag == "Enemy" || collider.tag == "Companion" ){
			obj.DamageCollider(collider);
		}
		
	}

	// Update is called once per frame
	void Update () {
		
	}
}
