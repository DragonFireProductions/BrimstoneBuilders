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

    public Quest currentQuest;

    public GameObject questConfirmation;

    public List < Quest > NPCs;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void QuestConfirmation(Quest quest ) {
        StaticManager.uiManager.ShowMessage(currentQuest.QuestDialog, 10, true);
    }

    public void Accept( ) {
      
        currentQuest.Accept();
    }
    public void CompleteQuest(Quest quest, string message ) {
        quest.SwitchState(Quest.state.QuestComplete);
        StaticManager.uiManager.ShowMessage(message, 10, false);
        for ( int i = 0 ; i < quests.Count ; i++ ){
            if ( quests[i] == quest ){
             quests.RemoveAt(i);
            break;
            }

        }
        Destroy(quest.ui.gameObject);
    }
}


