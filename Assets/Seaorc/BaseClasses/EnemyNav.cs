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

    private GameObject location;
    EnemyState State;
    GameObject player = null;
    NavMeshAgent Agent;
    private float Timer = 0;

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

        location = GameObject.Find("Location");
    }

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

                if (WanderDelay <= Timer)
                {
                    Agent.destination = Random.insideUnitSphere * WanderDistance + location.transform.position;
                    Timer = 0;
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

public enum EnemyState { Idle, Attacking }