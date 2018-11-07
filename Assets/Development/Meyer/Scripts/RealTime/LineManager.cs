using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;

using UnityEngine;

public class LineManager : MonoBehaviour {

	[SerializeField] public static GameObject[] line;

	private static int currentIndex = 1;

	private static bool negative = false;

	// Use this for initialization
	void Awake () {
		int count = GameObject.Find( "Line" ).transform.childCount;
		line = new GameObject[count];
		for ( int i = 0 ; i < count ; i++ ){
			line[i] = GameObject.Find("Line").transform.GetChild(i).gameObject;
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public static int assignIndex( ) {

		if (negative  ){
			negative = false;
			currentIndex = -currentIndex;
		currentIndex++;

        }
		else{
			currentIndex = -currentIndex;
			negative = true;
		}

		return currentIndex;
	}

}
