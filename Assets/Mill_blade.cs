using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mill_blade : MonoBehaviour
{

    public float Speed;
	// Use this for initialization
	void Start ()
	{
	}
	
	// Update is called once per frame
	void Update ()
	{
	    this.transform.Rotate(Vector3.forward * Speed);
	}
}
