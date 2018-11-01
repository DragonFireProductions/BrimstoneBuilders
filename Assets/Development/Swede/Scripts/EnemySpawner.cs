using Kristal;
using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private float minRange = 15;
    [SerializeField] private float maxRange = 25;
    [SerializeField] private float spawnRadius = 10;

    [SerializeField] private GameObject[] enemies;

    private float playerDistance;
    private bool isActive = false;

    // Use this for initialization
    private void Start()
    {

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
        for (int i = 0; i < enemies.Length; i++)
        {
            var newEnemy = Instantiate(enemies[i].gameObject);
            Enemy[] em = newEnemy.gameObject.GetComponentsInChildren<Enemy>();

            yield return new WaitForSeconds(2.0f);
            foreach (var variable in em)
            {
                Vector3 position = Random.insideUnitSphere * spawnRadius + this.gameObject.transform.position;
                position.y = 2;
                variable.transform.position = position;
                variable.GetComponent<EnemyNav>().location = this.gameObject;
            }
        }
    }

    private void Despawn()
    {
        for (int i = 0; i < enemies.Length; i++)
        {
            Destroy(enemies[i].gameObject);
        }
    }
}