using System.Collections;
using System.Collections.Generic;

using Assets.Meyer.TestScripts;
using Assets.Meyer.TestScripts.Player;

using Kristal;

using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Experimental.UIElements;

public class CompanionGroup : MonoBehaviour {

	[SerializeField] GameObject leader;

	[SerializeField] List < GameObject > group;

	public float[] strength;

	public float[] health;

	public int[] indexStrength;

	public int[] indexHealth;
	// Use this for initialization
	void Start () {
		leader = this.gameObject;

		Sort();
	}

	public List < GameObject > Group {
		get { return group; }
		set { group = value; }
	}

	public void Add( GameObject ad ) {
		group.Add( ad );
	}
	public void StartBattle( ) {
		TurnBased.Instance.lineUp(leader, group);
		CharacterUtility.instance.EnableObstacle(Character.player);
		Character.player.GetComponent < PlayerController >( ).enabled = false;
		CameraController.controller.SwitchMode(CameraMode.Battle);

	}
    // Update is called once per frame
    void Update () {
        if (Input.GetKeyDown(KeyCode.H) && !TurnBased.Instance.AttackMode)
        {
        }

		if ( Input.GetKeyDown(KeyCode.Y) && TurnBased.Instance.AttackMode ){
			TurnBased.Instance.AttackMode = false;

		}
    }

	public void Remove(GameObject obj ) {
        for (int i = 0; i < group.Count; i++){
	        if ( group[ i ] == obj )
		        group.RemoveAt( i );
        }
		Destroy( obj );
	}

	public void Sort( ) {
        strength = new float[group.Count + 1];
        health = new float[@group.Count + 1];
        indexStrength = new int[group.Count + 1];
        indexHealth = new int[@group.Count + 1];
        int i = 0;

        for (int j = 0; j < @group.Count; j++)
        {
            strength[i] = @group[j].GetComponent<Stat>().Strength;
            health[i] = @group[j].GetComponent<Stat>().Health;

            i++;
        }
        @group.Add(leader);

        for (int j = 0; j < @group.Count; j++)
        {
            indexStrength[j] = j;
            indexHealth[j] = j;
        }
		

        CharacterUtility.instance.quickSort(strength, 0, strength.Length - 1, indexStrength);
        CharacterUtility.instance.quickSort(health, 0, health.Length - 1, indexHealth);
    }
}
