using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drops : BaseCharacter
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
		if (stats.health <= 0)
        {
            keyPos = Random.insideUnitSphere * 2.5f + gameObject.transform.position;
            keyPos.y = StaticManager.Character.gameObject.transform.position.y;
            var newKey = Instantiate(drop);
            newKey.gameObject.transform.position = keyPos;
        }
	}
}
