using Kristal;
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
        }
    }

    private IEnumerator CompSpawn()
    {
        StaticManager.RealTime.Companions.RemoveAll(nulls => nulls == null);
            var newEnemy = Instantiate(Resources.Load<Companion>("Companion"));
            Vector3 position = Random.insideUnitSphere + this.gameObject.transform.position;
            newEnemy.GetComponent<CompanionNav>().transform.position = gameObject.transform.position;

            position.y = StaticManager.Character.gameObject.transform.position.y;
            StaticManager.particleManager.Play(ParticleManager.states.Spawn, position);
            yield return new WaitForSeconds(1.0f);
            //variable.gameObject.SetActive(true);
            newEnemy.GetComponent<Companion>().Nav.Agent.Warp(position);
            newEnemy.GetComponent<CompanionNav>().transform.position = this.gameObject.transform.position;
            comp.Add(newEnemy.gameObject);
            StaticManager.RealTime.Companions.Add(newEnemy.GetComponent<Companion>());
            StaticManager.RealTime.SetAttackCompanion();
    }

    private void Kill()
    {
            StaticManager.RealTime.Companions.Remove(comp[0].GetComponent<Companion>());
        var gameObj = comp[ 0 ];
        comp.RemoveAt(0);

            Destroy(gameObj);
    }
}
