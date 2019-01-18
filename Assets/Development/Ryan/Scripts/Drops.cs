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
        if ( _ene.questItem.quest is KeyQuest ){
            var quest = (KeyQuest)_ene.questItem.quest;
            quest.DropLoot(_ene.questItem, _ene);
        }
    }
}
