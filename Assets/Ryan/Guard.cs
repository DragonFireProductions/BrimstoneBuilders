using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Assertions;

public class Guard : MonoBehaviour
{
    NavMeshAgent agent;
    float stalk_distance;

    static GameObject player = null;
    float time = 0.0f;
    Vector3 prev_location;

    Vector3 from = new Vector3(0.0f, 120.0f, 0.0f);
    Vector3 to = new Vector3(0.0f, 210.0f, 0.0f);

    Vector3 center = new Vector3(0.0f, 0.0f, 0.0f);
    float radius = 5.0f;

    [SerializeField]
    GameObject[] enemy;

    int currenemy;

    Vector3 s_position;

 //overlapsphere();

    enum GuardState { idle, stalk, caught, city_danger}; GuardState state;
	// Use this for initialization
	void Start ()
    {
        agent = GetComponent<NavMeshAgent>();
        stalk_distance = 10.0f;
        enemy = GameObject.FindGameObjectsWithTag("Enemy");
    }

    private void Awake()
    {
        state = GuardState.idle;
        //agent.stoppingDistance = stalk_distance;
        player = GameObject.FindGameObjectWithTag("Player");
        Assert.IsNotNull(player, "player cannot be found");
        s_position = transform.position;
    }

    // Update is called once per frame
    void Update ()
    {
        ClosestInsideSpehere(center, radius);
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
                        //Debug.Log("i c u");
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
                }
                break;
            case GuardState.caught:
                Transform target = player.transform;
                Quaternion targetrotation = Quaternion.LookRotation(target.position - transform.position);
                transform.rotation = Quaternion.Lerp(transform.rotation, targetrotation, 10.0f);
                break;
            case GuardState.city_danger:
                agent.isStopped = false;
                //agent.SetDestination(enemy.transform.position);
                
                break;
            default:
                break;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            state = GuardState.caught;
            
        }
    }

    void ClosestInsideSpehere(Vector3 center, float radius)
    {
        Collider[] toofar = Physics.OverlapSphere(center, radius);
        //GameObject enemy = GameObject.FindGameObjectWithTag("Enemy");


        for (int i = 0; i < toofar.Length; ++i)
        {
            if (toofar[i].tag == "Enemy")
            {
                Debug.Log("too close to city");
                state = GuardState.city_danger;
                for (int j = 0; j < enemy.Length; ++j)
                {
                    float distance = Vector3.Distance(enemy[j].transform.position, center);

                    if (distance < radius)
                    {
                        agent.SetDestination(enemy[j].transform.position);
                    }
                }
            }
        }
        
        //return false;
    }
}
