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

    [SerializeField] private TextMeshProUGUI head;
    [SerializeField] private TextMeshProUGUI left_arm;
    [SerializeField] private TextMeshProUGUI right_arm;
    [SerializeField] private TextMeshProUGUI body;
    [SerializeField] private TextMeshProUGUI legs;
    [SerializeField] private TextMeshProUGUI feet;

    [SerializeField] private RawImage image;
    [SerializeField] private RawImage a_head;
    [SerializeField] private RawImage a_left_arm;
    [SerializeField] private RawImage a_right_arm;
    [SerializeField] private RawImage a_body;
    [SerializeField] private RawImage a_legs;
    [SerializeField] private RawImage a_feet;
    private bool showArmor = false;

	private ParticleSystem selected;


    [SerializeField] private float minRange = 15.0f;
    [SerializeField] private float maxRange = 25.0f;
    [SerializeField] private float spawnRadius = 10.0f;
    //[SerializeField]private float gang = 10.0f;

    [SerializeField] private GameObject friends;
    private int i;
    [SerializeField]private GameObject companionSpawner;

    private bool isActive = false;

	void Start () {
		base.Start();
		hit = new RaycastHit();
		character = GetComponent < Character >( );
		character.enemies = new List < Enemy >();
		message = GameObject.Find( "GoForward" ).GetComponent < TextMeshProUGUI >( );
		//selected = StaticManager.particleManager.Play( ParticleManager.states.Selected , gameObject.transform.position );

	}

	// Update is called once per frame
	void Update () {
		//selected.gameObject.transform.position = gameObject.transform.position;
        if (Input.GetMouseButtonDown(0) && !StaticManager.UiInventory.Dragging){
			Ray l_ray = Camera.main.ScreenPointToRay(Input.mousePosition);
	        if ( Physics.Raycast(l_ray, out hit) ){
		        if ( hit.collider.name == "Terrain"  ){
					SetState = state.MOVE;
                }
		        else if (hit.collider.tag == "Enemy"){
					character.enemies.Clear();
                    character.enemies.Insert(0, hit.collider.GetComponent < Enemy >( ));

                    SetState = state.ATTACKING;
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

	    if (Input.GetKeyDown("]"))
	    {
	        //StartCoroutine(CompanionSpawn());
	        var newEnemy = Instantiate(friends.gameObject);
	        newEnemy.transform.position = companionSpawner.transform.position;
	        //Instantiate(friends[i].gameObject);
	        ++i;
	    }
        else if (Input.GetKeyDown("["))
	    {
	        --i;
	         Despawn();
	    }

        if (Input.GetKeyDown(KeyCode.A))
	    {
	        showArmor = !showingArmor();
	    }

	    if (showArmor)
	    {
	        image.enabled = true;
	        head.enabled = true;
	        left_arm.enabled = true;
	        right_arm.enabled = true;
	        body.enabled = true;
	        legs.enabled = true;
	        feet.enabled = true;
	        a_head.enabled = true;
	        a_left_arm.enabled = true;
	        a_right_arm.enabled = true;
	        a_body.enabled = true;
	        a_legs.enabled = true;
	        a_feet.enabled = true;
	    }
	    else
        {
	        image.enabled = false;
            head.enabled = false;
            left_arm.enabled = false;
            right_arm.enabled = false;
            body.enabled = false;
            legs.enabled = false;
            feet.enabled = false;
            a_head.enabled = false;
            a_left_arm.enabled = false;
            a_right_arm.enabled = false;
            a_body.enabled = false;
            a_legs.enabled = false;
            a_feet.enabled = false;
        }

        displaytimer -= 0.005f;
        message.enabled = displaytimer > 0.0f;

        switch ( State ){
			case state.ATTACKING:

                Agent.SetDestination(character.enemies[0].transform.position);

                if (StaticManager.Utility.NavDistanceCheck(Agent) == DistanceCheck.HAS_REACHED_DESTINATION)
                {
                    SetState = state.FREEZE;
                }

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
					character.AnimationClass.Play(AnimationClass.states.Attacking);
				}
				break;
			default:

				break;
		}
	}

    bool showingArmor()
    {
        return showArmor;
    }

    private IEnumerator CompanionSpawn()
    {
        for (; i < friends.Length;)
        {
            var newFriend = Instantiate(friends[i].gameObject);

            Companion[] companion = newFriend.gameObject.GetComponentsInChildren<Companion>();

            foreach (var comp in companion)
            {
                Vector3 position = Random.insideUnitSphere * spawnRadius + this.gameObject.transform.position;
                //position.y = StaticManager.Character.gameObject.transform.position.y;
               // StaticManager.particleManager.Play()
                yield return new WaitForSeconds(1.0f);
                comp.Nav.Agent.Warp(position);
                comp.GetComponent<CompanionNav>().transform.position = this.gameObject.transform.position;
            }
        }
    }

    private void Despawn()
    {
        for (; i < friends.Length;)
        {
            Destroy(friends[i].gameObject);
        }
    }
}
