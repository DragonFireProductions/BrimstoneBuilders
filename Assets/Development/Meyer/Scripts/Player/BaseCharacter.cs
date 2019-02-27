using System;
using System.Collections;
using System.Collections.Generic;

using Assets.Meyer.TestScripts.Player;

using Kristal;

using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

using Random = UnityEngine.Random;

[ Serializable ]
public abstract class BaseCharacter : MonoBehaviour {

    public NavMeshAgent agent;

    public GameObject obj;

    public Stat stats;

    public Animator animator;

    public AnimationClass AnimationClass;

    public BaseNav Nav;

    public SpriteRenderer threat_signal;

    [ HideInInspector ] public WeaponObject attachedWeapon;

    [ HideInInspector ] public BaseCharacter enemy;

    /*[ HideInInspector ]*/ public List < BaseCharacter > attackers;

    public GameObject canvas;

    public GameObject leftHand;

    public GameObject rightHand;

    public Projector projector;

    public string characterName;

    public Transform bulletPosition;

    public Rigidbody ridgidbody;

    public GameObject startWeapon;

    public RawImage icon;

    public GameObject light;

    public float speed;

    [HideInInspector] AudioSource audio;

    public AudioClip[] clips;
    protected virtual void Awake( ) {
        obj       = gameObject;
        speed = GetComponent < NavMeshAgent >( ).speed;
        attackers = new List < BaseCharacter >( );
        audio = gameObject.AddComponent<AudioSource>();
        audio.playOnAwake = false;
        audio.spatialBlend = 1.0f;
    }

    public virtual object this[ string propertyName ] {
        get {
            if ( GetType( ).GetField( propertyName ) != null ){
                return GetType( ).GetField( propertyName ).GetValue( this );
            }

            return null;
        }
        set { GetType( ).GetField( propertyName ).SetValue( this , value ); }
    }

    public void ActivateWeapon( ) {
        attachedWeapon.Activate( );
    }

    public void DeactivateWeapon( ) {
        attachedWeapon.Deactivate( );
    }

    public void Freeze( float time ) {
        var timer = time;
        StartCoroutine( FreezeC( timer ) );
    }

    private IEnumerator FreezeC( float timer ) {
        while ( timer > 0 ){
            timer        -= Time.deltaTime;
            Nav.SetState =  BaseNav.state.FREEZE;

            yield return new WaitForEndOfFrame( );
        }

        Nav.SetState = BaseNav.state.ATTACKING;
    }

    public IEnumerator EDOT( int damage , float interval , int _hits , WeaponObject item ) {
        var hits = 0;

        while ( hits < _hits ){
            if ( this is Character ){
                gameObject.GetComponent<Character>().Damage(damage, item);
            }
            else if ( this is Enemy ){
                gameObject.GetComponent<Enemy>().Damage(damage, item);
            }
            else if ( this is Companion ){
                gameObject.GetComponent<Companion>().Damage(damage, item);
            }


            hits++;

            if ( item.RunAwayOnUse ){
                Nav.SetState = BaseNav.state.IDLE;
            }

            yield return new WaitForSeconds( interval );
        }

        Nav.SetState = BaseNav.state.ATTACKING;
    }

    public void KnockBack( float knockback ) {
        StartCoroutine( KnockBackC( knockback ) );
    }

    public IEnumerator KnockBackC( float knockback ) {
        animator.enabled = false;
        ridgidbody.isKinematic = false;
        ridgidbody.AddForce( -transform.forward * ( knockback * 10f) , ForceMode.Impulse );
        agent.speed = 2;
        yield return new WaitForSeconds( 0.5f );

        while ( Vector3.Distance( ridgidbody.velocity , new Vector3( 0 , 0 , 0 ) ) > 2 ){
            yield return new WaitForEndOfFrame( );
        }
         animator.enabled = true;
       
        yield return new WaitForSeconds(6);

        agent.speed = speed;
        ridgidbody.isKinematic = true;
       

    }

    public void DOT( int damage , float interval , int hits , WeaponObject item ) {
        if ( damage <= 1 ){
            damage = 1;
        }

        StartCoroutine( EDOT( damage , interval , hits , item ) );
    }

    private void IncreaseCritChance( float critInc ) {
        stats.luck += critInc;

        //Never let the crit chance go out of range
        if ( stats.luck > 100.0f ){
            stats.luck = 60;
        }
    }

    public virtual void Damage( int _damage , BaseItems item ) {
        this.animator.Play("Hit");
        float randValue = Random.Range( 1 , 100 );
        if ( randValue > 100 - item.AttachedCharacter.stats.luck ){
            var damage = _damage * 2;
            stats.Health -= damage;
            InstatiateFloatingText.InstantiateFloatingCriticalText( damage.ToString( ) , Color.yellow , this );
            IncreaseCritChance( 1 );
        }
        else if ( randValue < ( 100 - item.AttachedCharacter.stats.luck ) * 0.25 ){
            InstatiateFloatingText.InstantiateFloatingMissText( "MISS" , Color.gray , this );
        }
        else{
            var damage = _damage;
            stats.Health -= damage;
            InstatiateFloatingText.InstantiateFloatingText( damage.ToString( ) , Color.white , this );
        }
        
        if ( stats.health <= 0 && this is Character){
            StaticManager.uiManager.GameOverWindow.SetActive(true);
        }
        int ranIndex = Random.Range(0, 2);
        this.audio.clip = clips[ranIndex];
        if (this.audio.clip != null)
        {
            if (!this.audio.isPlaying)
            {
                this.audio.PlayOneShot(this.audio.clip, 0.3f);
            }
        }
    }

    // Update is called once per frame
    protected void Update( ) { }

}