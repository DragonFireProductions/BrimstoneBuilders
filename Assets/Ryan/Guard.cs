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
    float time = 0.0f;
    Vector3 prev_location;

    enum GuardState { idle, stalk}; GuardState state;
	// Use this for initialization
	void Start ()
    {
        agent = GetComponent<NavMeshAgent>();
        stalk_distance = 5.0f;
        prev_location = transform.position;
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
                agent.isStopped = true;
                float distance = Vector3.Distance(transform.position, player.transform.position);
                RaycastHit hit;
                Vector3 forward = transform.TransformDirection(Vector3.forward) * 10.0f;
                Debug.DrawRay(transform.position, forward, Color.blue);
                if (Physics.Raycast(transform.position, (forward), out hit))
                {
                    if (hit.collider.gameObject.tag == "Player")
                    {
                        Debug.Log("i c u");
                        state = GuardState.stalk;
                    }
                }

                //if (distance < 10.0f)
                //{
                //    state = GuardState.stalk;

                //}
                break;
            case GuardState.stalk:
                agent.isStopped = false;
                agent.SetDestination(player.transform.position);
                if (Vector3.Distance(transform.position, player.transform.position) <= stalk_distance)
                {
                    state = GuardState.idle;
                    //agent.isStopped = true;
                    //time += Time.deltaTime;
                    //Debug.Log(time);

                    //if (time > 5.0f)
                    //{
                    //    time = 0.0f;
                    //    agent.SetDestination(prev_location);
                    //}
                }
                break;
            default:
                break;
        }
    }
}
