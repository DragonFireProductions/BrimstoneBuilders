using System;
using System.Collections;
using System.Collections.Generic;

using Assets.Meyer.TestScripts.Player;

using Kristal;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LeaderNav : CompanionNav {

	private RaycastHit hit;

	private float timer = 0;

    [SerializeField] private TextMeshProUGUI message;
    private float displaytimer;

	private ParticleSystem selected;

	private LayerMask mask;

	private Collider[] colliders;
	void Start () {
		base.Start();
		hit = new RaycastHit();
		character = GetComponent < Character >( );
		message = GameObject.Find( "GoForward" ).GetComponent < TextMeshProUGUI >( );
		selected = StaticManager.particleManager.Play( ParticleManager.states.Selected , gameObject.transform.position );
		mask = LayerMask.GetMask("Enemy");
	}

	// Update is called once per frame
	protected override void Update () {
		selected.gameObject.transform.position = gameObject.transform.position;
        if (Input.GetMouseButtonDown(0) && !StaticManager.UiInventory.Dragging){
			Ray l_ray = Camera.main.ScreenPointToRay(Input.mousePosition);
	        if ( Physics.Raycast(l_ray, out hit) ){
		        if ( hit.collider.name == "Terrain"  ){
					SetState = state.MOVE;
                }
		        else if (hit.collider.tag == "Enemy"){
			        if ( Vector3.Distance(hit.collider.gameObject.transform.position, gameObject.transform.position) < 3 ){
				      character.AnimationClass.Play(AnimationClass.states.AttackTrigger);
						character.attachedWeapon.AnimationClass.Play(AnimationClass.weaponstates.EnabledTrigger);
			        }
		        }
                else if (hit.collider.tag == "Post")
		        {
                    //Debug.Log("got the post");
		            displaytimer = 3.0f;
			        message.text = "GO FORTH";
		        }
				else if ( hit.collider.tag == "Weapon" ){
			        displaytimer = 2.0f;
			        message.text = "Run over me and drag from inventory! (KeyCode-I) ";
			     
            }
          }

        }

		if ( !StaticManager.RealTime.Attacking ){
			 colliders = Physics.OverlapSphere(transform.position, 10, mask);
			
			if ( colliders.Length > 0 ){
				StartCoroutine( yield( ) );
			}

           
        }

        displaytimer -= 0.005f;
        message.enabled = displaytimer > 0.0f;
		
        switch ( State ){
			case state.ATTACKING:
				

                break;
			case state.MOVE:
				Agent.SetDestination( hit.point );
				break;
			case state.ENEMY_CLICKED:

				break;
			case state.IDLE:

				break;
			case state.FOLLOW:

				break;
			case state.FREEZE:

				if ( !character.AnimationClass.animation.GetBool("Attacking") && Input.GetMouseButton(0)){
					character.AnimationClass.Play(AnimationClass.states.AttackTrigger);
					character.attachedWeapon.AnimationClass.Play(AnimationClass.weaponstates.EnabledTrigger);
				}
				break;
		default:

				break;
		}
	}

	IEnumerator yield( ) {

		yield return new WaitForSeconds(0.5f);
		colliders = Physics.OverlapSphere(transform.position, 10, mask);
        foreach (var l_collider in colliders)
        {
            StaticManager.RealTime.Attacking = true;
            StaticManager.RealTime.Enemies.Add(l_collider.gameObject.GetComponent<Enemy>());

            SetState = state.ATTACKING;
        }


        StaticManager.RealTime.SetAttackCompanions();
        StaticManager.RealTime.SetAttackEnemies();
    }


}
