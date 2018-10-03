using System.Collections;
using System.Collections.Generic;

using Assets.Meyer.TestScripts.Player;

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
        State = EnemyState.Idle;
        Timer = Time.deltaTime;

        Agent.stoppingDistance = StoppingDistance;

        s_location = transform.position;
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
                if (player != null && Agent != null)
                {
                    if (Vector3.Distance(transform.position, player.transform.position) < MaintainAttackDistance ||
                        timer1 < 8.0f && !TurnBased.Instance.AttackMode)
                    {
                       // Agent.destination = player.transform.position;
                        timer1 += Time.deltaTime;
                    }

                    else
                    {
                        State = EnemyState.Idle;
                        timer1 = 0;
                    }
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
            default:
                break;
        }
    }

    public void SetDestination(Vector3 des ) {
        Agent.SetDestination( des );

    }

}

public enum EnemyState { Idle, Attacking, retreat }