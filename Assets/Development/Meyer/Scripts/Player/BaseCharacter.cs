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

	public bool isBlocking;

	public BaseNav Nav;
	



	protected void Awake( ) {
        stats = gameObject.GetComponent<Stat>();
		Assert.IsNotNull(stats, "Stats not found on " + this.gameObject.name);
        obj = gameObject;
        agent = gameObject.GetComponent<NavMeshAgent>();
        material = gameObject.GetComponent<Renderer>().material;
		animator = gameObject.GetComponent < Animator >( );
		AnimationClass = gameObject.AddComponent < AnimationClass >( );
		damageText = transform.Find( "DamageText" ).GetComponentInChildren< TextMeshPro >( );
	}
	

    // Update is called once per frame
    void Update () {
		
	}

	public abstract void RegenerateAttackPoints(bool betweenrounds );
	public void DamageDone(int damage, BaseCharacter gameObject ) {
		damageText.enabled = true;

        damageText.transform.position = gameObject.transform.position + ( gameObject.transform.up * 4 );
		damageText.text = damage.ToString( );
		damageText.transform.parent = this.transform;
		damageText.GetComponentInChildren < Animation >( ).Play( );
		StartCoroutine( deleteDamages( damageText ) );
	}

	IEnumerator deleteDamages(TextMeshPro instantiated ) {
		yield return new WaitForSeconds(4);
		AnimationClass.Stop(AnimationClass.states.DamageText);
		damageText.enabled = false;
	}

	public abstract void Damage( BaseCharacter _player_selected_companion );

}
