using System.Collections;
using System.Collections.Generic;

using TMPro;

using UnityEngine;
using UnityEngine.Experimental.UIElements;

public class UIItems : MonoBehaviour {

	// Use this for initialization
	public GameObject InventoryContainer;

	public GameObject InventoryContainerPanel;

	public GameObject PauseUI;

	public GameObject PlayerUI;

	public GameObject DialogueUI;

	public GameObject StatUI;

	public GameObject StatLabels;

	public GameObject CompanionUI;

	public GameObject CompanionLabels;

	public GameObject Instructions;

	public GameObject InstructionPanel;

	public GameObject GameOverUI;

	public GameObject WeaponStatsUI;

	public GameObject WeaponStatsPanel;

	public GameObject WeaponLabels;

	public GameObject GameStatLabels;

    public GameObject BackPackUI;

    public GameObject BackpackContainer;

	public GameObject WeaponOptions;

	public GameObject WeaponInventoryLabels;

	public GameObject GameInventoryStatLabels;

	public GameObject WeaponInventoryStatsUI;

	public GameObject AttackConfirmation;

    public object this[string propertyName]
    {
        get { return this.GetType().GetField(propertyName).GetValue(this); }
        set { this.GetType().GetField(propertyName).SetValue(this, value); }
    }
    public void Start () {
	    var properties = this.GetType().GetFields();

        for ( int i = 0 ; i < this.GetType( ).GetFields( ).Length; i++ ){

	        if ( GameObject.Find(properties[i].Name) != null ){

			 this[ properties[ i ].Name ] = GameObject.Find( properties[ i ].Name );

            }
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
