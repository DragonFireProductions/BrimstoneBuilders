using System.Collections;
using System.Collections.Generic;

using Assets.Meyer.TestScripts;
using Assets.Meyer.TestScripts.Player;

using Kristal;

using UnityEngine.AI;
using UnityEngine;
using UnityEngine.Assertions;

public class EnemyNav : BaseNav
{
    /// <remarks>Set in Inspector</remarks>
    [SerializeField] Animator animator;
    [SerializeField] float VeiwDistance;
    [SerializeField] float WanderDistance;
    [SerializeField] float WanderDelay;
    [SerializeField] float StoppingDistance;
    [SerializeField] float MaintainAttackDistance;

    [ SerializeField ] private float battleSpeed = 3;

    [SerializeField] private GameObject location;
    GameObject player = null;
    private float Timer = 0;
    private float timer1 = 0;

    private bool hasReachedDestination = false;

    [SerializeField]
    GameObject[] guard;

    public Vector3 s_location;

    [SerializeField] private GameObject threat_signal;
    private Stat stats;

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

        Assert.IsNotNull(animator, "No animator attached to _enemy");
        Timer = Time.deltaTime;

        Agent.stoppingDistance = StoppingDistance;

        s_location = transform.position;

        stats = GetComponent<Stat>();
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

        if (stats.Health > 10.0f)
        {
            Vector3 tmppos = transform.position;
            tmppos.y = transform.position.y + 1.0f;
            threat_signal.transform.position = tmppos;
            threat_signal.SetActive(true);
        }


        for (int i = 0; i < guard.Length; ++i)
        {
            float distance = Vector3.Distance(transform.position, guard[i].transform.position);
            //Debug.Log(distance);
            if (distance < 1.0f)
            {
                State = state.retreat;
            }
        }

        switch (State)
        {
            case state.Idle:
                if (player != null && !TurnBasedController.instance)
                {
                    if (StaticManager.instance.isTurnBasedOn ){
                        if (Vector3.Distance(transform.position, player.transform.position) < VeiwDistance)
                        {

                            if (TurnBasedController.instance == null)
                            {
                                GameObject.Find("ManagerHolder").gameObject.AddComponent<TurnBasedController>();
                            }

                            Agent.stoppingDistance = 0;
                            Agent.speed = battleSpeed;

                            TurnBasedController.instance.HasCollided(this.gameObject.GetComponent<Enemy>());
                        }
                    }

                }

                var check = StaticManager.utility.NavDistanceCheck( Agent );
                if (WanderDelay <= Timer && (check == DistanceCheck.HasReachedDestination || check == DistanceCheck.PathInvalid)  || StaticManager.utility.NavDistanceCheck(Agent) ==DistanceCheck.HasNoPath)
                {
                    Agent.destination = Random.insideUnitSphere * WanderDistance + location.transform.position;
                    Timer = 0;
                }
                break;
            case state.retreat:
                Agent.SetDestination(s_location);
                float distance = Vector3.Distance(transform.position, s_location);
                Debug.Log(distance);
                if (distance < 3.0f)
                {
                    Debug.Log("Idle");
                    State = state.Idle;
                }
                break;
            case state.Battle:
                Agent.stoppingDistance = 0;


                break;
            case state.Follow:
                Agent.stoppingDistance = 5;
               Agent.destination = gameObject.GetComponent < Enemy >( ).Leader.gameObject.transform.position;
                if (player != null && !TurnBasedController.instance)
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




}