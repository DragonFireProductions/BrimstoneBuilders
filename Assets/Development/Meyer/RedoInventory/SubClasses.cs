using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public abstract class SubClasses {

	public Companion character;

	public enum Types {

		MAGIC, MELE, RANGE

	}
    public virtual void IncreaseLevel(float amount)
    {
	    if ( CurrentLevel < 10 ){
			
		    CurrentLevel += amount;
        }
    }

	[ SerializeField ] public float CurrentLevel;

}
