using System.Collections;
using System.Collections.Generic;
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

    public bool hasCollecteditem;
    
    public QuestObject keyItems;

    public GameObject keyItemsHolder;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void CollectItem( QuestItem item ) {
        hasCollecteditem = true;
        item.gameObject.SetActive(false);
        questText.text = "Return to NPC";
        keyItems.icon.GetComponent<RawImage>().texture = item.stats.icon;
        keyItems.gameObject.SetActive(true);
        keyItems.transform.SetParent(keyItemsHolder.transform);
        keyItems.labels.FindLabels();
        keyItems.labels.Labels[ 0 ].labelText.text = item.objectName;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
