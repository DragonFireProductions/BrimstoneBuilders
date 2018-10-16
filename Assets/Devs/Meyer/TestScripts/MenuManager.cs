using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour {

	public static AudioManager audio;
	// Use this for initialization
	void Awake () {
		audio = gameObject.GetComponent < AudioManager >( );
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
