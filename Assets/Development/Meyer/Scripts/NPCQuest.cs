using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPCQuest : BaseCharacter
{
    public QuestItem objectToGet;

    public QuestManager.MyStruct mission;

    public string popupMessage;

    public bool hasTriggered = false;

    public Quest quest;

    public string thankyouMessage;

    public GameObject mapObject;

    public GameObject Icon;

    // Start is called before the first frame update
    void Start() {
        objectToGet.AttachedCharacter = this;
        StaticManager.map.Add(Map.Type.NPC, Icon);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter(Collider collider ) {
        if ( collider.tag == "Player" ){
            if ( hasTriggered ){
                if ( quest.hasCollecteditem ){
                    Destroy(quest.keyItems.gameObject);
                    Destroy(quest.gameObject);
                    StartCoroutine( message( thankyouMessage ) );

                    //TODO: Drop key
                }
            }
            else{
                StaticManager.questManager.AddQuest( mission );
                quest = StaticManager.questManager.quests[StaticManager.questManager.quests.Count - 1];
                objectToGet.quest = quest;
                hasTriggered = true;
                StartCoroutine( message( popupMessage ) );

                //Todo: add to map
                var obj = Instantiate( objectToGet.mapIcon );
                objectToGet.mapIcon = obj;
                obj.transform.position = new Vector3(mapObject.transform.position.x, mapObject.transform.position.y + 10, mapObject.transform.position.z);
                obj.transform.SetParent(mapObject.transform);
                StaticManager.map.All.Add(obj.gameObject);
                StaticManager.map.Destination.Add(obj.gameObject);
                

            }
        }
    }

    public IEnumerator message(string text ) {

        StaticManager.UiInventory.ShowWindow(StaticManager.uiManager.MessageWindow);
        StaticManager.uiManager.messageText.text = text;

        yield return new WaitForSecondsRealtime(5);

        StaticManager.UiInventory.CloseWindow(StaticManager.uiManager.MessageWindow);
    }

}
