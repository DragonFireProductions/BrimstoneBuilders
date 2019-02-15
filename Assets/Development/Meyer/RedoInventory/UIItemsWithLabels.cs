using System;

using Boo.Lang;

using TMPro;

using UnityEngine;
using UnityEngine.UI;

public class UIItemsWithLabels : MonoBehaviour
{

    public void SetStuff(BaseItems _item = null)
    {
        var ob = obj.transform.childCount;

        for (int i = 0; i < ob; i++)
        {
            var child = obj.transform.GetChild(i);
            Label l = new Label();
            l.name = child.name;
            l.labelText = child.GetComponent<Text>();
            l.labelObject = child.gameObject;
            Labels.Add( l );
        }

        if (_item)
        {
            item = _item;
        }
    }
    [Serializable]
    public struct Label
    {

        public Text labelText;

        public GameObject labelObject;

        public string name;

    }

    [SerializeField] public GameObject obj;
    [SerializeField] public List<Label> Labels;

    [SerializeField] public BaseItems item;

    public void SetLabels(Stat stats)
    {
        for (int i = 0; i < Labels.Count; i++)
        {
            
        }
    }

    public void FindLabels( ) {
        obj = gameObject;
        Labels = new List < Label >();
        var count = obj.transform.Find("Labels").transform.childCount;
        var label = obj.transform.Find( "Labels" );
        for ( int i = 0 ; i < count ; i++ ){
            Label la = new Label();
            var child = label.transform.GetChild( i );
            la.labelObject = child.gameObject;
            la.labelText = child.GetComponent < Text >( );
            la.name = child.name;
            Labels.Add( la );
        }
    }
}
