using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretTest : MonoBehaviour
{

    EnemyTargetManager enemyTM;
	// Use this for initialization
	void Start ()
    {
        enemyTM = FindObjectOfType<EnemyTargetManager>();
        enemyTM.AddTarget(transform);
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Destroy(gameObject);
        }
	}

    void OnDestroy()
    {
        if (enemyTM != null)
        {
            Debug.Log("Being fuck all uselass");
            enemyTM.RemoveTarget(transform);
        }
    }
}
