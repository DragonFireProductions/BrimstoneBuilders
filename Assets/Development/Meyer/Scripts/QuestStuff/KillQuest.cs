using System.Collections;
using System.Collections.Generic;

using Kristal;

using UnityEngine;

public class KillQuest : Quest {

   [HideInInspector] public int EnemiesKilled = 0;

    private int KillAmount = 0;

    private List < Enemy > enemies;

    public GameObject winObject;

    public Sprite EnemyIcon;
    private bool accepted = false;

    public int enemiesKilled = 0;
    public override void Accept( ) {
        accepted = true;
        base.Accept();
       
        ui.questText.text = "Please Kill " + KillAmount + " enemies.";
        ui.icon.sprite = EnemyIcon;


        ui.labels.Labels[ 0 ].labelText.text = "Kill Count: " + KillAmount;

       
    }

    public override void InstEnemies(Enemy enemy ) {
        ui.labels.Labels[ 0 ].labelText.text = "Kill Count: " + KillAmount;
    }

    public override void EnemyDied( Enemy enemy ) {
        enemiesKilled++;
          ui.labels.Labels[ 0 ].labelText.text = "Kill Count: " + enemiesKilled;
    }

    public override void Start( ) {
        base.Start();
        foreach (var l_enemySpawnerStuff in spawners)
        {
            foreach (var l_keyEnemiese in l_enemySpawnerStuff.enemies)
            {
                KillAmount++;
            }
        }
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
            if ( enemies != null ){
                 enemies.RemoveAll( item => item == null );
            }
           
            if (Completed)
            {
                var key = Instantiate(winObject);
                key.transform.position = transform.position + (transform.forward * 3);
                key.gameObject.SetActive(true);
                StaticManager.questManager.CompleteQuest(this, KeyDropDialog);
                gameObject.GetComponent<Collider>().enabled = false;
                GetComponentInChildren<Canvas>().enabled = false;
            }
            else if (( enemiesKilled == 0 && !Completed) || !accepted ){
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
