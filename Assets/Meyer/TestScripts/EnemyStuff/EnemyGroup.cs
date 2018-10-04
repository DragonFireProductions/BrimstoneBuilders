using System.Collections;
using System.Collections.Generic;

using Assets.Meyer.TestScripts;
using Assets.Meyer.TestScripts.Player;

using Kristal;

using TMPro;

using UnityEngine;
using UnityEngine.AI;

public class EnemyGroup : MonoBehaviour {

	// Use this for initialization

	private GameObject leader;

	public List < GameObject > Groupies;

	void Start () {
		leader = this.gameObject;

		foreach ( var VARIABLE in Groupies ){
			VARIABLE.GetComponent < Enemy >( ).Leader = leader;
		}
	}

	public void StartBattle( ) {
		TurnBased.Instance.EnemyList = Groupies;
        TurnBased.Instance.AttackMode = true;
		CameraController.controller.Mode = CameraMode.Battle;

		GameObject[] obj = GameObject.FindGameObjectsWithTag( "Guard" );

   for (int i = 0; i < obj.Length; i++){
	     obj[ i ].GetComponent < Guard >( ).enabled = false;
       if (Vector3.Distance(obj[i].transform.position, Character.player.transform.position) < 20){
	          obj[ i ].GetComponent<NavMeshAgent>().destination = obj[ i ].transform.forward + (Vector3.forward * 30 );
         }
    }
        foreach (var VARIABLE in Groupies)
        {
            VARIABLE.GetComponent<EnemyNav>().SetState = EnemyState.Attacking;
        }
        StartCoroutine(TurnBased.Instance.lineUpLeader(leader, Groupies));
		Character.player.GetComponent<CompanionGroup>().StartBattle();
	}
    public void Remove(GameObject obj)
    {
        for (int i = 0; i < Groupies.Count; i++)
        {
            if (Groupies[i] == obj)
                Groupies.RemoveAt(i);
        }
	    Destroy( obj );
    }

	void NewLeader(GameObject OldLeader ) {
		int index = 0;
        for (int i = 0; i < Groupies.Count; i++)
        {
            if (Groupies[i] == OldLeader){
	            index = i;
            }
        }
		
	}
    // Update is called once per frame
    void Update () {
		if ( Input.GetKeyDown(KeyCode.H ) && !TurnBased.Instance.AttackMode){
			TurnBased.Instance.AttackMode = true;
            foreach (var VARIABLE in Groupies){
	            VARIABLE.GetComponent < EnemyNav >( ).SetState = EnemyState.Attacking;
            }
            StartCoroutine(TurnBased.Instance.lineUpLeader(leader, Groupies ));
		}

		if ( Input.GetKeyDown(KeyCode.Y)){
            foreach (var VARIABLE in Groupies){
	            TurnBased.Instance.AttackMode = false;
	            CharacterUtility.instance.EnableObstacle(VARIABLE, true);
                VARIABLE.GetComponent<EnemyNav>().SetState = EnemyState.Idle;
            }
        }
	}
}
