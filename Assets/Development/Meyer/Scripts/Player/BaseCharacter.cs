using System;
using System.Collections;
using System.Collections.Generic;

using Assets.Meyer.TestScripts;

using Kristal;

using TMPro;

using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Assertions;

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

	public TextMeshPro damageText;

	public BaseNav Nav;

	public bool isCaught = false;

	public int damage = 0;

	public SpriteRenderer threat_signal;


	protected void Awake( ) {
        stats = gameObject.GetComponent<Stat>();
		Assert.IsNotNull(stats, "Stats not found on " + this.gameObject.name);
        obj = gameObject;
        agent = gameObject.GetComponent<NavMeshAgent>();
        material = gameObject.GetComponent<Renderer>().material;
		animator = gameObject.GetComponent < Animator >( );
		AnimationClass = gameObject.GetComponent< AnimationClass >( );
		damageText = transform.Find( "DamageText/Gamobj" ).GetComponent < TextMeshPro >( );
		
	}
	
    // Update is called once per frame
    protected void Update () {

	}
	
	public abstract void Damage( int _damage );


}
