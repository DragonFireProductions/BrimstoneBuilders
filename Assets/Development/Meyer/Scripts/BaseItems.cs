using System.Collections;
using System.Collections.Generic;

using Assets.Meyer.TestScripts.Player;

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

	public float subClassLevel;

	protected virtual void Start( ) {

	}
    public virtual void Use(BaseCharacter enemy = null  ) {
		
	}

	public virtual void Attach( ) {
    }

	public virtual void AssignDamage( ) { }
	public virtual void IncreaseSubClass( float amount ){}
	
}
