﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class companionBehaviors : MonoBehaviour
{
    [SerializeField] private Companion.AggressionStates currentstate;

    public Companion newFriend;
	// Use this for initialization
	void Start()
	{

	}

    public void onClick(Companion.AggressionStates state)
    {
        newFriend.mood = state;
    }

	// Update is called once per frame
	void Update()
	{

	}
}