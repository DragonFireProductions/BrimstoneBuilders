using Kristal;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private float minRange = 15;
    [SerializeField] private float maxRange = 25;
    [SerializeField] private float spawnRadius = 10;

    [SerializeField] private Enemy[] enemies;

    private List < GameObject > instantiated;
    private float playerDistance;
    private bool isActive = false;

    public int numberofEnemies;
    // Use this for initialization
    private void Start()
    {
        instantiated = new List < GameObject >();
    }


    // Update is called once per frame
    private void Update()
    {
        playerDistance =
            Vector3.Distance(StaticManager.Character.transform.position, this.gameObject.transform.position);
        if (playerDistance <= minRange && isActive == false)
        {
            StartCoroutine(Spawn());
            isActive = true;
        }
        else if (playerDistance > maxRange && isActive == true)
        {
            Despawn();
            isActive = false;
        }
    }

    private IEnumerator Spawn()
    {
        StaticManager.RealTime.Enemies.RemoveAll(nulls => nulls == null);
        for (int i = 0; i < numberofEnemies; i++)
        {
            var newEnemy = Instantiate(Resources.Load<GameObject>("EnemyLeader"));
                Vector3 position = Random.insideUnitSphere * spawnRadius + this.gameObject.transform.position;
            newEnemy.GetComponent < EnemyNav >( ).location = gameObject;

                position.y = StaticManager.Character.gameObject.transform.position.y;
                StaticManager.particleManager.Play(ParticleManager.states.Spawn, position);
                yield return new WaitForSeconds(1.0f);
                //variable.gameObject.SetActive(true);
                newEnemy.GetComponent<Enemy>().Nav.Agent.Warp(position);
                newEnemy.GetComponent<EnemyNav>().location = this.gameObject;
                instantiated.Add(newEnemy.gameObject);
            StaticManager.RealTime.Enemies.Add(newEnemy.GetComponent<Enemy>());
            StaticManager.RealTime.SetAttackEnemies();
        }

    }

    private void Despawn() {
       
        for (int i = 0; i < instantiated.Count; i++){
            Destroy(instantiated[i]);
        }
        gameObject.SetActive(false);
    }
}