using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDrone : MonoBehaviour
{
    [Header("DroneSettings")]

    [SerializeField]
    float fireRate;

    float fireTimer;

    [SerializeField]
    float range;

    [SerializeField]
    int damagePerShot;

    [SerializeField]
    float moveSpeed;

    [Header("UnitySettings")]

    [SerializeField]
    Vector3 offset;

    [SerializeField]
    float rotateSpeed;

    [SerializeField]
    float effectsDisplayTime;

    [SerializeField]
    Transform partToRotate;

    [SerializeField]
    Transform firePoint;

    [SerializeField]
    Transform drone;

    [SerializeField]
    Transform droneSlot;

    Transform target;

    [SerializeField]
    LineRenderer droneLR;

    [SerializeField]
    Light droneLight;

    SphereCollider droneRange;

    [SerializeField]
    string enemytag = "Enemy";

    Ray shootRay;

    RaycastHit shootHit;

    Rigidbody droneRB;

    int shootableMask;

    int floorMask;

    float camRayLength = 100f;

    void Awake()
    {
        shootableMask = LayerMask.GetMask("Shootable");
        floorMask = LayerMask.GetMask("Floor");
        droneRB = GetComponent<Rigidbody>();
        droneLR = GetComponentInChildren<LineRenderer>();
        droneLight = GetComponentInChildren<Light>();
        droneRange = GetComponent<SphereCollider>();

        droneRange.radius = range;
    }
	// Update is called once per frame
	void Update ()
    {
        fireTimer += Time.deltaTime;

        UpdateDroneTarget();

        LockOnTarget();

        if (target == null)
        {
            MoveToPlayer();
        }
        else
        {
            MoveToEnemy();
        }

        if (fireTimer >= fireRate && target != null)
        {
            Fire();
        }
        if (fireTimer >= fireRate * effectsDisplayTime)
        {
            DisableEffects();
        }
	}

    void FixedUpdate()
    {
        DroneTurning();
    }

    void UpdateDroneTarget()
    {
        //Find the tag named enemy tag of all the objects within the
        //Array of GameObjects
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemytag);

        float shortestDistance = Mathf.Infinity;

        GameObject nearestEnemy = null;

        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);

            if (distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }

        if (nearestEnemy != null && shortestDistance <= range)
        {
            target = nearestEnemy.transform;
        }
        else
        {
            target = null;
        }
    }

    void LockOnTarget()
    {

        if (target != null)
        {
            //Turn from current transform, to the targets transform
            Vector3 dir = target.position - transform.position;

            Quaternion lookRotation = Quaternion.LookRotation(dir);

            //This is so that if an enemy exits the turrets range and another comes in
            //It doesn't snap right to the next target
            Vector3 rotation = Quaternion.Lerp(partToRotate.rotation, lookRotation, Time.deltaTime * rotateSpeed).eulerAngles;

            //The pivot point turns on the y rotation only 
            partToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f);
        }
        else
        {
            return;
        }
    }

    void Fire()
    {
        fireTimer = 0;

        firePoint.LookAt(target);
        drone.LookAt(target);

        droneLR.enabled = true;
        droneLight.enabled = true;

        droneLR.SetPosition(0, firePoint.position);

        shootRay.origin = firePoint.position;
        shootRay.direction = firePoint.forward;

        if (Physics.Raycast(shootRay, out shootHit, range, shootableMask))
        {
            BaseEnemy enemyHealth = shootHit.collider.GetComponent<BaseEnemy>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(damagePerShot, shootHit.point);
            }
            droneLR.SetPosition(1, shootHit.point);
        }
        else
        {
            droneLR.SetPosition(1, shootRay.origin + shootRay.direction * range);
        }
    }

    void DroneTurning()
    {
        if (Vector3.Distance(droneSlot.position, transform.position) <= 2f)
        {
            Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit floorHit;

            if (Physics.Raycast(camRay, out floorHit, camRayLength, floorMask))
            {
                Vector3 droneToMouse = floorHit.point - transform.position;
                droneToMouse.y = 0f;

                Quaternion newRotation = Quaternion.LookRotation(droneToMouse);
                droneRB.MoveRotation(newRotation);
            }
        }
    }

    void MoveToPlayer()
    {
        transform.position = Vector3.MoveTowards(transform.position, droneSlot.position, moveSpeed * Time.deltaTime);

    }

    void MoveToEnemy()
    {
        drone.LookAt(target);
        transform.position = Vector3.MoveTowards(transform.position, target.position + offset, moveSpeed * Time.deltaTime);
    }

    void DisableEffects()
    {
        droneLR.enabled = false;
        droneLight.enabled = false;
    }
}
