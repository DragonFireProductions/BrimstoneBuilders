using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class companionSpawner : MonoBehaviour
{

    [SerializeField] private Companion friends;
    private List<Companion> comp;
    [SerializeField] private GameObject companionspawner;
    // Use this for initialization
    void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{
	    if (Input.GetKeyDown("]"))
	    {
	        //StartCoroutine(CompanionSpawn());
	        var newEnemy = Instantiate(friends);
	        newEnemy.transform.position = companionspawner.transform.position;
	        comp.Add(newEnemy);
	        //i = comp.Count;
	        //Debug.Log("comp length after adding a companion:" + i);
	        foreach (var companion in comp)
	        {
	            Vector3 pos = Random.insideUnitSphere + companionspawner.transform.position;
	            companion.Nav.Agent.Warp(pos);
	        }
	        //Instantiate(friends[i].gameObject);

	    }
	    else if (Input.GetKeyDown("["))
	    {
	        comp.RemoveAt(comp.Count);
	        //i = comp.Length - 1;
	        //Debug.Log("comp length after subtracting a companion:" + i);
	        //Debug.Log(i);
	        //Destroy(comp[i].gameObject);
	        //Destroy(comp[i]);
	        //--i;
	    }
    }
}
