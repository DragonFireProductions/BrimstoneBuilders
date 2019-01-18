using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestManager : MonoBehaviour {

    public KeyItemContainer keyItemContainer;

    public List < Quest > quests;
    [SerializeField]
    public QuestUI questUI;

    public GameObject KeyItemsHolder;

    public GameObject QuestsHolder;

    public GameObject questWindow;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CompleteQuest(Quest quest ) {
        for ( int i = 0 ; i < quests.Count ; i++ ){
            if ( quests[i] == quest ){
             quests.RemoveAt(i);
            break;
            }

        }
        Destroy(quest.ui.gameObject);
    }
}


