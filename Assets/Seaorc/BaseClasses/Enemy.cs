using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] float Health;
    [SerializeField] Animator animator;
    [SerializeField] float Damage;
    [SerializeField] float VeiwDistance;

    EnemyState State;
    Player player = null;
    NavMeshAgent Agent;

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

        State = EnemyState.Idle;
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
                break;
            case EnemyState.Attacking:
                if (player != null && Agent != null)
                    Agent.destination = player.transform.position;
                break;
            default:
                break;
        }



    }

    public void TakeDamage(float _damage)
    {
        Health -= _damage;

        if (Health <= 0)
            Die();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<Player>())
        {
            other.GetComponent<Player>().Damage(Damage);
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }

    public void SetHealth(float NewHealth) { Health = NewHealth; }
    public float GetHealth() { return Health; }

    public void SetDamage(float NewDamage) { Damage = NewDamage; }
    public float GetDamage() { return Damage; }
}

public enum EnemyState { Idle, Attacking}