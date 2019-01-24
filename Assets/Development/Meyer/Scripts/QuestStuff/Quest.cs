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

    public GameObject QuestAvalible;

    public GameObject CollectionReady;

    public GameObject QuestInProgress;

    public enum state {

        QuestAvailble,

        QuestInProgress,

        CollectionReady,

        QuestComplete

    }

    protected state currState;

    public void SwitchState( state state ) {
        QuestAvalible.SetActive(false);
        CollectionReady.SetActive(false);
        QuestInProgress.SetActive( false );
        switch ( state ){
            case state.CollectionReady:
                   CollectionReady.SetActive(true);
                break;
            case state.QuestAvailble:
          QuestAvalible.SetActive(true);
                break;
            case state.QuestInProgress:
                QuestInProgress.SetActive( true );
                break;
            case state.QuestComplete:

                break;
        }
    }
    public virtual void Accept( ) { 
        SwitchState(state.QuestInProgress);
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
        SwitchState(state.CollectionReady);
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

        StaticManager.uiManager.ShowMessage("You have found a " + item.gameObject.name, 5, false);

    }
    public virtual void CollidedWithKey(QuestItem item) {

        StaticManager.uiManager.ShowMessage(item.needToCollMessage, 10, false);
        PickedUpKey = true;
        this.CollidedWithItem(item);
    }

    public virtual void Complete( ) { }

    // Start is called before the first frame update
    public void Start()
    {
        SwitchState(state.QuestAvailble);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
