using System.Collections;
using System.Collections.Generic;

using TMPro;

using UnityEngine;

public class WeaponCollision : MonoBehaviour {

	public WeaponObject obj;
	// Use this for initialization
	void Start () {
		
	}

	public void TurnOnCollider() {
		GetComponent < Collider >( ).enabled = true;
	}

	public void TurnOffCollider( ) {
		GetComponent < Collider >( ).enabled = false;
	}

	public void OnTriggerEnter( Collider collider ) {
		obj.DamageCollider(collider);
	}

	// Update is called once per frame
	void Update () {
		
	}
}
