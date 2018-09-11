using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BaseEnemyV2 : MonoBehaviour
{
    [Header("EnemySettings")]
    public Stat health;
    public int attackDamage;
    public float attackSpeed;
    [HideInInspector]
    public float attackTimer;
    public float attackRange;
    public float moveSpeed;
    public int spawnCost;

    [Header("UnitySettings")]
    PlayerController player;
    BaseTurret turret;
    WaveSpawner waveSpawnerRef;
    DayNightCycle dayRef;
    public EnemyTargetManager targetManager;

    CapsuleCollider attackRadius;

    Vector3 targetWayPoint;
    public Transform[] wayPoints;
    int wayPointIndex = 0;
    public float distanceThreshold;

    //Transform target;

    //[SerializeField]
    //Transform curTarget;

    [SerializeField]
    Transform closestTarget;

    //Must be populated within inspector
    public List<bool> targetsInRange;

    public int minCoins;
    public int maxCoins;
    public GameObject coin;
    NavMeshAgent agent;

    Ray attackRay;
    RaycastHit attackHit;

    void Awake()
    {
        health.SetValues();
        player = FindObjectOfType<PlayerController>();
        turret = FindObjectOfType<BaseTurret>();
        targetManager = FindObjectOfType<EnemyTargetManager>();
        dayRef = FindObjectOfType<DayNightCycle>();
        agent = GetComponent<NavMeshAgent>();
        attackRadius = GetComponent<CapsuleCollider>();
        agent.speed = moveSpeed;
        attackRadius.radius = attackRange;
        attackRadius.height = attackRange;
    }

    void Start()
    {
        targetWayPoint = wayPoints[0].position;
    }

    void Update()
    {
        CheckForDamage();
        FindClosestTarget();
    }

    void LateUpdate()
    {
        if (closestTarget == null)
            MoveToWayPoint();
        else
            MoveToTarget();
    }

    public void ChangeWayPoint()
    {
        int chosenWayPoint = Random.Range(0, wayPoints.Length);
        targetWayPoint = wayPoints[chosenWayPoint].position;
    }

    public void MoveToWayPoint()
    {
        agent.SetDestination(targetWayPoint);
        agent.speed = moveSpeed;

        if (Vector3.Distance(transform.position, targetWayPoint) < attackRange)
        {
            ChangeWayPoint();
        }

    }

    public void MoveToTarget()
    {
        agent.SetDestination(closestTarget.position);
        agent.speed = moveSpeed;

        if (Vector3.Distance(transform.position, closestTarget.position) <= distanceThreshold)
        {
            Attack();
        }
    }

    public void FindClosestTarget()
    {
        closestTarget = null;
        float shortestDistance = Mathf.Infinity;
        Vector3 position = transform.position;

        foreach (Transform target in targetManager.targets)
        {
            float distanceToTarget = Vector3.Distance(transform.position, target.transform.position);

            if (distanceToTarget < shortestDistance)
            {
                shortestDistance = distanceToTarget;
                closestTarget = target;
            }
        }
    }

    //public void GetTargets()
    //{
    //    if (dayRef.GetMeridiem() == DayNightCycle.Meridiem.PM)
    //        if (dayRef.GetHour() == 8)
    //            targetManager.GetTargets();
    //    else
    //        return;
    //}

    public void Attack()
    {
        attackRay.origin = transform.position;
        attackRay.direction = transform.forward;

        if (targetsInRange[2] == true && health.CurValue > 0)
        {
            if (Physics.Raycast(attackRay, out attackHit))
            {
                attackTimer += Time.deltaTime;

                if (attackTimer >= attackSpeed)
                {
                    if (turret.health.CurValue > 0)
                    {
                        turret.TakeDamage(attackDamage, attackHit.point);
                    }
                    attackTimer = 0;
                }
            }
        }
    }

    public void CheckForDamage()
    {
        if (health.CurValue == health.MaxValue)
            health.bar.gameObject.SetActive(false);
        else
            health.bar.gameObject.SetActive(true);
    }

    public void TakeDamage(int amount, Vector3 hitPoint)
    {
        health.CurValue -= amount;

        //hitParticles.transform.positon = hitPoint;
        //hitPartcles.Play();

        if (health.CurValue <= 0)
            Die();
    }

    void Die()
    {
        int reward = Random.Range(minCoins, maxCoins);

        for (int r = 0; r < reward; r++)
        {
            Instantiate(coin, transform.position, transform.rotation);
        }

        Destroy(gameObject);

    }

    void OnTriggerEnter(Collider target)
    {
        if (target.tag == "Player")
        {
            targetsInRange[0] = true;
        }

        if (target.tag == "Fire")
        {
            targetsInRange[1] = true;
        }

        if (target.tag == "Turret")
        {
            targetsInRange[2] = true;
        }
    }

    void OnTriggerExit(Collider target)
    {
        if (target.tag == "Player")
        {
            targetsInRange[0] = false;
        }

        if (target.tag == "Fire")
        {
            targetsInRange[1] = false;
        }

        if (target.tag == "Turret")
        {
            targetsInRange[2] = false;
        }
    }
}
