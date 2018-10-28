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
        if (Input.GetMouseButtonDown(0)){
			Ray l_ray = Camera.main.ScreenPointToRay(Input.mousePosition);
	        if ( Physics.Raycast(l_ray, out hit) ){
		        if ( hit.collider.name == "Terrain" ){
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
                    character.AnimationClass.Play(AnimationClass.states.Attacking);

                    character.enemies[0].AnimationClass.Play(AnimationClass.states.DamageText);
                    character.enemies[0].damageText.text = StaticManager.DamageCalc.CalcAttack(character.enemies[0].stats, character.stats).ToString();
                    character.enemies[0].damage += (int)StaticManager.DamageCalc.CalcAttack(character.enemies[0].stats, character.stats);
                    character.enemies[0].damageText.text = character.enemies[0].damage.ToString();
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
				timer += Time.deltaTime;
				if ( timer > 1.0f ){
					timer = 0;
					character.enemies[ 0 ].damage -= ( int )StaticManager.DamageCalc.CalcAttack( character.enemies[ 0 ].stats , character.stats );
					character.AnimationClass.Stop(AnimationClass.states.Attacking);
					SetState = state.FOLLOW;
				}
				break;
			default:

				break;
		}
	}


	
}
