using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Assertions;

public class Guard : MonoBehaviour
{
    NavMeshAgent agent;
    float stalk_distance;

    GameObject player = null;

    enum GuardState { idle, stalk}; GuardState state;
	// Use this for initialization
	void Start ()
    {
        agent = GetComponent<NavMeshAgent>();
        stalk_distance = 5.0f;
	}

    private void Awake()
    {
        state = GuardState.idle;
        //agent.stoppingDistance = stalk_distance;
        player = GameObject.FindGameObjectWithTag("Player");
        Assert.IsNotNull(player, "player cannot be found");
    }

    // Update is called once per frame
    void Update ()
    {
        switch (state)
        {
            case GuardState.idle:
                float distance = Vector3.Distance(transform.position, player.transform.position);
                Debug.Log(distance);
                if (distance < 10.0f)
                {
                    state = GuardState.stalk;
                }
                break;
            case GuardState.stalk:
                agent.SetDestination(player.transform.position);
                if (Vector3.Distance(transform.position, player.transform.position) < stalk_distance)
                    agent.isStopped = true;
                break;
            default:
                break;
        }
    }
}
