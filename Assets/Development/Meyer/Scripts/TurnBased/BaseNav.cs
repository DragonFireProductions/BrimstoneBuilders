using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BaseNav : MonoBehaviour {

	public NavMeshAgent Agent;

	[ SerializeField ] protected float stoppingDistance = 4;

	[SerializeField] protected state State;

	public BaseCharacter Character;
	// Use this for initialization
	protected void Start () {
		Character = gameObject.GetComponent < BaseCharacter >( );
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
    public enum state { IDLE, ATTACKING, FOLLOW, MOVE, ENEMY_CLICKED }


    public state SetState
    {
        get { return State; }
	    set {
		    if ( State == state.FOLLOW ){
			    Agent.stoppingDistance = stoppingDistance;
		    }

		    if ( State == state.ATTACKING ){
			    Agent.stoppingDistance = stoppingDistance;
		    }

		    if ( State == state.MOVE ){
			    Agent.stoppingDistance = 0;
		    }

		    if ( State == state.ENEMY_CLICKED ){
			    Agent.stoppingDistance = stoppingDistance;
		    }
			
			
		    State = value;
	    }
    }

}
