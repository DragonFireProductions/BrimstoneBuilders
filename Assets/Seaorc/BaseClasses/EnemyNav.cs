using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;
using UnityEngine.Assertions;

public class EnemyNav : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] float VeiwDistance;
    [SerializeField] float WanderDistance;
    [SerializeField] float WanderDelay;
    [SerializeField] float StoppingDistance;
    [SerializeField] float MaintainAttackDistance;

    EnemyState State;
    GameObject player = null;
    NavMeshAgent Agent;
    float Timer;

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
                    Agent.destination = Random.insideUnitSphere * WanderDistance;
                    Timer = Time.time + WanderDelay;
                }

                break;
            case EnemyState.Attacking:
                if (player != null && Agent != null)
                {
                    if (Vector3.Distance(transform.position, player.transform.position) < MaintainAttackDistance)
                        Agent.destination = player.transform.position;
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