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
         var vec = new Vector3(_ene.transform.position.x, -94, _ene.transform.position.z);
        if (_ene.dropKey && _ene.quest && _ene.quest is KeyQuest ){
            var quest = (KeyQuest)_ene.quest;
            quest.DropLoot(quest.key, _ene);
        }
        if ( _ene.DropWeapon ){
            var a = Instantiate( _ene.attachedWeapon.gameObject );

            a.SetActive(true);
           
            a.transform.position = vec;
            a.tag = "PickUp";
            a.GetComponent < WeaponObject >( ).leftHand = Instantiate( _ene.attachedWeapon.leftHand.gameObject );
            a.GetComponent < WeaponObject >( ).rightHand = Instantiate( _ene.attachedWeapon.rightHand.gameObject );
            a.GetComponent < Collider >( ).enabled = true;
            a.GetComponent<WeaponObject>().mesh.SetActive(true);
        }
         if ( _ene.objectToDrop ){
            var objects = Instantiate( _ene.objectToDrop );
            objects.transform.position = vec;
        }
    }
}
