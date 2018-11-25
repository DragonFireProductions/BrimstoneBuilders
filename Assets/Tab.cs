using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tab : MonoBehaviour {

	public Companion companion { get; set; }

	public BaseItems item;

	public void Attach( ) {
		item.Attach();
	}
}
