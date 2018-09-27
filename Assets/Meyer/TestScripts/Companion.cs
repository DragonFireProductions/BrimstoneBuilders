using System.Collections;
using System.Collections.Generic;

using Assets.Meyer.TestScripts.Player;

using UnityEngine;
using UnityEngine.AI;

public class Companion : MonoBehaviour {

	private WeaponObject attachedWeapon;

    [ SerializeField ] private NavMeshAgent agent;

    private Vector3 startPos;

    private Quaternion startRotation;

    // Use this for initialization
    void Start () {
        TurnBased.Instance.AddCompanions(this.gameObject);

    }

    // Update is called once per frame
    void Update () { 
    }
	
	private WeaponObject AttachedWeapon {
		get { return attachedWeapon; }
		set { attachedWeapon = value; }
	}

}
