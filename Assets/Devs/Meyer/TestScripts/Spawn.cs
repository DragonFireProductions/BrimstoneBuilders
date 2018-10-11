using System.Collections;
using System.Collections.Generic;

using Assets.Meyer.TestScripts.Player;

using Kristal;

using UnityEngine;

public class Spawn : MonoBehaviour {

	public List < List < BaseCharacter > > enemyGroups;

	[ HideInInspector ] public List < BaseCharacter > currentGroup;

	// Use this for initialization
	void Start () {
		currentGroup = enemyGroups[ 0 ];
	}

	void Instatiate() {
		currentGroup = enemyGroups[(int)LevelManagerV2.instance.level ];
		GameObject location = new GameObject();
		location.transform.position = Character.player.transform.position + ( Vector3.forward * 20 );
		foreach ( Enemy VARIABLE in currentGroup ){
			var randomLocation = Random.insideUnitSphere + location.transform.position;
			VARIABLE.enabled = true;
			VARIABLE.Nav.s_location = randomLocation;
		}
	}
	// Update is called once per frame
	void Update () {
		
	}
}
