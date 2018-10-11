using System;
using System.Collections;
using System.Collections.Generic;

using Kristal;

using UnityEngine;
using UnityEngine.AI;

[Serializable]
public abstract class BaseCharacter : MonoBehaviour {

	public Color IsSelectedColor;

	public Color LeaderColor;

	public Color BaseColor;

	public List < BaseCharacter > characters;

	public List < GameObject > characterObjs;

	public NavMeshAgent agent;

	public GameObject obj;

	public Stat stats;

	public BaseCharacter leader;


	// Use this for initialization
	void Awake () {
        stats = gameObject.GetComponent<Stat>();
        obj = gameObject;
        agent = gameObject.GetComponent<NavMeshAgent>();

    }

    // Update is called once per frame
    void Update () {
		
	}

	public abstract void Remove( BaseCharacter chara );

	public void Damage( BaseCharacter _player_selected_companion ) { }

}
