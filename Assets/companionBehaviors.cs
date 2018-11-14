using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class companionBehaviors : MonoBehaviour
{
    [SerializeField] private CompanionNav.AggressionStates currentstate;

    public Companion newFriend;
    private GameObject prev_button;

    private TextMeshProUGUI name;
    // Use this for initialization
    void Start()
    {
        name  = transform.Find("character_name").GetComponent<TextMeshProUGUI>();
        name.text = newFriend.stats.name;
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
