using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 using Kristal;

public class Drops : MonoBehaviour
{
    [SerializeField] GameObject drop;

    public int coinWorth;
    private float dir;
    private float TurnSpeed;
    private bool turn;

    private Vector3 keyPos;
    // Use this for initialization
    void Start()
    {
        TurnSpeed = Random.Range(0.5f, 2.0f); //Randomizes the turn speed of each coin.
        dir = Random.Range(0.0f, 100.0f); //Randomizes the direction each coin spins.

        if ((int)dir % 2 == 0)
        {
            turn = true;
        }
        else
            turn = false;
       
    }
	
	// Update is called once per frame
	void Update()
    {
	}

   

    public void Drop_Loot(Enemy _ene)
    {
        if (_ene.stats.health <= 0)
        {
            _ene.transform.position = Random.insideUnitSphere * 2.5f + gameObject.transform.position;
            //a_ene.transform.position.y = StaticManager.Character.gameObject.transform.position.y;
            var newKey = Instantiate(_ene.key);
            newKey.gameObject.transform.position = _ene.transform.position;
        }
    }
}
