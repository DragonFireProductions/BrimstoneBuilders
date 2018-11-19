using Kristal;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class companionSpawner : MonoBehaviour
{
    public bool DevTools;
    [SerializeField] bool useBrackets = true;

    [SerializeField] private Companion friends;

    public int index;
    public List<GameObject> comp;
    [SerializeField] private GameObject companionspawner;

    [ SerializeField ] private Companion[] companion;

    public int numberofcompanions;



    // Use this for initialization
    void Start()
    {
        comp = new List<GameObject>();

    }

    // Update is called once per frame
    void Update()
    {
        if (DevTools)
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
    }

    public IEnumerator CompSpawn()
    {
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
        }

        StaticManager.RealTime.Companions.RemoveAll(nulls => nulls == null);
        var newEnemy = Instantiate(companion[1]);
        newEnemy.name = newEnemy.name + index.ToString();
        Vector3 position = Random.insideUnitSphere * 5 + this.gameObject.transform.position;
        newEnemy.GetComponent<CompanionNav>().transform.position = gameObject.transform.position;
        comp.Add(newEnemy.gameObject);
        var location = GameObject.Find("panel_location");
        var newButton = Instantiate(Resources.Load<companionBehaviors>("Panel"));
        newEnemy.GetComponent<CompanionNav>().behaviors = newButton.GetComponent<companionBehaviors>();
        newButton.GetComponent<companionBehaviors>().newFriend = newEnemy;

        position.y = StaticManager.Character.gameObject.transform.position.y;
        StaticManager.particleManager.Play(ParticleManager.states.Spawn, position);
        newButton.transform.SetParent(location.transform, false);

        newButton.transform.position = location.transform.position;
        yield return new WaitForSeconds(1.0f);
        //variable.gameObject.SetActive(true);
        newEnemy.GetComponent<Companion>().Nav.Agent.Warp(position);
        newEnemy.GetComponent<CompanionNav>().transform.position = this.gameObject.transform.position;
        comp.Add(newEnemy.gameObject);
        StaticManager.RealTime.Companions.Add(newEnemy.GetComponent<Companion>());
        newButton.newFriend = newEnemy.GetComponent<Companion>();

    }


public void Kill()
    {
        if (comp.Count <= 0)
        {
            Debug.Log("no companions to despawn");
            index = 0;
        }
        else
        {
            StaticManager.RealTime.Companions.Remove(comp[0].GetComponent<Companion>());
            var gameObj = comp[0];
            comp.RemoveAt(0);
            Destroy(gameObj.GetComponent < CompanionNav >( ).behaviors.gameObject);
            StaticManager.inventories.Destroy(gameObj.GetComponent<PlayerInventory>());
            Destroy(gameObj);
            index--;
        }
    }
}
