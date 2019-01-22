using System.Collections;
using System.Collections.Generic;

using Kristal;

using UnityEngine;

public class Quest : MonoBehaviour {

    public Vector3 destination;

    public NPC npc;

     public QuestItem key;

    public bool PickedUpKey;

    public string QuestDialog;

    public string KeyDropDialog;

    public bool Completed = false;

    public QuestItem loot;

    public virtual void Accept( ) { 
        if (ui == null)
        {
            
            var a = Instantiate(StaticManager.questManager.questUI.gameObject);
            var b = a.GetComponent<QuestUI>();

            a.transform.SetParent(StaticManager.questManager.QuestsHolder.transform);
            a.SetActive(true);
            ui = b;
            ui.labels.FindLabels();
            ui.questText.text = QuestDialog;
        }

        if ( !StaticManager.RealTime.Companions.Contains(StaticManager.Character) ){
            StaticManager.RealTime.Companions.Add( StaticManager.Character );
        }
      
    }

    public virtual void InstEnemies(Enemy enemy ) {

    }

    public virtual void EnemyDied( Enemy enemy ) {

    }
    private Quest obj;

    public QuestUI ui;
    public void ReturnToNPC( ) {
        ui.questText.text = "Return to NPC";

    }

    public virtual void CollidedWithItem(QuestItem item ) {
        var keyContainer = Instantiate(StaticManager.questManager.keyItemContainer.gameObject);
        keyContainer.SetActive(true);
        keyContainer.gameObject.transform.SetParent(StaticManager.questManager.KeyItemsHolder.transform);
        keyContainer.GetComponent < KeyItemContainer >( ).icon.sprite = item.icon;
        keyContainer.GetComponent<KeyItemContainer>().labels.FindLabels();
        keyContainer.GetComponent<KeyItemContainer>().labels.Labels[0].labelText.text = item.gameObject.name;
        item.keyItem = keyContainer.GetComponent < KeyItemContainer >( );

        StaticManager.uiManager.ShowMessage("You have found a " + item.gameObject.name, 5);

    }
    public virtual void CollidedWithKey(QuestItem item) {

        StaticManager.uiManager.ShowMessage(item.needToCollMessage, 10);
        PickedUpKey = true;
        this.CollidedWithItem(item);
    }

    public virtual void Complete( ) { }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
