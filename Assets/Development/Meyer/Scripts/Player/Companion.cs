using System.Collections;
using System.Collections.Generic;

using Assets.Meyer.TestScripts;
using Assets.Meyer.TestScripts.Player;

using Kristal;

using TMPro;

using UnityEngine;
using UnityEngine.AI;

public class Companion : BaseCharacter {
	

	[ SerializeField ] public GameObject camHolder;
	

    // Use this for initialization
    void Start () {
		base.Awake();
		this.material.color = BaseColor;
	    camHolder = transform.Find( "CamHolder" ).gameObject;
	    Nav = gameObject.GetComponent < CompanionNav >( );
	    leader = StaticManager.character.GetComponent < CompanionLeader >( );
    }

	public override void Damage(BaseCharacter attacker ) {
		
    }
    public void Remove( BaseCharacter chara ) { }
    // Update is called once per frame
    void Update () { 
    }
	

	public GameObject CamHolder {
		get { return camHolder; }

	}
    public CompanionLeader Leader
    {
        get
        {
            return (CompanionLeader)leader;

        }
        set { leader = value; }
    }
}
