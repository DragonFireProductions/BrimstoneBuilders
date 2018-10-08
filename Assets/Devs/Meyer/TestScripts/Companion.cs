using System.Collections;
using System.Collections.Generic;

using Assets.Meyer.TestScripts.Player;

using UnityEngine;
using UnityEngine.AI;

public class Companion : MonoBehaviour {
	

    [ SerializeField ] public NavMeshAgent agent;

	[ SerializeField ] public GameObject camHolder;
	

	[SerializeField] float health = 100;

	public CompanionLeader leader;

	public GameObject obj;

	public CompanionNav Nav;

	public Stat Stats;

    // Use this for initialization
    void Start () {
	    camHolder = transform.Find( "CamHolder" ).gameObject;
	    Stats = gameObject.GetComponent < Stat >( );
	    obj = gameObject;
	    Nav = gameObject.GetComponent < CompanionNav >( );
	    leader = Character.player.GetComponent < CompanionLeader >( );
	    agent = gameObject.GetComponent < NavMeshAgent >( );

    }

    // Update is called once per frame
    void Update () { 
    }

	public GameObject CamHolder {
		get { return camHolder; }

	}

}
