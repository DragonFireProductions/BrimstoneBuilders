using System;
using System.Collections;
using System.Collections.Generic;

using Assets.Meyer.TestScripts;

using Kristal;

using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Assertions;

[Serializable]
public abstract class BaseCharacter : MonoBehaviour {

	public Color IsChosenBySelf;

	public Color LeaderColor;

	public Color BaseColor;

	public Color IsChosenByEnemy;

	public List < BaseCharacter > characters;

	public List < GameObject > characterObjs;

	public NavMeshAgent agent;

	public GameObject obj;

	public Stat stats;

	public BaseCharacter leader;

	public Material material;

	public Animator animator;

	public AnimationClass AnimationClass;

	protected void Awake( ) {
        stats = gameObject.GetComponent<Stat>();
		Assert.IsNotNull(stats, "Stats not found on " + this.gameObject.name);
        obj = gameObject;
        agent = gameObject.GetComponent<NavMeshAgent>();
        material = gameObject.GetComponent<Renderer>().material;
		animator = gameObject.GetComponent < Animator >( );
		AnimationClass = gameObject.AddComponent < AnimationClass >( );
	}
	

    // Update is called once per frame
    void Update () {
		
	}

	public abstract void Remove( BaseCharacter chara );

	public void Damage( BaseCharacter _player_selected_companion ) { }

}
