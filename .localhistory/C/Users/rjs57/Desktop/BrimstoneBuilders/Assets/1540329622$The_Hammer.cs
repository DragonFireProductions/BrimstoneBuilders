using System.Collections;
using System.Collections.Generic;
using Kristal;
using UnityEngine;
using UnityEngine.AI;

public class The_Hammer : Enemy
{
    private Stat stats;
    private NavMeshAgent agent;
    private float distance;

    [SerializeField] private GameObject[] players;

	// Use this for initialization
	void Start ()
	{
	    stats.Agility = 30.0f;
	    stats.AttackPoints = 6;
	    stats.Dexterity = 30.0f;
	    stats.Endurance = 30.0f;
	    stats.Health = 100.0f;
	    stats.MaxHealth = 100.0f;
	    agent = GetComponent<NavMeshAgent>();
	}

	// Update is called once per frame
	void Update ()
	{
	    for (int i = 0; i < players.Length; ++i)
	    {
	        distance = Vector3.Distance(transform.position, players[i].transform.position);

	        if (distance < 5.0f)
	        {
	            agent.SetDestination(players[i].transform.position);
	        }
	    }
	}
}
