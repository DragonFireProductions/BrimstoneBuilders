using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

using TMPro;

using UnityEngine;
using UnityEngine.Experimental.UIElements;

public class UIItems : MonoBehaviour {

	
	// Use this for initialization
	[SerializeField]
	public UIItemsWithLabels InventoryContainer;
	
    public GameObject PauseUI;

	public GameObject PlayerUI;

	public GameObject DialogueUI;

	public GameObject GameOverUI;

    public GameObject BackPackUI;

    public GameObject BackpackContainer;

	public GameObject Equip;

    public GameObject AttackConfirmation;

	public GameObject BattleWon;

	public GameObject DamageText;

    public object this[string propertyName]
    {
        get { return this.GetType().GetField(propertyName).GetValue(this); }
        set { this.GetType().GetField(propertyName).SetValue(this, value); }
    }
    public void Start () {
        var properties = this.GetType().GetFields();
        

	    for (int i = 0; i < properties.Length; i ++ ){
		    FieldInfo l_fieldInfo = properties[ i ];
		    if ( l_fieldInfo.FieldType == typeof(GameObject) ){
                if (GameObject.Find(l_fieldInfo.Name) != null)
                {
                    this[l_fieldInfo.Name] = GameObject.Find(l_fieldInfo.Name);
                    GameObject.Find(l_fieldInfo.Name).SetActive(false);
                }
            }

		    else if ( l_fieldInfo.FieldType == typeof(UIItemsWithLabels) ){
			    this[ l_fieldInfo.Name ] = obj( this[ l_fieldInfo.Name ] as UIItemsWithLabels , l_fieldInfo.Name );
		    }
	    }
	}
	
	public UIItemsWithLabels obj(UIItemsWithLabels obj, string name ) {
		obj = new UIItemsWithLabels();
		obj.obj = GameObject.Find(name).gameObject;
		int count = obj.obj.transform.Find("Labels").childCount;
		obj.Labels = new List<UIItemsWithLabels.Label>();

        UIItemsWithLabels.Label label;
		for ( int i = 0 ; i < count ; i++ ){
			label = new UIItemsWithLabels.Label();
			label.labelObject = obj.obj.transform.Find( "Labels" ).GetChild( i ).gameObject;
			label.labelText = obj.obj.transform.Find( "Labels" ).GetChild( i ).gameObject.GetComponent < TextMeshProUGUI >( );
			label.name = label.labelObject.name;
			obj.Labels.Add(label);
		}
		obj.obj.SetActive(false);
		return obj;
	}
    public UIItemsWithLabels obj(GameObject _obj)
    {
        UIItemsWithLabels obj = new UIItemsWithLabels();
	    obj.obj = _obj;
        int count = obj.obj.transform.Find("Labels").childCount;
        obj.Labels = new List<UIItemsWithLabels.Label>();

        UIItemsWithLabels.Label label;
        for (int i = 0; i < count; i++)
        {
            label = new UIItemsWithLabels.Label();
            label.labelObject = obj.obj.transform.Find("Labels").GetChild(i).gameObject;
            label.labelText = obj.obj.transform.Find("Labels").GetChild(i).gameObject.GetComponent<TextMeshProUGUI>();
            label.name = label.labelObject.name;
            obj.Labels.Add(label);
        }
        obj.obj.SetActive(false);
        return obj;
    }
}
[Serializable]
public class UIItemsWithLabels {

	[Serializable]
	public struct Label {

		public TextMeshProUGUI labelText;

		public GameObject labelObject;

		public string name;

	}

	[SerializeField] public GameObject obj;
	[SerializeField] public List < Label > Labels;

	public void SetLabels( Stat stats ) {
		for ( int i = 0 ; i < Labels.Count ; i++ ){
			Labels[ i ].labelText.text = stats[ Labels[ i ].labelObject.name ].ToString( );
		}
	}
	
}
