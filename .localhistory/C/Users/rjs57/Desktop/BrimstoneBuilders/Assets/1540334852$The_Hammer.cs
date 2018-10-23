using System.Collections;
using System.Collections.Generic;
using Assets.Meyer.TestScripts;
using Kristal;
using UnityEngine;
using UnityEngine.AI;

public class The_Hammer : EnemyLeader
{
    private float battleSpeed = 3;

    [SerializeField] GameObject[] players;
    private int currenemy;

    void Awake()
    {
        base.Awake();
    }
	// Use this for initialization
	void Start ()
	{
        base.Start();
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
	//void Update ()
	//{
	//    distance = Vector3.Distance(transform.position, players[0].transform.position);
	//    if (distance < 5.0f)
	//    {
	//        if (TurnBasedController.instance == null)
	//        {
	//            GameObject.Find("ManagerHolder").gameObject.AddComponent<TurnBasedController>();
	//        }

	//        agent.stoppingDistance = 0;
	//        agent.speed = battleSpeed;

	//        TurnBasedController.instance.HasCollided(this.gameObject.GetComponent<Enemy>());
 //       }
	//}
}
