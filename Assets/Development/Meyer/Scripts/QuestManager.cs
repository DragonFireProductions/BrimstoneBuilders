using System;
using System.Collections;
using System.Collections.Generic;

using JetBrains.Annotations;

using UnityEngine;
using UnityEngine.UI;

public class QuestManager : MonoBehaviour {

    public List < GameObject > icons;
    [Serializable]
    public struct MyStruct {

        public string message;

        public NPCQuest NPC;

    }

    public List < Quest > quests = new List < Quest >( );


    [SerializeField]
    public Quest placeHolder;

    public GameObject questWindow;

    public GameObject questsHolder;


    public Quest currentQuest;

    // Start is called before the first frame update
    void Start()
    {
       
    }

    public Quest placeHolderInst( ) {
        return Instantiate(placeHolder);
    }
    public void SelectQuest( GameObject obj ) {

    }

    public void AddQuest( MyStruct quest ) {
        Quest _quest = newQuest( quest.message , quest.NPC );
        quests.Add(_quest);
    }


    public Quest newQuest(string text, NPCQuest _NPC)
    {
        Quest quest = StaticManager.questManager.placeHolderInst();
        quest.questMessage = text;
        quest.NPC = _NPC;
        quest.questText.text = text;

        quest.transform.SetParent(StaticManager.questManager.questsHolder.transform);
        quest.gameObject.SetActive(true);
        quest.objectNeededContainer.icon.GetComponent<RawImage>().texture = _NPC.objectToGet.stats.icon;
        quest.objectNeededContainer.labels.FindLabels();
        quest.objectNeededContainer.labels.Labels[ 0 ].labelText.text = _NPC.objectToGet.gameObject.name;


        return quest;


    }
    // Update is called once per frame
    void Update()
    {
        
    }
}