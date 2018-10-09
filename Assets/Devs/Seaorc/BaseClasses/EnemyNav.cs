using System.Collections;
using System.Collections.Generic;

using Assets.Meyer.TestScripts;
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
    public EnemyState State;
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
        guard = GameObject.FindGameObjectsWithTag("Guard");
        Assert.IsNotNull(guard, "guard is not the guard");

        animator = gameObject.GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
        Assert.IsNotNull(player, "Player or Player.Player script cannot be found");

        Assert.IsNotNull(animator, "No animator attached to enemy");
        Timer = Time.deltaTime;

        Agent.stoppingDistance = StoppingDistance;

        s_location = transform.position;
    }

    void Start( ) {
         Agent.destination = Random.insideUnitSphere * WanderDistance + location.transform.position;
    }

    /// <summary>
    /// Moves the agent based on current State
    /// </summary>
    void Update()
    {
        Timer += Time.deltaTime;

        for (int i = 0; i < guard.Length; ++i)
        {
            float distance = Vector3.Distance(transform.position, guard[i].transform.position);
            //Debug.Log(distance);
            if (distance < 1.0f)
            {
                State = EnemyState.retreat;
            }
        }
        
        switch (State)
        {
            case EnemyState.Idle:
                if (player != null && !TurnBasedController.instance.AttackMode)
                {
                    if ( Vector3.Distance( transform.position , player.transform.position ) < VeiwDistance ){

                        if ( TurnBasedController.instance == null ){
                            Character.player.AddComponent<TurnBasedController  >();
                        }

                        Agent.stoppingDistance = 0;
                        TurnBasedController.instance.HasCollided(this.gameObject.GetComponent<Enemy>());
                    }
                }

                if (WanderDelay <= Timer && CharacterUtility.instance.NavDistanceCheck(Agent) == DistanceCheck.HasReachedDestination || CharacterUtility.instance.NavDistanceCheck(Agent) ==DistanceCheck.HasNoPath)
                {
                    Agent.destination = Random.insideUnitSphere * WanderDistance + location.transform.position;
                    Timer = 0;
                }
                break;
            case EnemyState.retreat:
                Agent.SetDestination(s_location);
                float distance = Vector3.Distance(transform.position, s_location);
                Debug.Log(distance);
                if (distance < 3.0f)
                {
                    Debug.Log("Idle");
                    State = EnemyState.Idle;
                }
                break;
            case EnemyState.Battle:

                
                break;
            case EnemyState.Follow:
                Agent.stoppingDistance = 5;
               Agent.destination = gameObject.GetComponent < Enemy >( ).Leader.gameObject.transform.position;
                if (player != null && !TurnBasedController.instance.AttackMode)
                {
                    if (Vector3.Distance(transform.position, player.transform.position) < VeiwDistance)
                    {

                        if (TurnBasedController.instance == null)
                        {
                            Character.player.AddComponent<TurnBasedController>();
                        }

                        Agent.stoppingDistance = 0;
                        TurnBasedController.instance.HasCollided(this.gameObject.GetComponent<Enemy>());
                    }
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

public enum EnemyState { Idle, Attacking, retreat, Battle, Follow }