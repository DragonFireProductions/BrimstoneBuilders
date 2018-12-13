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

using Random = UnityEngine.Random;

[Serializable]
public abstract class BaseCharacter : MonoBehaviour {
	
	public NavMeshAgent agent;

	public GameObject obj;

	public Stat stats;

	public Animator animator;

	public AnimationClass AnimationClass;

	public BaseNav Nav;

	public SpriteRenderer threat_signal;

	[HideInInspector] public WeaponObject attachedWeapon;

	[HideInInspector] public BaseCharacter enemy;

	[HideInInspector] public List < BaseCharacter > attackers;

	public GameObject canvas;

	public GameObject leftHand;

	public GameObject rightHand;

	public Projector projector;

	public string characterName;

	public Transform bulletPosition;

	public Rigidbody ridgidbody;

	public GameObject startWeapon;
	
	protected virtual void Awake( ) {
        obj = gameObject;
		attackers = new List < BaseCharacter >();
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

	public void DeactivateWeapon( ) {
		attachedWeapon.Deactivate( );
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
        while (hits < _hits){
	        item.AttachedCharacter.stats.health -= damage;

	        if ( item.AttachedCharacter.stats.health <= 0 ){
		        Destroy(item.AttachedCharacter.gameObject);
	        }
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
		//Nav.Agent.enabled = false;
		//Nav.enabled = false;
		ridgidbody.isKinematic = false;
		ridgidbody.AddForce(-transform.forward * (knockback * 2), ForceMode.Impulse);

		yield return new WaitForSeconds(0.4f);

		while ( Vector3.Distance(ridgidbody.velocity, new Vector3(0,0,0)) > 2){
			yield return new WaitForEndOfFrame( );
		}

		ridgidbody.isKinematic = true;
		//Nav.enabled = true;
		//Nav.Agent.enabled = true;

	}
    public void DOT(int damage, float interval, int hits, WeaponObject item)
    {
	    if ( damage <= 1 ){
		    damage = 1;
	    }
        StartCoroutine(EDOT(damage, interval, hits, item));

    }
    void IncreaseCritChance(float critInc)
    {
        stats.luck += critInc;

        //Never let the crit chance go out of range
        if (stats.luck > 100.0f){
	        stats.luck = 60;
        }
    }
    public virtual void Damage(int _damage, BaseItems item)
    {
       float randValue = Random.Range(1, 100 );
       if (randValue > (100 - item.AttachedCharacter.stats.luck) )
       {
            var damage = _damage * 2;
            stats.Health -= damage;
            InstatiateFloatingText.InstantiateFloatingCriticalText(damage.ToString(), Color.yellow, this );
		   IncreaseCritChance(1);
        }
       else if (randValue < (100 - item.AttachedCharacter.stats.luck)* 0.25){
	       InstatiateFloatingText.InstantiateFloatingMissText("MISS", Color.gray, this);
        }
		else {
            var damage = _damage;
            stats.Health -= damage;
            InstatiateFloatingText.InstantiateFloatingText(damage.ToString(), Color.white, this);   
        }
        if (item.AttachedCharacter.stats.health <= 0)
        {
            Destroy(item.AttachedCharacter.gameObject);
        }
    }
    // Update is called once per frame
    protected void Update () {

	}


}
