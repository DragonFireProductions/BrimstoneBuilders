using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class BaseItems : MonoBehaviour {
    public virtual object this[string propertyName]
    {
        get
        {
            if (this.GetType().GetField(propertyName) != null)
            {
                return this.GetType().GetField(propertyName).GetValue(this);
            }
            else
            {
                return null;
            }
        }
        set { this.GetType().GetField(propertyName).SetValue(this, value); }
    }


    public GameObject item;

	public ItemStats stats;

	public BaseCharacter AttachedCharacter;

	public string objectName;

	public float objectLevel = 1;

	public float IncreaseAmount;

	protected virtual void Start( ) {
		IncreaseStats(0);
	}
    public virtual void Use(BaseCharacter enemy = null  ) {
		
	}

	public virtual void Attach( ) {
		var a = AttachedCharacter as Companion;
	}

	public virtual void IncreaseStats(float amount ) {
		stats.baseDamage = (int)objectLevel + (int)amount + stats.baseDamage;
		stats.attackSpeed = ( int )objectLevel + (int)amount + stats.attackSpeed;
		stats.value = ( int )objectLevel + ( int )amount + stats.value;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
