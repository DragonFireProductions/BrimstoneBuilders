using System;

using Boo.Lang;

using TMPro;

using UnityEngine;

public class UIItemsWithLabels : MonoBehaviour
{

    public void SetStuff(BaseItems _item)
    {
        var ob = obj.transform.Find("Labels").childCount;

        for (int i = 0; i < ob; i++)
        {
            var child = obj.transform.GetChild(i);
            Label l = new Label();
            l.name = child.name;
            l.labelText = child.GetComponent<TextMeshProUGUI>();
            l.labelObject = child.gameObject;
        }

        if (_item)
        {
            item = _item;
        }
    }
    [Serializable]
    public struct Label
    {

        public TextMeshProUGUI labelText;

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
            Labels[i].labelText.text = stats[Labels[i].labelObject.name].ToString();
        }
    }

    public void FindLabels( ) {
        Labels = new List < Label >();
        var count = obj.transform.Find("Labels").transform.childCount;
        var label = obj.transform.Find( "Labels" );
        for ( int i = 0 ; i < count ; i++ ){
            Label la = new Label();
            var child = label.transform.GetChild( i );
            la.labelObject = child.gameObject;
            la.labelText = child.GetComponent < TextMeshProUGUI >( );
            la.name = child.name;
        }
    }
}
