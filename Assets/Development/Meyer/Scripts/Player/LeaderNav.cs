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

        if (Input.GetMouseButtonDown(0) && !StaticManager.UiInventory.Dragging){
			Ray l_ray = Camera.main.ScreenPointToRay(Input.mousePosition);
	        if ( Physics.Raycast(l_ray, out hit) ){
		        if ( hit.collider.name == "Terrain"  ){
					SetState = state.MOVE;
		            StaticManager.particleManager.Play(ParticleManager.states.Click, hit.point);
		        }
		        else if (hit.collider.tag == "Enemy"){
			        if ( Vector3.Distance(hit.collider.gameObject.transform.position, gameObject.transform.position) < 3 ){
				      character.AnimationClass.Play(AnimationClass.states.AttackTrigger);
						character.attachedWeapon.AnimationClass.Play(AnimationClass.weaponstates.EnabledTrigger);
			        }
		        }
                else if (hit.collider.tag == "Post")
		        {
			        if ( hit.collider.name == "End" ){
				        message.text = "The End is Neigh!";
			        }
			        else{
				        message.text = "Go Forth!";
			        }
                    //Debug.Log("got the post");
			        StartCoroutine( show( ) );
		        }
            }

        }

        if (Input.GetKeyDown("]"))
        {
            //StartCoroutine(CompanionSpawn());
            var newEnemy = Instantiate(friends.gameObject);
            newEnemy.transform.position = companionSpawner.transform.position;
            comp = newEnemy.gameObject.GetComponentsInChildren<Companion>();
            //++i;
            Debug.Log(comp.Length);
            foreach (var companion in comp)
            {
                Vector3 pos = Random.insideUnitSphere + companionSpawner.transform.position;
                companion.Nav.Agent.Warp(pos);
            }
            //Instantiate(friends[i].gameObject);

        }
        else if (Input.GetKeyDown("["))
        {
            Destroy(comp[i].gameObject);
            //--i;
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

    //bool showingArmor()
	
    //{
    //    return showArmor;
    //}

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
			StaticManager.RealTime.SetAttackCompanions();
			StaticManager.RealTime.SetAttackEnemies();
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
