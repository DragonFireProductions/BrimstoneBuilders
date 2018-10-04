using System.Collections;
using System.Collections.Generic;

using Assets.Meyer.TestScripts.Player;

using UnityEngine;
using UnityEngine.AI;

public class Companion : MonoBehaviour {

	private WeaponObject attachedWeapon;

    [ SerializeField ] private NavMeshAgent agent;

	[ SerializeField ] private GameObject camHolder;

    private Vector3 startPos;

    private Quaternion startRotation;

	[SerializeField] float health = 100;

    // Use this for initialization
    void Start () {
	    camHolder = transform.Find( "CamHolder" ).gameObject;
    }

    // Update is called once per frame
    void Update () { 
    }
	
	public WeaponObject AttachedWeapon {
		get { return attachedWeapon; }
		set { attachedWeapon = value; }
	}

	public GameObject CamHolder {
		get { return camHolder; }
	}

	public float Health {
		get { return health; }
		set { health = value; }
	}

}
