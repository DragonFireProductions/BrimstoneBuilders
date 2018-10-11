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
	    this.material.color = LeaderColor;
	    camHolder = gameObject.transform.Find("CamHolder").gameObject;
	    leader = this.gameObject.GetComponent<CompanionLeader>();

		foreach ( var VARIABLE in characterObjs){
			characters.Add(VARIABLE.GetComponent<Companion>());
		}
	}
	
    
}
