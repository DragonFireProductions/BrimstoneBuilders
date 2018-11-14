using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class companionBehaviors : MonoBehaviour
{
    [SerializeField] private CompanionNav.AggressionStates currentstate;

    public Companion newFriend;
	// Use this for initialization
	void Start()
	{
	    var location = GameObject.Find("panel_location");
	    this.gameObject.transform.parent = location.transform;
	    this.gameObject.transform.position = location.transform.position;
	}

    public void onClick(int state)
    {
        newFriend.GetComponent<CompanionNav>().SetAgreesionState = (CompanionNav.AggressionStates) state;
    }

	// Update is called once per frame
	void Update()
	{

	}
}
