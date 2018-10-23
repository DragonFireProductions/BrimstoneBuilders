using System.Collections;
using System.Collections.Generic;

using Assets.Meyer.TestScripts;
using Assets.Meyer.TestScripts.Player;

using Kristal;

using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Assertions;

public class CompanionNav : BaseNav
{

    /// <remarks>Set in Inspector</remarks>
    [SerializeField] Animator animator;

    [ SerializeField ] private float StoppingDistance;

    [ SerializeField ] private float battleSpeed = 3;

    [ SerializeField ] private float followSpeed = 4;
       

    [SerializeField] private GameObject location;
    GameObject player = null;
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

        SetState = State;
        Timer = Time.deltaTime;
        

        s_location = transform.position;
    }

    /// <summary>
    /// Moves the agent based on current State
    /// </summary>
    void Update()
    {
        Timer += Time.deltaTime;



        switch (State)
        {
            case state.Follow:

                if (player != null)
                {

                    if (!Agent.enabled)
                    {
                       StaticManager.utility.EnableObstacle(Agent, true);
                    }

                    Agent.stoppingDistance = 4.0f;
                    var r = Random.Range(4, 10);
                    var a = Character.player.transform.forward + (-Vector3.forward * r);
                    Agent.destination = Character.player.transform.position;
                }

                break;
            case state.Attacking:
                {
                    Agent.stoppingDistance = 0;

                }
                break;
            default:
                break;
        }
    }
    
    


    

}
