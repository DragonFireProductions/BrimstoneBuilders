using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class companionBehaviors : MonoBehaviour
{
    [SerializeField] private Companion.AggressionStates currentstate;

    [SerializeField] public Companion newFriend;
	// Use this for initialization
	void Start()
	{

	}

    void isClicked()
    {
        newFriend.mood = currentstate;
    }

	// Update is called once per frame
	void Update()
	{

	}
}
