//using System.Collections;
//using System.Collections.Generic;

//using Assets.Meyer.TestScripts;

//using UnityEditor;
//using UnityEngine;
//using UnityEngine.AI;
//using UnityEngine.Assertions;

//public class Guard : MonoBehaviour
//{
//    NavMeshAgent agent;
//    float stalk_distance;

//    static GameObject player = null;
//    float time = 0.0f;
//    Vector3 prev_location;

//    Vector3 from = new Vector3(0.0f, 120.0f, 0.0f);
//    Vector3 to = new Vector3(0.0f, 210.0f, 0.0f);

//    private Vector3 center;
//    float radius = 5.0f;

//    [SerializeField]
//    GameObject[] enemy;

//    [SerializeField]
//    Transform[] patrols;
//    [SerializeField]
//    int currentwaypoint;

//    Transform targetwaypoint;

//    int currenemy;

//    Vector3 s_position;

//    [SerializeField]
//    int s_spot;
//    [SerializeField]
//    Transform post;

//    bool tooclose = false;

//    enum GuardState { idle, stalk, caught, city_danger, stalking, battle}; GuardState state;
//	// Use this for initialization
//	void Start () {
//	    center = post.transform.position;
//        agent = GetComponent<NavMeshAgent>();
//        stalk_distance = 4.0f;
//    }

//    private void Awake()
//    {
//        state = GuardState.idle;
//        player = GameObject.FindGameObjectWithTag("Player");
//        Assert.IsNotNull(player, "player cannot be found");
//        s_position = transform.position;
//    }

//    // Update is called once per frame
//    void Update ()
//    {
//        Vector3 tmppos = transform.position;
//        tmppos.y = 0.5f;
//        transform.position = tmppos;
//        tooclose = ClosestInsideSpehere(center, radius);
//        if (tooclose == true)
//            state = GuardState.city_danger;
//        switch (state)
//        {
//            case GuardState.idle:
//                agent.isStopped = true;

//                //making the guard rotate to look for scoundrels
//                Quaternion from = Quaternion.Euler(this.from);
//                Quaternion to = Quaternion.Euler(this.to);
//                float lerp = 0.5f * (1.0f + Mathf.Sin(Mathf.PI * Time.realtimeSinceStartup * 1.0f));
//                transform.rotation = Quaternion.Lerp(from, to, lerp);

//                //checking to see if the guard has spotted any scoundrels
//                RaycastHit hit;
//                Debug.DrawRay(transform.position, transform.forward * 10.0f);
//                Ray ray = new Ray(transform.position, transform.forward);

//                if (Physics.Raycast(ray, out hit, 10.0f))
//                {
//                    if (hit.collider.tag == "Player" && !TurnBasedController.instance)
//                    {
//                        state = GuardState.stalk;
//                    }
//                }
//                break;
//            case GuardState.stalk:

//                if ( !TurnBasedController.instance ){


//                    agent.isStopped = false;
//                    agent.SetDestination( player.transform.position );
//                    float dist = Vector3.Distance( transform.position , player.transform.position );

//                    if ( Vector3.Distance( transform.position , player.transform.position ) < stalk_distance ){
//                        agent.isStopped = true;
//                        state           = GuardState.stalking;
//                    }
//                    else if ( Vector3.Distance( transform.position , player.transform.position ) > 10.0f ){
//                        agent.SetDestination( post.position );

//                        if ( Vector3.Distance( transform.position , post.position ) < 1.0f )
//                            state = GuardState.idle;
//                    }
//                }

//                break;
//            case GuardState.caught:
//                Transform target = player.transform;
//                Quaternion targetrotation = Quaternion.LookRotation(target.position - transform.position);
//                transform.rotation = Quaternion.Lerp(transform.rotation, targetrotation, 10.0f);

//                break;
//            case GuardState.city_danger:
//                agent.isStopped = false;
//                agent.SetDestination(enemy[currenemy].transform.position);
//                if (Vector3.Distance(enemy[currenemy].transform.position, center) > radius)
//                {
//                    agent.SetDestination(post.position);
//                    if (Vector3.Distance(transform.position, post.position) < 3.0f)
//                    {
//                        state = GuardState.idle;
//                    }
//                }

//                break;
//            case GuardState.stalking:
//                time += Time.deltaTime;
//                Debug.Log(time);
//                if (time > 3.0f)
//                {
//                   state = GuardState.idle;
//                   time = 0.0f;
//                }
//                break;
//            case GuardState.battle:
//                agent.SetDestination(post.transform.position);

//                var check = StaticManager.utility.NavDistanceCheck( this.agent );
//                if ( check == DistanceCheck.NavMeshNotEnabled || check == DistanceCheck.HasNotReachedDestination ){
//                    if ( check == DistanceCheck.NavMeshNotEnabled ){
//                        this.gameObject.GetComponent < NavMeshAgent >( ).enabled = true;
//                    }
//                    agent.SetDestination( post.transform.position );
//                }

//                break;
//            default:
//                break;
//        }
//    }
//    private void OnTriggerEnter(Collider other)
//    {
//        if (other.gameObject.tag == "Player")
//        {
//            state = GuardState.caught;

//        }
//    }

//    private void OnTriggerExit(Collider other)
//    {
//        state = GuardState.stalk;
//    }

//    bool ClosestInsideSpehere(Vector3 center, float radius)
//    {
//        Collider[] toofar = Physics.OverlapSphere(center, radius);
//        enemy = GameObject.FindGameObjectsWithTag("Enemy");


//        for (int i = 0; i < toofar.Length; ++i)
//        {
//            if (toofar[i].tag.Contains("Enemy") && !TurnBasedController.instance)
//            {
//                state = GuardState.city_danger;
//                for (int j = 0; j < enemy.Length; ++j)
//                {
//                    float distance = Vector3.Distance(enemy[j].transform.position, center);

//                    if (distance < radius)
//                    {
//                        currenemy = j;
//                        return true;
//                    }
//                }
//            }

//        }

//        return false;
//    }

//    IEnumerator Patroling()
//    {
//        if (currentwaypoint < patrols.Length)
//        {
//            if (targetwaypoint == null)
//            {
//                targetwaypoint = patrols[currentwaypoint];
//                transform.position = Vector3.MoveTowards(transform.position, targetwaypoint.position, 4.0f * Time.deltaTime);

//                if (transform.position == targetwaypoint.position)
//                {
//                    ++currentwaypoint;
//                    targetwaypoint = patrols[currentwaypoint];
//                }
//                yield return new WaitForEndOfFrame();
//            }
//            yield return null;
//        }
//    }
//}
