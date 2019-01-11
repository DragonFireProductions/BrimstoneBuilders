using System;

using Kristal;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    //Variables:
    //minRange is the distance the player has to be from the spawner in order to spawn in the enemies.
    //maxRange '' in order to despawn the enemies.
    //spawnRadius is the radius from the origin of the spawner that the enemies will be randomly spawned within.
    [SerializeField] private float minRange = 15;
    [SerializeField] private float maxRange = 25;
    [SerializeField] private float spawnRadius = 10;

    
   [Serializable] public struct EnemyStruct
    {

        public Enemy enemy;

        public int Damage;

        public float luck;

        public GameObject weapon;


    }
    [SerializeField]
    public EnemyStruct[] enemies;



    private List<GameObject> instantiated;
    private float playerDistance;
    private bool isActive = false;

    public int numberofEnemies;

    private void Start()
    {
        instantiated = new List<GameObject>();
    }


    private void Update()
    {
        //Constantly sets the distance from the player to the spawner for checks.
        Vector3 character = new Vector3(StaticManager.Character.transform.position.x, 0, StaticManager.Character.transform.position.z);

        playerDistance =
            Vector3.Distance(StaticManager.Character.transform.position, this.gameObject.transform.position);
        //If the player is within the minRange of the spawner, spawns enemies.
        if (playerDistance <= minRange && isActive == false)
        {
            Spawn();
            isActive = true;
        }
        //If the player goes outside the maxRange, the instantiated enemies will despawn.
        else if (playerDistance > maxRange && isActive == true && !StaticManager.UiInventory.ItemsInstance.windowIsOpen)
        {
            Despawn();
            isActive = false;
        }
    }

    private void Spawn()
    {
        StaticManager.RealTime.Enemies.RemoveAll(nulls => nulls == null);

        for (int i = 0; i < numberofEnemies; i++)
        {
            Vector3 position = Random.insideUnitSphere * spawnRadius + this.gameObject.transform.position;
            var random = Random.Range( 0 , enemies.Length );
            var newEnemy = Instantiate(enemies[random].enemy.gameObject, position, Quaternion.identity);
            
            //Randomizes the spawn position (within the set range) of the current enemy.
            newEnemy.GetComponent<EnemyNav>().location = gameObject;

            //Adjust for y to properly place on nav mesh.
            position.y = StaticManager.Character.gameObject.transform.position.y;
            //Plays the spawn animation

            StaticManager.particleManager.Play(ParticleManager.states.Spawn, position);
            newEnemy.GetComponent<EnemyNav>().location = this.gameObject;
            
            //warps the enemy to the position ^
            newEnemy.GetComponent<Enemy>().Nav.Agent.Warp(position);
            //adds the enemy to the list of instantiated enemies.
            instantiated.Add(newEnemy.gameObject);
           

            newEnemy.GetComponent < Enemy >( ).startWeapon = Instantiate(enemies[ random ].weapon);

            newEnemy.GetComponent<Enemy>().startWeapon.GetComponent<WeaponObject>().PickUp(newEnemy.GetComponent<Enemy>());
            newEnemy.GetComponent<Enemy>().startWeapon.GetComponent<WeaponObject>().Attach(newEnemy.GetComponent<Enemy>());
            StaticManager.RealTime.Enemies.Add(newEnemy.GetComponent<Enemy>());
            newEnemy.GetComponent < Enemy >( ).damage = enemies[ random ].Damage;
            newEnemy.GetComponent < Stat >( ).luck = enemies[ random ].luck;
            StaticManager.RealTime.SetAttackEnemies();
        }

    }

    private void Despawn() {
        instantiated.RemoveAll( item => item == null );
        foreach ( var l_t in instantiated ){
            Destroy(l_t);
        }
        gameObject.SetActive(false);
    }
}