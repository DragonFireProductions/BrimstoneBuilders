using System;
using System.Collections;
using System.Collections.Generic;

using Kristal;

using UnityEngine;

public class LeaderNav : CompanionNav {

	private RaycastHit hit;

	void Start () {
		base.Start();
		hit = new RaycastHit();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0)){
			Ray l_ray = Camera.main.ScreenPointToRay(Input.mousePosition);
	        if ( Physics.Raycast(l_ray, out hit) ){
		        if ( hit.collider.name == "Terrain" ){
					SetState = state.MOVE;
                }
		        else{
					
		        }
            }
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
			default:

				break;
		}
	}


	
}
