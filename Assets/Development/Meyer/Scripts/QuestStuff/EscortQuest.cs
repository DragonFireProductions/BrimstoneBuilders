using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EscortQuest : Quest {

    public GameObject winObject;

    private bool accepted;

    public GameObject _destination;

    public Sprite CompanionIcon;

    public override void Accept( ) {
        accepted = true;
        base.Accept( );
        gameObject.GetComponent < NavMeshAgent >( ).SetDestination( _destination.transform.position );
        ui.icon.sprite = CompanionIcon;
        
        StaticManager.RealTime.Companions.Add(gameObject.GetComponent<EscortNPC>());

        ui.labels.Labels[ 0 ].labelText.text = name;
        
    }

    public void Failed( ) {
        StaticManager.uiManager.ShowMessage("Quest failed : NPC has died",4, false);
        StaticManager.questManager.quests.Remove( this );
        this.GetComponent < Collider >( ).enabled = false;
       
        Destroy(ui.gameObject);
         Destroy(gameObject);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void Complete( ) {
        base.Complete( );
        //StaticManager.uiManager.ShowMessage(KeyDropDialog, 10, false);
    }

    public void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "Player")
        {
            if (Completed)
            {
                var key = Instantiate(winObject);
                key.transform.position = transform.position + (transform.right * 3);
                key.gameObject.SetActive(true);
                gameObject.GetComponent<Collider>().enabled = false;
                StaticManager.questManager.CompleteQuest(this, KeyDropDialog);
                
                if (StaticManager.RealTime.Companions.Contains(GetComponent<EscortNPC>()))
                {
                    StaticManager.RealTime.Companions.Remove(GetComponent<EscortNPC>());
                    Destroy(GetComponent<EscortNPC>());
                }

                QuestInProgress.SetActive(false);
                QuestAvalible.SetActive(false);
                CollectionReady.SetActive(false);

                this.GetComponent<BreakRuins>().EscortDone = true;
                this.GetComponent<FlameOn>().EscortDone = true;
                this.GetComponent<SunChanges>().DesertFlameQuestLink = true;

                GetComponentInChildren<Canvas>().enabled = false;
                gameObject.GetComponent<Collider>().enabled = false;

            }
            else if (!accepted){
                 StaticManager.questManager.currentQuest = this;
                StaticManager.questManager.QuestConfirmation(this);
            }
            else
            {
                StaticManager.uiManager.ShowMessage("Please open quest log to see your current objective", 5, false);
            }

        }
    }
}
