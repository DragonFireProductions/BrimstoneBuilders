using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class BaseItems : MonoBehaviour {

	public GameObject item;

	public ItemStats stats;

	public BaseCharacter AttachedCharacter;

	public string objectName;

	public virtual void Use(BaseCharacter enemy = null  ) {
		
	}

	public virtual void Attach( ) {
		var a = AttachedCharacter as Companion;
	}
	// Use this for initialization
	protected virtual void Start () {
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
