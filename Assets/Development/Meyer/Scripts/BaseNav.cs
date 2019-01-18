﻿using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.AI;

public class BaseNav : MonoBehaviour {

    public enum state {

        IDLE ,

        ATTACKING ,

        FOLLOW ,

        MOVE ,

        ENEMY_CLICKED ,

        FREEZE

    }

    public NavMeshAgent Agent;

    public float battleDistance = 3;

    public BaseCharacter character;

    public AudioSource audio;

    protected float innerRadius = 8f;

    protected Vector3 newpos;

    protected float outerRadius = 10f;

    [ HideInInspector ] protected state State;

    [ SerializeField ] public float stoppingDistance;

    [ SerializeField ] protected float waittime = 0.5f;

    protected float timer = 0;

    protected float speed;

    protected Vector3 lastPosition;

    public void RunAway( ) {
        SetState = state.IDLE;
        StartCoroutine( Run( ) );
    }

    IEnumerator Run( ) {
        yield return new WaitForSeconds(4);

        SetState = state.ATTACKING;
    }
    public state SetState {
        get { return State; }
        set
        {
            Agent.isStopped = false;

            if ( value == state.FOLLOW ){
                Agent.stoppingDistance = stoppingDistance;
            }

            if ( value == state.ATTACKING ){
                Agent.stoppingDistance = stoppingDistance;
                newpos = StaticManager.Utility.randomInsideDonut(outerRadius, innerRadius, StaticManager.Character.transform.position);
            }

            if ( value == state.MOVE ){
                Agent.stoppingDistance = 2;
                Agent.isStopped        = false;
            }

            if ( value == state.ENEMY_CLICKED ){
                Agent.stoppingDistance = stoppingDistance;
            }

            if ( value == state.IDLE ){
                Agent.stoppingDistance = stoppingDistance;
            }

            if(value == state.FREEZE)
            {
                Agent.isStopped = true;
            }

            State = value;
        }
    }

    // Use this for initialization
    protected void Start( ) {
        Agent.stoppingDistance = stoppingDistance;
        audio = gameObject.GetComponent<AudioSource>();
    }
    void FixedUpdate()
    {
        speed = Mathf.Lerp(speed, (transform.position - lastPosition).magnitude / Time.deltaTime, 0.75f);
        lastPosition = transform.position;
       
    }
    // Update is called once per frame
    protected virtual void Update( ) {
        if (!Agent.isStopped)
            audio.Play();
        //character.AnimationClass.animation.SetFloat("Walk", 3);
        switch ( State ){
            
            default:

                break;
        }
    }

}