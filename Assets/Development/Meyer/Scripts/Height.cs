using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Height : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = new Vector3(transform.position.x, 30, transform.position.y);
	}
}
