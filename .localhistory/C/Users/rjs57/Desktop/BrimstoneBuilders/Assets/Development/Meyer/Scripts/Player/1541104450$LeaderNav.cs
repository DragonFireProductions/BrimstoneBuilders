﻿using System;
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

    [SerializeField] private TextMeshProUGUI head;
    [SerializeField] private TextMeshProUGUI left_arm;
    [SerializeField] private TextMeshProUGUI right_arm;
    [SerializeField] private TextMeshProUGUI body;
    [SerializeField] private TextMeshProUGUI torso;
    [SerializeField] private TextMeshProUGUI feet;

    [SerializeField] private RawImage image;
    private bool showArmor = false;

	private ParticleSystem selected;
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

	    if (Input.GetKey(KeyCode.A))
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
	        torso.enabled = true;
	        feet.enabled = true;
	    }
	    else
        {
	        image.enabled = false;
            head.enabled = false;
            left_arm.enabled = false;
            right_arm.enabled = false;
            body.enabled = false;
            torso.enabled = false;
            feet.enabled = false;
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

}
