using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class skull : MonoBehaviour {

	// Use this for initialization
	void Start()
	{
		transform.LookAt(Camera.main.transform.position, Vector3.up);
	}

	// Update is called once per frame
	void Update()
	{

	}
}
