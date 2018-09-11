using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class BaseEnemy : MonoBehaviour
{
    [Header("EnemySettings")]

    public Stat health;

    public int attackDamage;
    public float attackSpeed;
    [HideInInspector]
    public float attackTimer;
    //Might be used later on
    //public float attackRange;

    public float moveSpeed;

    public int spawnCost;

    public int minCoins;
    public int maxCoins;

    [Header("UnitySettings")]

    PlayerController player;
    BaseTurret turret;
    WaveSpawner waveSpawnerRef;
    public EnemyTargetManager targetManager;

    public List<Transform> wayPoints;
    int wayPointIndex = 0;

    [SerializeField]
    Transform curTarget;

    [SerializeField]
    Transform closestTarget;

    //Must be populated within inspector
    public List<bool> targetsInRange;

    public GameObject coin;

    Transform myPos;
    NavMeshAgent agent;

    Ray attackRay;
    RaycastHit attackHit;

    //Accessors
    public int GetSpawnCost()
    {
        return spawnCost;
    }

    void Awake()
    {
        health.SetValues();
        player = FindObjectOfType<PlayerController>();
        turret = FindObjectOfType<BaseTurret>();
        targetManager = FindObjectOfType<EnemyTargetManager>();
        agent = GetComponent<NavMeshAgent>();
        myPos = transform;
        agent.speed = moveSpeed;
    }

    void Start()
    {
        targetManager.AddTarget(transform);
        //targetManager.GetTargets();
    }

    public Transform FindClosestTarget()
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
        return closestTarget;
    }

    public void MoveToTarget()
    {
        if (closestTarget != null)
        {
            curTarget = closestTarget;
            agent.SetDestination(closestTarget.position);
            agent.speed = moveSpeed;
        }
    }

    public void CheckForDamage()
    {
        if (health.CurValue == health.MaxValue)
        {
            health.bar.gameObject.SetActive(false);
        }
        else
        {
            health.bar.gameObject.SetActive(true);
        }
    }

    public void TakeDamage(int amount, Vector3 hitPoint)
    {
        health.CurValue -= amount;

        //hitParticles.transform.positon = hitPoint;
        //hitPartcles.Play();

        if (health.CurValue <= 0)
        {
            Die();
        }
    }

    //When the enemies health reaches 0
    //Drop coins, and destroy the object
    public void Die()
    {
        //Randomly reward the player a number of coins from the 
        //Min and Max ints
        int reward = Random.Range(minCoins, maxCoins);

        for (int i = 0; i < reward; i++)
        {
            //Make them coins
            Instantiate(coin, transform.position, transform.rotation);
        }
        
        List<GameObject> t = waveSpawnerRef.GetEnemies();

        for (int i = 0; i < t.Count; i++)
        {
            if (gameObject.tag == "Enemy")
            {
                t.RemoveAt(i);
                break;
            }
        }
        //Destroy that baddie
        Destroy(gameObject);
    }

    //public void AttackPlayer()
    //{
    //    attackRay.origin = transform.position;
    //    attackRay.direction = transform.forward;

    //    if (Physics.Raycast(attackRay, out attackHit))
    //    {
    //        if (player.health.CurValue > 0)
    //        {
    //            player.TakeDamage(attackDamage, attackHit.point);
    //        }
    //    }
    //    attackTimer = 0;
    //}

    public void AttackTurret()
    {
        attackRay.origin = transform.position;
        attackRay.direction = transform.forward;

        if (Physics.Raycast(attackRay, out attackHit))
        {
            if (turret.health.CurValue > 0)
            {
                turret.TakeDamage(attackDamage, attackHit.point);
            }
        }
        attackTimer = 0;
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