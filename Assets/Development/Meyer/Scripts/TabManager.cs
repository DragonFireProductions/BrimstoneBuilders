using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TabManager : MonoBehaviour {

	public List < Tab > tabs = new List < Tab >();
	// Use this for initialization
	void Start () {
	}

	public Tab GetTab( Companion companion ) {
		foreach ( var l_tab in tabs ){
			if ( l_tab.companion == companion ){
				return l_tab;
			}
		}

		return null;
	}
	// Update is called once per frame
	void Update () {
		
	}
}
