using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class companionBehaviors : MonoBehaviour
{
    [SerializeField] private CompanionNav.AggressionStates currentstate;

    public Companion newFriend;
    private GameObject prev_button;

    // Use this for initialization
    void Start()
	{
	    var location = GameObject.Find("panel_location");
	    this.gameObject.transform.parent = location.transform;
	    this.gameObject.transform.position = location.transform.position;
	}

    public void onClick(int state)
    {
        if (prev_button)
        {
            prev_button.GetComponent<Image>().color = Color.white;
        }
        newFriend.GetComponent<CompanionNav>().SetAgreesionState = (CompanionNav.AggressionStates) state;


    }

    public void color(GameObject go)
    {
        go.GetComponent<Image>().color = Color.green;


        prev_button = go;
    }

	// Update is called once per frame
	void Update()
	{

	}
}
