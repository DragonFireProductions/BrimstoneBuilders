using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CompanionLeader : Companion {
	
	[SerializeField] List < GameObject > companions;

	public List < Companion > CompanionGroup;
	

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
    public void Remove(GameObject _obj)
    {
        for (int l_i = 0; l_i < CompanionGroup.Count; l_i++)
        {
            if (CompanionGroup[l_i].gameObject == _obj)
                CompanionGroup.RemoveAt(l_i);
        }
        Destroy(_obj);
    }
}
