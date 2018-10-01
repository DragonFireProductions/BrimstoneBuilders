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

    Vector3 from = new Vector3(0.0f, 120.0f, 0.0f);
    Vector3 to = new Vector3(0.0f, 210.0f, 0.0f);

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

                //making the guard rotate to look for scoundrels
                Quaternion from = Quaternion.Euler(this.from);
                Quaternion to = Quaternion.Euler(this.to);
                float lerp = 0.5f * (1.0f + Mathf.Sin(Mathf.PI * Time.realtimeSinceStartup * 1.0f));
                transform.rotation = Quaternion.Lerp(from, to, lerp);

                //checkiing to see if the guard has spotted any scoundrels
                RaycastHit hit;
                Vector3 forward = gameObject.transform.forward * 10.0f;
                Debug.DrawRay(transform.position, forward, Color.blue);
                Ray ray = new Ray(transform.position, forward);
                if (Physics.Raycast(ray, out hit, 10.0f))
                {
                    if (hit.collider.tag == "Player")
                    {
                        Debug.Log("i c u");
                        state = GuardState.stalk;
                    }
                }
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
