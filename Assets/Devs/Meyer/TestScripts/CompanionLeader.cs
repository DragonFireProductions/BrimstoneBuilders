using System.Collections;
using System.Collections.Generic;

using Assets.Meyer.TestScripts;

using UnityEngine;
using UnityEngine.AI;

public class CompanionLeader : Companion {
	
	[SerializeField] List < GameObject > companions;

	public List < Companion > CompanionGroup;


    public Color selectedColor = Color.yellow;

	public Color baseColor = Color.blue;

	public Color LeaderColor = Color.green;

	public Color PlayerSelectedColor = Color.red;

    // Use this for initialization
    void Start () {
		leader = this;
		obj = this.gameObject;
		Nav = null;
		agent = this.GetComponent < NavMeshAgent >( );
		Stats = this.gameObject.GetComponent < Stat >( );
		camHolder = this.gameObject.transform.Find( "CamHolder" ).gameObject;

		foreach ( var VARIABLE in companions ){
			CompanionGroup.Add(VARIABLE.GetComponent<Companion>());
		}
	}
    public void Remove(Companion _obj)
    {
        for (int l_i = 0; l_i < CompanionGroup.Count; l_i++)
        {
	        if ( CompanionGroup[ l_i ].gameObject == _obj.gameObject ){
		        CompanionGroup.RemoveAt( l_i );
		        TurnBasedController.instance.Companions.Remove( _obj );
	        }

        }
        Destroy(_obj.obj);
    }
}
