using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;
using UnityEngine.Assertions;

public class EnemyNav : MonoBehaviour
{
    [SerializeField] protected Animator animator;
    [SerializeField] protected float VeiwDistance;
    [SerializeField] protected float WanderDistance;
    [SerializeField] protected float WanderDelay;
    [SerializeField] protected float StoppingDistance;
    [SerializeField] protected float MaintainAttackDistance;

    protected EnemyState State;
    protected GameObject player = null;
    protected NavMeshAgent Agent;
    protected float Timer;

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

    void Update()
    {
        //Debug.Log(Agent.destination);
        switch (State)
        {
            case EnemyState.Idle:
                if(player != null)
                {
                    if (Vector3.Distance(transform.position, player.transform.position) < VeiwDistance)
                        State = EnemyState.Attacking;
                }

                if (Time.time >= Timer)
                {
                    Vector3 des = Random.insideUnitSphere * WanderDistance;
                    Agent.SetDestination(des);
                    Timer = Time.time + WanderDelay;
                }

                break;
            case EnemyState.Attacking:
                if (player != null && Agent != null)
                {
                    if (Vector3.Distance(transform.position, player.transform.position) < MaintainAttackDistance)
                        Agent.SetDestination(player.transform.position); 
                    else
                    {
                        State = EnemyState.Idle;
                    }
                }

                break;
            default:
                break;
        }
    }

}

public enum EnemyState { Idle, Attacking}