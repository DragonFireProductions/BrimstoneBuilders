using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EscortQuest : Quest {

    public GameObject winObject;

    private bool accepted;

    public GameObject _destination;

    public GameObject spawner;

    public Sprite CompanionIcon;

    public override void Accept( ) {
        accepted = true;
        base.Accept( );
        gameObject.GetComponent < NavMeshAgent >( ).SetDestination( _destination.transform.position );
        ui.icon.sprite = CompanionIcon;
        
        StaticManager.RealTime.Companions.Add(gameObject.GetComponent<EscortNPC>());

        if ( StaticManager.RealTime.Companions.Contains(StaticManager.Character) ){
            StaticManager.RealTime.Companions.Remove( StaticManager.Character );
        }

        ui.labels.Labels[ 0 ].labelText.text = name;
        spawner.SetActive(true);
    }

    public void Failed( ) {
        //StaticManager.uiManager.ShowMessage("");

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void Complete( ) {
        base.Complete( );
        StaticManager.uiManager.ShowMessage(KeyDropDialog, 10, false);
    }

    public void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "Player")
        {
            if (Completed)
            {
                var key = Instantiate(winObject);
                key.transform.position = transform.position + (transform.forward * 3);
                key.gameObject.SetActive(true);
                gameObject.GetComponent<Collider>().enabled = false;
                StaticManager.questManager.CompleteQuest(this, KeyDropDialog);
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
