using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Assets.Meyer.TestScripts.Player;

using Kristal;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class LeaderNav : CompanionNav {

	private RaycastHit hit;

	private float timer = 0;

    [SerializeField] private TextMeshProUGUI message;
    private float displaytimer;

    //[SerializeField] private TextMeshProUGUI head;
    //[SerializeField] private TextMeshProUGUI left_arm;
    //[SerializeField] private TextMeshProUGUI right_arm;
    //[SerializeField] private TextMeshProUGUI body;
    //[SerializeField] private TextMeshProUGUI legs;
    //[SerializeField] private TextMeshProUGUI feet;

    //[SerializeField] private RawImage image;
    //[SerializeField] private RawImage a_head;
    //[SerializeField] private RawImage a_left_arm;
    //[SerializeField] private RawImage a_right_arm;
    //[SerializeField] private RawImage a_body;
    //[SerializeField] private RawImage a_legs;
    //[SerializeField] private RawImage a_feet;
    //private bool showArmor = false;

	private ParticleSystem selected;
    private ParticleSystem rain;

    Vector3 rainPosition;

	private LayerMask mask;

	private Collider[] colliders;

	public Enemy enemy;

    public ParticleSystem enemySystem;


    void Start () {
		base.Start();
		hit = new RaycastHit();
		character = GetComponent < Character >( );
		message = GameObject.Find( "GoForward" ).GetComponent < TextMeshProUGUI >( );
		selected = StaticManager.particleManager.Play( ParticleManager.states.Selected , gameObject.transform.position );
		mask = LayerMask.GetMask("Enemy");
	    rain = StaticManager.particleManager.Play(ParticleManager.states.Rain, gameObject.transform.position);
	}

	// Update is called once per frame
	protected override void Update () {
		selected.gameObject.transform.position = gameObject.transform.position;
	    rainPosition = new Vector3(gameObject.transform.position.x, 10, gameObject.transform.position.z);
	    rain.gameObject.transform.position = rainPosition;

        if (Input.GetMouseButtonDown(0) && !StaticManager.UiInventory.ItemsInstance.windowIsOpen ){
			Ray l_ray = Camera.main.ScreenPointToRay(Input.mousePosition);
	        if ( Physics.Raycast(l_ray, out hit) ){
		        if ( hit.collider.name == "Terrain" ){
					SetState = state.MOVE;
		            StaticManager.particleManager.Play(ParticleManager.states.Click, hit.point);

			        if ( enemySystem ){
					Destroy(enemySystem.gameObject);

                    }
                }
		        else if (hit.collider.tag == "Enemy"){
					
					Destroy(enemySystem);
			        enemy = hit.collider.GetComponent < Enemy >( );
			        enemySystem = StaticManager.particleManager.Play( ParticleManager.states.EnemySelected , enemy.transform.position );
					enemySystem.transform.SetParent(enemy.transform);
			        SetState = state.ENEMY_CLICKED;
		        }
                else if (hit.collider.tag == "Post"){
			        message.text = hit.collider.name == "End" ? "The End is Neigh!" : "Go Forth!";

			        //Debug.Log("got the post");
			        StartCoroutine( show( ) );
		        }
            }

        }

		if ( StaticManager.UiInventory.ItemsInstance.windowIsOpen == false ){


			if ( Input.GetMouseButtonDown( 1 ) ){
				Ray l_ray = Camera.main.ScreenPointToRay( Input.mousePosition );
				RaycastHit hit1 = new RaycastHit();
				if ( Physics.Raycast( l_ray , out hit1 ) ){
					if ( hit1.collider.tag == "Companion" || hit1.collider.tag == "Player" ){
						StaticManager.UiInventory.ShowWindow( StaticManager.UiInventory.ItemsInstance.PlayerStats );
						StaticManager.UiInventory.UpdateStats( hit1.collider.GetComponent < BaseCharacter >( ).attachedWeapon.WeaponStats , StaticManager.UiInventory.ItemsInstance.AttachedWeapon );
						StaticManager.UiInventory.UpdateStats( hit1.collider.GetComponent < BaseCharacter >( ).stats ,                      StaticManager.UiInventory.ItemsInstance.CharacterStats, false );
						StaticManager.UiInventory.ItemsInstance.PlayerStats.transform.Find( "WeaponImage" ).GetComponent < RawImage >( ).texture = hit1.collider.GetComponent < BaseCharacter >( ).attachedWeapon.WeaponStats.icon;
					}
				}
			}
		}

		if ( Input.GetMouseButtonUp( 1 ) ){
				StaticManager.UiInventory.CloseWindow(StaticManager.UiInventory.ItemsInstance.PlayerStats );
			}
		


		//   if (Input.GetKeyDown(KeyCode.A))
	    //{
	    //    showArmor = !showingArmor();
	    //}

	    //if (showArmor)
	    //{
	    //    image.enabled = true;
	    //    head.enabled = true;
	    //    left_arm.enabled = true;
	    //    right_arm.enabled = true;
	    //    body.enabled = true;
	    //    legs.enabled = true;
	    //    feet.enabled = true;
	    //    a_head.enabled = true;
	    //    a_left_arm.enabled = true;
	    //    a_right_arm.enabled = true;
	    //    a_body.enabled = true;
	    //    a_legs.enabled = true;
	    //    a_feet.enabled = true;
	    //}
	    //else
     //   {
	    //    image.enabled = false;
     //       head.enabled = false;
     //       left_arm.enabled = false;
     //       right_arm.enabled = false;
     //       body.enabled = false;
     //       legs.enabled = false;
     //       feet.enabled = false;
     //       a_head.enabled = false;
     //       a_left_arm.enabled = false;
     //       a_right_arm.enabled = false;
     //       a_body.enabled = false;
     //       a_legs.enabled = false;
     //       a_feet.enabled = false;
     //   }

		if ( !StaticManager.RealTime.Attacking ){
			 colliders = Physics.OverlapSphere(transform.position, 10, mask);
			
			if ( colliders.Length > 0 ){
				StaticManager.RealTime.Attacking = true;
				StartCoroutine( yield( ) );
			}

           
        }
        if (StaticManager.RealTime.Enemies.Count == 0)
        {
            StaticManager.RealTime.Attacking = false;
        }
		
        switch ( State ){
			case state.ATTACKING:

				

				if ( enemy == null ){
					SetState = state.FREEZE;

					return;
				}
                if (Vector3.Distance(enemy.transform.position, gameObject.transform.position) > 3)
                {
                    SetState = state.ENEMY_CLICKED;
                }
                character.transform.LookAt(enemy.transform.position);
                    character.AnimationClass.Play(AnimationClass.states.AttackTrigger);
                    character.attachedWeapon.AnimationClass.Play(AnimationClass.weaponstates.EnabledTrigger);

                break;
			case state.MOVE:
				Agent.SetDestination( hit.point );
				break;
			case state.ENEMY_CLICKED:

				if (Vector3.Distance(enemy.transform.position, gameObject.transform.position) < 3  ){
					SetState = state.ATTACKING;
				}
				else{
					Agent.SetDestination( enemy.transform.position );
				}
				break;
			case state.IDLE:
				break;
			case state.FOLLOW:

				break;
			case state.FREEZE:
				break;
		default:

				break;
		}
	}

	IEnumerator show( ) {
		message.enabled = true;
		yield return  new WaitForSeconds(2);

		message.enabled = false;
	}
IEnumerator yield( ) {
		yield return new WaitForSeconds(0.5f);
		colliders = Physics.OverlapSphere(transform.position, 20, mask);
        foreach (var l_collider in colliders)
        {
            StaticManager.RealTime.Attacking = true;
            StaticManager.RealTime.Enemies.Add(l_collider.gameObject.GetComponent<Enemy>());
	        StaticManager.RealTime.Attacking = true;
			StaticManager.RealTime.SetAttackEnemies();
			StaticManager.RealTime.SetAttackCompanion();
            SetState = state.ATTACKING;
        }

    }
    //private IEnumerator CompanionSpawn()
    //{
    //    for (; i < friends.Length;)
    //    {
    //        var newFriend = Instantiate(friends[i].gameObject);
    //        Companion[] companion = newFriend.gameObject.GetComponentsInChildren<Companion>();

    //        foreach (var comp in companion)
    //        {
    //            Vector3 position = Random.insideUnitSphere * spawnRadius + this.gameObject.transform.position;
    //            //position.y = StaticManager.Character.gameObject.transform.position.y;
    //           // StaticManager.particleManager.Play()
    //            yield return new WaitForSeconds(1.0f);
    //            comp.Nav.Agent.Warp(position);
    //            comp.GetComponent<CompanionNav>().transform.position = this.gameObject.transform.position;
    //        }
    //    }
    //}

    //private void Despawn()
    //{
    //    for (; i < friends.Length;)
    //    {
    //        Destroy(friends[i].gameObject);
    //    }
    //}

}
