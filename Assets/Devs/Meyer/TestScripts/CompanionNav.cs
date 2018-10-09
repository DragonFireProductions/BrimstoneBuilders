﻿using System.Collections;
using System.Collections.Generic;

using Assets.Meyer.TestScripts;
using Assets.Meyer.TestScripts.Player;

using Kristal;

using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Assertions;

public class CompanionNav : MonoBehaviour
{

    /// <remarks>Set in Inspector</remarks>
    [SerializeField] Animator animator;
    [SerializeField] float VeiwDistance;
    [SerializeField] float WanderDistance;
    [SerializeField] float WanderDelay;
    [SerializeField] float StoppingDistance;
    [SerializeField] float MaintainAttackDistance;

    [SerializeField] private GameObject location;
    CompanionState state;
    GameObject player = null;
    public NavMeshAgent Agent;
    private float Timer = 0;
    private float timer1 = 0;

    private bool hasReachedDestination = false;

    [SerializeField]
    GameObject[] guard;

    Vector3 s_location;

    
    void Awake()
    {
        //Get Player form level manager


        if (GetComponent<NavMeshAgent>() != null)
        {
            Agent = GetComponent<NavMeshAgent>();
        }
        else
        {
            gameObject.AddComponent<NavMeshAgent>();
            Agent = GetComponent<NavMeshAgent>();
        }

        player = GameObject.FindGameObjectWithTag("Player");
        Assert.IsNotNull(player, "Player or Player.Player script cannot be found");

        State = CompanionState.Follow;
        Timer = Time.deltaTime;

        Agent.stoppingDistance = StoppingDistance;

        s_location = transform.position;
    }
    public void Switch(CompanionState state)
    {
        switch (state)
        {
            case CompanionState.Follow:
                State = CompanionState.Follow;

                break;
            case CompanionState.Attacking:
                State = CompanionState.Attacking;

                break;
        }
    }

    /// <summary>
    /// Moves the agent based on current State
    /// </summary>
    void Update()
    {
        Timer += Time.deltaTime;



        switch (State)
        {
            case CompanionState.Follow:
                if (player != null)
                {
                    if (!Agent.enabled)
                    {
                        CharacterUtility.instance.EnableObstacle(Agent, true);
                    }

                    Agent.stoppingDistance = 4.0f;
                    var r = Random.Range(4, 10);
                    var a = Character.player.transform.forward + (-Vector3.forward * r);
                    Agent.destination = Character.player.transform.position;
                }

                break;
            case CompanionState.Attacking:
                {
                    Agent.stoppingDistance = 0;
                }
                break;
            default:
                break;
        }
    }
    
    
    public CompanionState State {
        get { return state;}
        set { state = value; }
    }


    
    public enum CompanionState { Follow, Attacking }


}
