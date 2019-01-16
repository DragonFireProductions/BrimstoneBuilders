using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPCQuest : BaseCharacter {

    public Quest quest;

    public QuestManager.MyStruct mission;

    public string popupMessage;

    public bool hasTriggered = false;

    public string thankyouMessage;

    public GameObject mapObject;

    public QuestObjective objective;

    public GameObject npcQuestItem;

    public BoxCollider collider;

    public bool Completed;

    public QuestItem Goal;

    public Quest quest2;

    public QuestItem key;

    public Collider goalCollider;

    public GameObject spawner;

    // Start is called before the first frame update
    void Start() {
        StaticManager.map.Add(Map.Type.NPC, icon);
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void Complete( ) {
        Completed = true;
        light.SetActive(true);
    }
    public void OnTriggerEnter(Collider collider ) {
        if ( collider.tag == "Player" ){

            if (Completed){
                quest.keyItems.RemoveAll( item => item == null );
                foreach ( var l_questKeyItem in quest.keyItems ){
                    Destroy(l_questKeyItem.gameObject);
                }
                GetComponent<Collider>().enabled = false;
                light.SetActive(false);
                StartCoroutine(StaticManager.questManager.message(thankyouMessage));

                ///dropKey
                var keyitem = Instantiate(StaticManager.questManager.keyItems);
                quest.questText.text = key.message;
                keyitem.transform.SetParent(StaticManager.questManager.keyItemsHolder.gameObject.transform);
                keyitem.gameObject.SetActive(true);
                keyitem.icon.sprite = key.icon;
                keyitem.labels.FindLabels();
                keyitem.labels.Labels[0].labelText.text = key.gameObject.name;

                goalCollider.enabled = true;

            }
            else{
                var _quest = Instantiate(StaticManager.questManager.placeHolder);
                _quest.items = quest.items;
                _quest.type = quest.type;
                _quest.questMessage = quest.questMessage;
                _quest.NPC = this;
                quest = _quest;
                StartCoroutine( StaticManager.questManager.message( popupMessage ) );
                spawner.SetActive(true);
                quest.Accept();
            }

        }
    }

}
