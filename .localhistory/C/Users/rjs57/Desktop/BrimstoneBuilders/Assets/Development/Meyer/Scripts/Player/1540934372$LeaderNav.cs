using System;
using System.Collections;
using System.Collections.Generic;

using Assets.Meyer.TestScripts.Player;

using Kristal;

using UnityEngine;

public class LeaderNav : CompanionNav {

	private RaycastHit hit;

	private float timer = 0;
	void Start () {
		base.Start();
		hit = new RaycastHit();
		character = GetComponent < Character >( );
		character.enemies = new List < Enemy >();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0) && !StaticManager.UiInventory.Dragging){
			Ray l_ray = Camera.main.ScreenPointToRay(Input.mousePosition);
	        if ( Physics.Raycast(l_ray, out hit) ){
		        if ( hit.collider.name == "Terrain"  ){
					SetState = state.MOVE;
                }
		        else if (hit.collider.tag == "Enemy"){
					character.enemies.Clear();
                    character.enemies.Add(hit.collider.GetComponent < Enemy >( ));

                    SetState = state.ATTACKING;
		        }
            }
        }
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


	
}
