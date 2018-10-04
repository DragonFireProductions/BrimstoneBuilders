using System.Collections;
using System.Collections.Generic;

using Assets.Meyer.TestScripts.Player;

using Kristal;

using UnityEngine.AI;
using UnityEngine;
using UnityEngine.Assertions;

public class EnemyNav : MonoBehaviour
{
    /// <remarks>Set in Inspector</remarks>
    [SerializeField] Animator animator;
    [SerializeField] float VeiwDistance;
    [SerializeField] float WanderDistance;
    [SerializeField] float WanderDelay;
    [SerializeField] float StoppingDistance;
    [SerializeField] float MaintainAttackDistance;

    [SerializeField] private GameObject location;
    EnemyState State;
    GameObject player = null;
    NavMeshAgent Agent;
    private float Timer = 0;
    private float timer1 = 0;

    private bool hasReachedDestination = false;

    /// <summary>
    /// Initilizes all variables not set in inspector
    /// </summary>
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

        animator = gameObject.GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
        Assert.IsNotNull(player, "Player or Player.Player script cannot be found");

        Assert.IsNotNull(animator, "No animator attached to enemy");
        State = EnemyState.Idle;
        Timer = Time.deltaTime;

        Agent.stoppingDistance = StoppingDistance;

        
    }

    /// <summary>
    /// Moves the agent based on current State
    /// </summary>
    void Update()
    {
        Timer += Time.deltaTime;
        switch (State)
        {
            case EnemyState.Idle:
                if (player != null)
                {
                    if (Vector3.Distance(transform.position, player.transform.position) < VeiwDistance)
                        State = EnemyState.Attacking;
                }

                if (WanderDelay <= Timer && !TurnBased.Instance.AttackMode)
                {
                    Agent.destination = Random.insideUnitSphere * WanderDistance + location.transform.position;
                    Timer = 0;
                }

                break;
            case EnemyState.Attacking:
                if (player != null && Agent != null && !TurnBased.Instance.AttackMode){
                    gameObject.GetComponent < Enemy >( ).Leader.GetComponent<EnemyGroup>().StartBattle();
                    
                }

                break;
            default:
                break;
        }
    }

    public EnemyState SetState {
        get { return State; }
        set { State = value;}
    }


    public void SetDestination(Vector3 des ) {
        Agent.SetDestination( des );

    }


}

public enum EnemyState { Idle, Attacking }