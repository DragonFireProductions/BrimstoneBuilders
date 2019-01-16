using System.Collections;
using System.Collections.Generic;

using JetBrains.Annotations;

using UnityEngine;
using UnityEngine.UI;

public class Quest : MonoBehaviour
{
    public GameObject questWindow;

    public GameObject questTextBox;

    public Text questText;

    public QuestObject objectNeededContainer;

    public string questMessage;

    public NPCQuest NPC;
    
    public List <QuestObject> keyItems;

    public GameObject keyItemsHolder;

    public QuestObjective objective;

    public QuestItem objectToGet;

    public Type type;

    public List < QuestItem > items;



    public enum Type {

        Kill, Escort, Key

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void Accept( ) {
        switch ( type ){
            case Type.Kill:

                break;
            case Type.Escort:

                break;
            case Type.Key:
                questMessage = items[0].message;
                questText.text = items[0].message;
                items[ 0 ].quest = this;
                objectToGet = items[0];
                objectToGet.type = type;

                transform.SetParent(StaticManager.questManager.questsHolder.transform);
                gameObject.SetActive(true);

                objectNeededContainer.icon.sprite = items[0].icon;
                objectNeededContainer.labels.FindLabels();
                objectNeededContainer.labels.Labels[0].labelText.text = items[0].gameObject.name;
                StaticManager.map.Add(Map.Type.destination, objectToGet.mapIcon);
                objectToGet.light.gameObject.SetActive(true);
                NPC.light.SetActive(false);
                break;
        }
    }
    public void Complete( QuestItem item, string message ) {
        switch ( item.type ){
            case Type.Escort:

                break;
            case Type.Kill:

                break;
            case Type.Key:
                var keyitem = Instantiate(StaticManager.questManager.keyItems);
                 keyitem.gameObject.SetActive(true);
                keyitem.gameObject.transform.SetParent(StaticManager.questManager.keyItemsHolder.gameObject.transform);
                keyItems.Add(keyitem);
                keyitem.icon.sprite = items[ 0 ].icon;
                keyitem.labels.FindLabels();
                keyitem.labels.Labels[ 0 ].labelText.text = items[ 0 ].gameObject.name;
                items[0].gameObject.SetActive(false);
                items.RemoveAt(0 );
                
                if ( items.Count == 0 ){
                    NPC.Complete();
                }
                else{
                    
                    questText.text = "Collect " + items[ 0 ].gameObject.name;
                    objectNeededContainer.icon.sprite = items[ 0 ].icon;
                    items[ 0 ].light.enabled = true;
                    objectNeededContainer.labels.Labels[ 0 ].labelText.text = items[ 0 ].gameObject.name;
                    
                    StaticManager.map.Destination.Clear();
                    StaticManager.map.Add(Map.Type.destination, items[0].mapIcon);
                    item.gameObject.SetActive(false);
                }
                break;
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}