using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BaseNav : MonoBehaviour {

	public NavMeshAgent Agent;

	[SerializeField] protected state State;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


	public void SetDestination(Vector3 des)
	{
		Agent.SetDestination(des);

	}

    public enum state { Idle, Attacking, retreat, Battle, Follow }


    public state SetState
    {
        get { return State; }
	    set {
		    if ( State == state.Follow ){
			    Agent.stoppingDistance = 4.0f;
		    }

		    if ( State == state.Attacking ){
			    Agent.stoppingDistance = 0.0f;
		    }
		    State = value;
	    }
    }

}
