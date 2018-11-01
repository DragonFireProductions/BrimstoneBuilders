using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BaseNav : MonoBehaviour {

	public NavMeshAgent Agent;

	[ SerializeField ] public float stoppingDistance = 4;

	[SerializeField] protected state State;
	
	// Use this for initialization
	protected void Start () {
		Agent = gameObject.GetComponent < NavMeshAgent >( );
		Agent.stoppingDistance = stoppingDistance;
	}
	
	// Update is called once per frame
	void Update () {
		
	}


	public void SetDestination(Vector3 _des)
	{
		Agent.SetDestination(_des);

	}

	public void SetDestination( Transform transform ) {
		Agent.SetDestination( transform.position );
	}
    public enum state { IDLE, ATTACKING, FOLLOW, MOVE, ENEMY_CLICKED, FREEZE }


    public state SetState
    {
        get { return State; }
	    set {
		    if ( value == state.FOLLOW ){
			    Agent.stoppingDistance = stoppingDistance;
		    }

		    if ( value == state.ATTACKING ){
			    Agent.stoppingDistance = stoppingDistance;
		    }

		    if ( value == state.MOVE ){
			    Agent.stoppingDistance = 2;
		    }

		    if ( value == state.ENEMY_CLICKED ){
			    Agent.stoppingDistance = stoppingDistance;
		    }

		    if ( value == state.IDLE ){
			    Agent.stoppingDistance = stoppingDistance;
		    }

			
		    State = value;
	    }
    }

}
