using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Mail;

using Assets.Meyer.TestScripts;

using Kristal;

using TMPro;

using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Assertions;
using UnityEngine.Playables;

using Random = System.Random;

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

	public BaseNav Nav;

	public int damage = 0;

	public SpriteRenderer threat_signal;

	public WeaponObject attachedWeapon;

	public BaseCharacter enemy;

	public List < BaseCharacter > attackers;

	public List < GameObject > damageNumbers;

	public GameObject canvas;

	public GameObject cube;

	public Projector projector;
	protected void Awake( ) {
        stats = gameObject.GetComponent<Stat>();
		Assert.IsNotNull(stats, "Stats not found on " + this.gameObject.name);
        obj = gameObject;
        agent = gameObject.GetComponent<NavMeshAgent>();
        material = gameObject.GetComponent<Renderer>().material;
		animator = gameObject.GetComponent < Animator >( );
		AnimationClass = gameObject.GetComponent< AnimationClass >( );
		attachedWeapon = transform.GetComponentInChildren < WeaponObject >( );
		attackers = new List < BaseCharacter >();
		canvas = transform.Find( "Canvas" ).gameObject;
		projector = gameObject.transform.Find( "Projector" ).GetComponent < Projector >( );
		projector.gameObject.SetActive(false);
	}

	public abstract void Attack( BaseCharacter attacker );

    // Update is called once per frame
    protected void Update () {

	}
	

}
