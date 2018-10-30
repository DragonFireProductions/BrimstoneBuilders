using System.Collections;
using System.Collections.Generic;
using Assets.Meyer.TestScripts;
using Kristal;
using UnityEngine;
using UnityEngine.AI;

public class The_Hammer : Enemy
{
    private float battleSpeed = 3;
    private Stat stats;
    private NavMeshAgent agent;
    private float distance;

    [SerializeField] GameObject[] players;
    private int currenemy;

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
	    stats = GetComponent<Stat>();
	}

	// Update is called once per frame
	void Update ()
	{
	    Ray ray = new Ray(transform.position, Vector3.forward);
	    RaycastHit hit;
	    if (Physics.Raycast(ray, out hit, 10.0f))
	    {
	        if (hit.collider.tag == "Player")
	        {
	            if (TurnBasedController.instance == null)
	            {
	                GameObject.Find("ManagerHolder").gameObject.AddComponent<TurnBasedController>();
	            }

	            agent.stoppingDistance = 0;
	            agent.speed = battleSpeed;

	            TurnBasedController.instance.HasCollided(this.gameObject.GetComponent<Enemy>());
            }
	    }
	}
}
