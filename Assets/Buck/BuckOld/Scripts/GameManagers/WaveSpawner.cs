using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    [SerializeField]
    List<GameObject> enemyTypes;

    [SerializeField]
    List<Transform> spawnLocations;

    [SerializeField]
    List<GameObject> spawnedEnemies;

    DayNightCycle dayRef;

    [SerializeField]
    int spawnCurrency;

    [SerializeField]
    float spawnRate;

    float spawnTimer;

    bool nightComplete;

    public List<GameObject> GetEnemies()
    {
        return spawnedEnemies;
    }

    void Awake()
    {
        dayRef = FindObjectOfType<DayNightCycle>();
    }

    // Use this for initialization
    void Start()
    {
        SetNightSpawnCurrency();
    }

    // Update is called once per frame
    void Update()
    {
        if (!nightComplete)
        {
            spawnTimer += Time.deltaTime;

            if (dayRef.GetMeridiem() == DayNightCycle.Meridiem.PM)
            {
                if (dayRef.GetHour() == 8)
                {
                    if (dayRef.CanCycle())
                    {
                        dayRef.SetCycle(false);
                    }

                    if (spawnTimer >= spawnRate)
                    {
                        SpawnWave();
                    }
                }
            }
        }

        if (spawnCurrency <= 0)
        {
            EndNight();

            if (dayRef.GetMeridiem() == DayNightCycle.Meridiem.AM)
            {
                if (dayRef.GetHour() >= 6)
                {
                    SetNightSpawnCurrency();
                    dayRef.SetMinuteToSecond(1.0f);
                    nightComplete = false;
                }
            }
        }
    }

    void SetNightSpawnCurrency()
    {
        spawnCurrency = Mathf.RoundToInt(15 * dayRef.GetDaysPassed() * 0.75f);
    }

    void SpawnWave()
    {
        //Select a spawn location from the list of locations
        Transform currentSpawn = spawnLocations[Random.Range(0, spawnLocations.Count)];
        //Store the position of the chosen location
        Vector3 currentSpawnPosition = currentSpawn.position;
        //And its rotation
        Quaternion currentSpawnRotation = currentSpawn.rotation;
        //select an enemy from the list of enemies
        GameObject selectedEnemy = enemyTypes[Random.Range(0, enemyTypes.Count)];
        //Create the baddie
        Instantiate(selectedEnemy, currentSpawnPosition, currentSpawnRotation);

        spawnCurrency -= selectedEnemy.GetComponent<BaseEnemy>().spawnCost;

        if (spawnCurrency >= 0)
        {
            spawnedEnemies.Add(selectedEnemy);
        }

        spawnTimer = 0;
    }

    void EndNight()
    {
        nightComplete = true;

        //Need to figure out the for loop for this
        if (spawnedEnemies.Count == 0)
        {
            if (dayRef.GetMeridiem() == DayNightCycle.Meridiem.PM)
            {
                dayRef.SetCycle(true);
                dayRef.SetMinuteToSecond(60f);
            }
        }
    }
}
