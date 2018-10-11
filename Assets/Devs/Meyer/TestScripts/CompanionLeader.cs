using System.Collections;
using System.Collections.Generic;

using Assets.Meyer.TestScripts;

using UnityEngine;
using UnityEngine.AI;

public class CompanionLeader : Companion {
	
	//[SerializeField] List < GameObject > companions;

	//public List < Companion > CompanionGroup;

    // Use this for initialization
    void Start () {
		leader = this;
		obj = this.gameObject;
		Nav = null;
		agent = this.GetComponent < NavMeshAgent >( );
		stats = this.gameObject.GetComponent < Stat >( );
		camHolder = this.gameObject.transform.Find( "CamHolder" ).gameObject;

		foreach ( var VARIABLE in characterObjs){
			characters.Add(VARIABLE.GetComponent<Companion>());
		}
	}
	
    
}
