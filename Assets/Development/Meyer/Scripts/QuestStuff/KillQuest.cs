using System.Collections;
using System.Collections.Generic;

using Kristal;

using UnityEngine;

public class KillQuest : Quest {

   [HideInInspector] public int EnemiesKilled = 0;

    public int KillAmount;

    public List < Enemy > enemies;

    public GameObject winObject;

    public GameObject spawner;

    public Sprite EnemyIcon;
    private bool accepted = false;

    public int enemiesKilled = 0;
    public override void Accept( ) {
        accepted = true;
        base.Accept();
        spawner.SetActive(true);
        accepted = true;
        ui.questText.text = QuestDialog;
        ui.icon.sprite = EnemyIcon;
        ui.labels.Labels[ 0 ].labelText.text = "Kill Count: " + enemies.Count;
    }

    public override void InstEnemies(Enemy enemy ) {
        enemies.Add(enemy);
        ui.labels.Labels[ 0 ].labelText.text = "Kill Count: " + enemies.Count;
    }

    public override void EnemyDied( Enemy enemy ) {
        enemies.Remove( enemy );
        ui.labels.Labels[ 0 ].labelText.text = "Kill Count: " + enemies.Count;
        enemiesKilled++;
    }
    

    // Update is called once per frame
    void Update()
    {
        if (KillAmount == enemiesKilled){
            ReturnToNPC();
            Completed = true;
        }
        else if (enemies != null && ui){
            ui.questText.text = "Kill " + enemies.Count + " more enemies";
        }
       
    }
    public void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "Player"){
            enemies.RemoveAll( item => item == null );
            if (Completed)
            {
                var key = Instantiate(winObject);
                key.transform.position = transform.position + (transform.forward * 3);
                key.gameObject.SetActive(true);
                StaticManager.questManager.CompleteQuest(this, KeyDropDialog);
                gameObject.GetComponent<Collider>().enabled = false;
            }
            else if (( enemies.Count == 0 && !Completed) || !accepted ){
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
