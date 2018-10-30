using System.Collections;
using System.Collections.Generic;
using System.Reflection;

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

	public GameObject GameOverUI;

    public GameObject BackPackUI;

    public GameObject BackpackContainer;

	public GameObject AttackConfirmation;

	public GameObject BattleWon;

	public UIItemsWithLabels gameobjectName;

    public object this[string propertyName]
    {
        get { return this.GetType().GetField(propertyName).GetValue(this); }
        set { this.GetType().GetField(propertyName).SetValue(this, value); }
    }
    public void Start () {
       gameobjectName.SetLabels(StaticManager.Character.stats);
        var properties = this.GetType().GetFields();

	    foreach ( FieldInfo l_fieldInfo in properties ){
		    if ( l_fieldInfo.FieldType == typeof(GameObject) ){
                if (GameObject.Find(l_fieldInfo.Name) != null)
                {
                    this[l_fieldInfo.Name] = GameObject.Find(l_fieldInfo.Name);
                    GameObject.Find(l_fieldInfo.Name).SetActive(false);
                }
            }

		    if ( l_fieldInfo.FieldType == typeof(UIItemsWithLabels) ){
			    this[ l_fieldInfo.Name ] = obj( this[ l_fieldInfo.Name ] as UIItemsWithLabels , l_fieldInfo.Name );
		    }
	    }
	}

	public UIItemsWithLabels obj(UIItemsWithLabels obj, string name ) {
		obj = new UIItemsWithLabels();
		obj.obj = GameObject.Find(name);
		int count = obj.obj.transform.Find("Labels").childCount;
		obj.Labels = new List<UIItemsWithLabels.Label>();

        UIItemsWithLabels.Label label;
		for ( int i = 0 ; i < count ; i++ ){
			label = new UIItemsWithLabels.Label();
			label.labelObject = obj.obj.transform.Find( "Labels" ).GetChild( i ).gameObject;
			label.labelText = obj.obj.transform.Find( "Labels" ).GetChild( i ).gameObject.GetComponent < TextMeshProUGUI >( );
			obj.Labels.Add(label);
		}

		return obj;
	}
}

public class UIItemsWithLabels {

	public struct Label {

		public TextMeshProUGUI labelText;

		public GameObject labelObject;

		public string name;

	}

	public GameObject obj;
	public List < Label > Labels;

	public void SetLabels( Stat stats ) {
		for ( int i = 0 ; i < Labels.Count ; i++ ){
			Labels[ i ].labelText.text = stats[ Labels[ i ].labelObject.name ].ToString( );
		}
	}
	
}
