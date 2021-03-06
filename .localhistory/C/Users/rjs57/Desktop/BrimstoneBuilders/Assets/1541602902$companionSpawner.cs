﻿using Kristal;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class companionSpawner : MonoBehaviour
{

    [SerializeField] private Companion friends;
    private List<GameObject> comp;
    [SerializeField] private GameObject companionspawner;

    public int numberofcompanions;
    // Use this for initialization
    void Start()
	{
        comp = new List<GameObject>();
	}

	// Update is called once per frame
	void Update()
	{
        if (Input.GetKeyDown("]"))
        {
            StartCoroutine(CompSpawn());


        }
        else if (Input.GetKeyDown("["))
        {
            Kill();
            //comp.RemoveAt(comp.Count);
            //i = comp.Length - 1;
            //Debug.Log("comp length after subtracting a companion:" + i);
            //Debug.Log(i);
            //Destroy(comp[i].gameObject);
            //Destroy(comp[i]);
            //--i;
        }
    }

    private IEnumerator CompSpawn()
    {
        StaticManager.RealTime.Enemies.RemoveAll(nulls => nulls == null);
        for (int i = 0; i < numberofcompanions; i++)
        {
            var newEnemy = Instantiate(Resources.Load<GameObject>("EnemyLeader"));
            Vector3 position = Random.insideUnitSphere + this.gameObject.transform.position;
            newEnemy.GetComponent<EnemyNav>().location = gameObject;

            position.y = StaticManager.Character.gameObject.transform.position.y;
            StaticManager.particleManager.Play(ParticleManager.states.Spawn, position);
            yield return new WaitForSeconds(1.0f);
            //variable.gameObject.SetActive(true);
            newEnemy.GetComponent<Enemy>().Nav.Agent.Warp(position);
            newEnemy.GetComponent<EnemyNav>().location = this.gameObject;
            comp.Add(newEnemy.gameObject);
            StaticManager.RealTime.Enemies.Add(newEnemy.GetComponent<Enemy>());
            StaticManager.RealTime.SetAttackEnemies();
        }
    }

    private void Kill()
    {
        for (int i = 0; i < comp.Count; i++)
        {
            Destroy(comp[i]);
        }
        gameObject.SetActive(false);
    }
}
