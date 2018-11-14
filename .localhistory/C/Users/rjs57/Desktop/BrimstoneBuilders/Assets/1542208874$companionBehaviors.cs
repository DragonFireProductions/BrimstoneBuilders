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

        var newButton = GameObject.Find("Panel");
        if (newFriend.GetComponent<CompanionNav>().aggState == CompanionNav.AggressionStates.BERZERK)
        {
          newButton.GetComponent<Renderer>().material.color = Color.green;
        }
    }

	// Update is called once per frame
	void Update()
	{

	}
}
