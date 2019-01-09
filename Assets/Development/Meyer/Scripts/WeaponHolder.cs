using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHolder : MonoBehaviour {

	public GameObject[] weapons;
	public void AttachWeapon( BaseCharacter character ) {
		//character.attachedWeapon = Instantiate(weapons[ Random.Range( 0 , weapons.Length ) ].GetComponent<WeaponObject>());
		//character.attachedWeapon.leftHand.transform.position = character.leftHand.transform.position;
		//character.attachedWeapon.rightHand.transform.position = character.rightHand.transform.position;
		//character.attachedWeapon.leftHand.transform.rotation = character.leftHand.transform.rotation;
		//character.attachedWeapon.rightHand.transform.rotation = character.rightHand.transform.rotation;
		//character.attachedWeapon.leftHand.transform.SetParent(character.leftHand.transform);
		//character.attachedWeapon.leftHand.transform.SetParent(character.leftHand.transform);

		//character.attachedWeapon.AttachedCharacter = character;
		//character.attachedWeapon.gameObject.layer = character.gameObject.layer;
		//character.attachedWeapon.tag = "Weapon";
		//var l_gun = character.attachedWeapon as GunType;

		//if ( l_gun != null ){
		//	l_gun.FillBullets(character.gameObject);
		//}
	}
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
