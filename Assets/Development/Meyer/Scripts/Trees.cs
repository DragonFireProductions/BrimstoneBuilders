using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trees : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if ( Vector3.Distance(Camera.main.transform.position , transform.position) < 30 ){
			Debug.Log("Transparent");
		}
	}
}
