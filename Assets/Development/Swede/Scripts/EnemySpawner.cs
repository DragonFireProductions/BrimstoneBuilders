using Kristal;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemySpawner : MonoBehaviour
{
    //Variables:
    //minRange is the distance the player has to be from the spawner in order to spawn in the enemies.
    //maxRange '' in order to despawn the enemies.
    //spawnRadius is the radius from the origin of the spawner that the enemies will be randomly spawned within.
    [SerializeField] private float minRange = 15;
    [SerializeField] private float maxRange = 25;
    [SerializeField] private float spawnRadius = 10;

    public float Strength;

    [SerializeField] private Enemy[] enemies;

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
        playerDistance =
            Vector3.Distance(StaticManager.Character.transform.position, this.gameObject.transform.position);
        //If the player is within the minRange of the spawner, spawns enemies.
        if (playerDistance <= minRange && isActive == false)
        {
            StartCoroutine(Spawn());
            isActive = true;
        }
        //If the player goes outside the maxRange, the instantiated enemies will despawn.
        else if (playerDistance > maxRange && isActive == true && !StaticManager.UiInventory.ItemsInstance.windowIsOpen)
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
            Vector3 position = Random.insideUnitSphere * spawnRadius + this.gameObject.transform.position;

            var newEnemy = Instantiate(enemies[Random.Range(0, enemies.Length)], position, Quaternion.identity);

            newEnemy.GetComponent < Enemy >( ).stats.Strength = Strength;
            newEnemy.GetComponent<Enemy>().stats.UpdateStrength();
            //Randomizes the spawn position (within the set range) of the current enemy.
            newEnemy.GetComponent<EnemyNav>().location = gameObject;
            //Adjust for y to properly place on nav mesh.
            position.y = StaticManager.Character.gameObject.transform.position.y;
            //Plays the spawn animation
            StaticManager.particleManager.Play(ParticleManager.states.Spawn, position);
            newEnemy.GetComponent<EnemyNav>().location = this.gameObject;

            yield return new WaitForSeconds(1.0f);

            
            //warps the enemy to the position ^
            newEnemy.GetComponent<Enemy>().Nav.Agent.Warp(position);
            //adds the enemy to the list of instantiated enemies.
            instantiated.Add(newEnemy.gameObject);
            StaticManager.RealTime.Enemies.Add(newEnemy.GetComponent<Enemy>());
            StaticManager.RealTime.SetAttackEnemies();
        }

    }

    private void Despawn() {
        instantiated.RemoveAll( item => item == null );
        for (int i = 0; i < instantiated.Count; i++){
            Destroy(instantiated[i]);
        }
        gameObject.SetActive(false);
    }
}