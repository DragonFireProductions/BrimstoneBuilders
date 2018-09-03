using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float Health;
    [SerializeField] GameObject Object;
    [SerializeField] Animator Ani;

    CharacterController Controller;
    NavMeshAgent Agent;

    private void Awake()
    {
        if (GetComponent<CharacterController>())
            Controller = GetComponent<CharacterController>();
        else
        {
            gameObject.AddComponent<CharacterController>();
            Controller = GetComponent<CharacterController>();
        }

        if (GetComponent<NavMeshAgent>())
            Agent = GetComponent<NavMeshAgent>();
        else
        {
            gameObject.AddComponent<NavMeshAgent>();
            Agent = GetComponent<NavMeshAgent>();
        }
    }

    public void Damage(float _damage)
    {
        Health -= _damage;

        if (Health <= 0)
            Die();
    }

    public void Move()
    {

    }

    void Die()
    {
        // 
    }
}
