using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[SerializeField]
public class QuestItem : BaseItems {

    public GameObject mapIcon;

    public Quest quest;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter(Collider collider ) {
        if ( collider.tag == "Player" ){
             quest.CollectItem(this);
        }
       
    }
}
