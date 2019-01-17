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

        public QuestItem objectToCollect;

    }

    public List < QuestObjective > objectives = new List < QuestObjective >( );

    [SerializeField]
    public Quest placeHolder;

    public GameObject questWindow;

    public GameObject questsHolder;


    public GameObject keyItemsHolder;

    public Quest currentQuest;

    public QuestObject keyItems;

    // Start is called before the first frame update
    void Start()
    {
       
    }

    public Quest placeHolderInst( ) {
        return Instantiate(placeHolder);
    }
    public void SelectQuest( GameObject obj ) {

    }

    public QuestObjective NewObjective(NPCQuest npc ) {
        QuestObjective obj = new QuestObjective(npc);
        objectives.Add(obj);

        return obj;
    }
    
    public void AddQuest( MyStruct quest, QuestObjective objective ) {
        objective.newQuest( quest.message , quest.objectToCollect, objective );
    }

    public void QuestComplete(QuestObjective objective, Quest quest ) {
        objective.QuestComplete(quest);
        StartCoroutine(message( quest.questMessage ));
        StaticManager.map.All.Remove( quest.objectToGet.mapIcon );
        StaticManager.map.Destination.Remove( quest.objectToGet.mapIcon );
        //Destroy(quest.keyItems);
        Destroy(quest.gameObject);
        quest.objectToGet.light.gameObject.SetActive(false);
    }
    public IEnumerator message(string text)
    {

        StaticManager.UiInventory.ShowWindow(StaticManager.uiManager.MessageWindow);
        StaticManager.uiManager.messageText.text = text;

        yield return new WaitForSecondsRealtime(15);

        StaticManager.UiInventory.CloseWindow(StaticManager.uiManager.MessageWindow);
    }

}

public class QuestObjective {

    public List <Quest> quests;

    public NPCQuest npc;

    public void QuestComplete(Quest quest ) {
        quests.Remove( quest );
    }

    public QuestObjective(NPCQuest _npc) {
        quests = new List < Quest >();
        npc = _npc;
    }
    public void newQuest(string text, QuestItem objecttocollect, QuestObjective objective)
    {
        Quest quest = StaticManager.questManager.placeHolderInst();
        quest.questMessage = text;
        quest.NPC = objective.npc;
        quest.questText.text = text;
        objecttocollect.quest = quest;
        quest.objectToGet = objecttocollect;

        quest.transform.SetParent(StaticManager.questManager.questsHolder.transform);
        quest.gameObject.SetActive(true);

        quest.objectNeededContainer.icon.GetComponent<RawImage>().texture = objecttocollect.mapIcon.texture;
        quest.objectNeededContainer.labels.FindLabels();
        quest.objectNeededContainer.labels.Labels[0].labelText.text = objecttocollect.gameObject.name;
        StaticManager.map.Add(Map.Type.destination, quest.objectToGet.mapIcon);
        quest.objectToGet.light.gameObject.SetActive(true);

        quests.Add(quest);

    }

}
