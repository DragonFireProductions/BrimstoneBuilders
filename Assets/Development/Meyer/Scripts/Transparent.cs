using System.Collections;
using System.Collections.Generic;

using UnityEditor;

using UnityEngine;

public class Transparent : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void OnDrawGizmos( ) {
		UnityEditor.Handles.color = Color.yellow;
		Vector3 pos = new Vector3(transform.position.x, 0, transform.position.z + 5);
		UnityEditor.Handles.DrawWireDisc( pos, Vector3.down , 10 );

	}

}
