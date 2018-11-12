using Kristal;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class companionSpawner : MonoBehaviour
{
    [SerializeField] bool useBrackets = true;

    [SerializeField] private Companion friends;

    public int index;
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
	    if (useBrackets)
	    {
	        if (Input.GetKeyDown("]"))
	        {
	            StartCoroutine(CompSpawn());
	        }
	        else if (Input.GetKeyDown("["))
	        {
	            Kill();
	        }
	    }
	    else
	    {

	    }
	}

    private IEnumerator CompSpawn() {
       // Debug.Log(index);
        if (index >= numberofcompanions)
        {
            Debug.Log("cant have anymore companions");
            index = numberofcompanions;
        }
        else
        {
            index++;
            StaticManager.RealTime.Companions.RemoveAll(nulls => nulls == null);
            var newEnemy = Instantiate(Resources.Load<Companion>("Companion"));
        newEnemy.name = newEnemy.name + index.ToString( );
            Vector3 position = Random.insideUnitSphere + this.gameObject.transform.position;
            newEnemy.GetComponent<CompanionNav>().transform.position = gameObject.transform.position;

            var newButton = Instantiate(Resources.Load<companionBehaviors>("Panel"));
            newEnemy.behaviors = newButton.GetComponent<companionBehaviors>();
            newButton.GetComponent<companionBehaviors>().newFriend = newEnemy;

            position.y = StaticManager.Character.gameObject.transform.position.y;
            StaticManager.particleManager.Play(ParticleManager.states.Spawn, position);
            yield return new WaitForSeconds(1.0f);
            //variable.gameObject.SetActive(true);
            newEnemy.GetComponent<Companion>().Nav.Agent.Warp(position);
            newEnemy.GetComponent<CompanionNav>().transform.position = this.gameObject.transform.position;
            comp.Add(newEnemy.gameObject);
            StaticManager.RealTime.Companions.Add(newEnemy.GetComponent<Companion>());
            StaticManager.RealTime.SetAttackCompanion();
            newButton.newFriend = newEnemy.GetComponent<Companion>();
        }
    }

    private void Kill() {
        if (index <= 0)
        {
            Debug.Log("no companions to despawn");
            index = 0;
        }
        else
        {
        StaticManager.RealTime.Companions.Remove(comp[0].GetComponent<Companion>());
        var gameObj = comp[ 0 ];
        comp.RemoveAt(0);
        StaticManager.inventories.Destroy(gameObj.GetComponent<PlayerInventory>());
            Destroy(gameObj);
        index--;
        }
    }
}
