using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHolder : MonoBehaviour {

	public GameObject[] weapons;
	public void AttachWeapon( BaseCharacter character ) {
		character.attachedWeapon = Instantiate(weapons[ Random.Range( 0 , weapons.Length ) ].GetComponent<WeaponObject>());
		character.attachedWeapon.transform.position = character.cube.transform.position;
		character.attachedWeapon.transform.rotation = character.cube.transform.rotation;
		character.attachedWeapon.transform.SetParent(character.cube.transform);
		character.attachedWeapon.AttachedCharacter = character;
		character.attachedWeapon.gameObject.layer = character.gameObject.layer;
		character.attachedWeapon.tag = "Weapon";
		var l_gun = character.attachedWeapon as GunType;

		if ( l_gun != null ){
			l_gun.FillBullets(character.gameObject);
		}
	}
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
