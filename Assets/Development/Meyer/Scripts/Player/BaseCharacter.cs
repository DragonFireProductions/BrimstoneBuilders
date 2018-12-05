using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
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

	public SpriteRenderer threat_signal;

	public WeaponObject attachedWeapon;

	public BaseCharacter enemy;

	public List < BaseCharacter > attackers;

	public List < GameObject > damageNumbers;

	public GameObject canvas;

	public GameObject leftHand;

	public GameObject rightHand;

	public Projector projector;

	public float Level = 1;

	public string characterName;

	public bool critical = false;

	public Transform bulletPosition;


	public Rigidbody ridgidbody;
	public virtual void IncreaseLevel(float amount_f ) {
		Level += amount_f;
	}
	protected virtual void Awake( ) {
        stats = gameObject.GetComponent<Stat>();
		Assert.IsNotNull(stats, "Stats not found on " + this.gameObject.name);
        obj = gameObject;
        agent = gameObject.GetComponent<NavMeshAgent>();
        //material = gameObject.GetComponent<Renderer>().material;
		animator = gameObject.GetComponent < Animator >( );
		AnimationClass = gameObject.GetComponent< AnimationClass >( );
		attachedWeapon = transform.GetComponentInChildren < WeaponObject >( );
		attackers = new List < BaseCharacter >();
		canvas = transform.Find( "Canvas" ).gameObject;
		projector = gameObject.transform.Find( "Projector" ).GetComponent < Projector >( );
		projector.gameObject.SetActive(false);
		leftHand = transform.Find( "root/weaponShield_l" ).gameObject;
		rightHand = transform.Find( "root/weaponShield_r" ).gameObject;
		characterName = gameObject.name;
		bulletPosition = transform.Find( "BulletPosition" );
		ridgidbody = GetComponent < Rigidbody >( );
	}
    public virtual object this[string propertyName]
    {
        get
        {
            if (this.GetType().GetField(propertyName) != null)
            {
                return this.GetType().GetField(propertyName).GetValue(this);
            }

            return null;
        }
        set { this.GetType().GetField(propertyName).SetValue(this, value); }
    }

	public void ActivateWeapon( ) {
		attachedWeapon.Activate();
	}
    public void Freeze(float time)
    {
        float timer = time;
        StartCoroutine(FreezeC(timer));

    }
    private IEnumerator FreezeC(float timer)
    {
        while (timer > 0)
        {
            timer -= Time.deltaTime;
            Nav.SetState = BaseNav.state.FREEZE;

            yield return new WaitForEndOfFrame();
        }
        Nav.SetState = BaseNav.state.ATTACKING;
    }
    public IEnumerator EDOT(int damage, float interval, int _hits, WeaponObject item)
    {
        int hits = 0;
        while (hits < _hits)
        {
            Damage(damage, item);
            hits++;

	        if ( item.RunAwayOnUse ){
		        Nav.SetState = BaseNav.state.IDLE;
            }

            yield return new WaitForSeconds(interval);
        }

        Nav.SetState = BaseNav.state.ATTACKING;
    }

	public void KnockBack(float knockback ) {
		StartCoroutine( KnockBackC(knockback ) );
	}

	public IEnumerator KnockBackC(float knockback ) {
		Nav.Agent.enabled = false;
		Nav.enabled = false;
		ridgidbody.isKinematic = false;
		ridgidbody.AddForce(-transform.forward * (knockback * 2), ForceMode.Impulse);

		yield return new WaitForSeconds(0.4f);

		while ( Vector3.Distance(ridgidbody.velocity, new Vector3(0,0,0)) > 2){
			yield return new WaitForEndOfFrame( );
		}

		ridgidbody.isKinematic = true;
		Nav.enabled = true;
		Nav.Agent.enabled = true;

	}
    public void DOT(int damage, float interval, int hits, WeaponObject item)
    {
	    if ( damage <= 1 ){
		    damage = 1;
	    }
        StartCoroutine(EDOT(damage, interval, hits, item));

    }

	public abstract void Damage( int damage, BaseItems item );
    // Update is called once per frame
    protected void Update () {

	}


}
