using System;
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

    [SerializeField]
       public List < EnemySpawnerStuff > spawners;

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
                CollectionReady.SetActive(false);
                QuestInProgress.SetActive(false);
                QuestAvalible.SetActive(false);
                break;
        }
    }
    [Serializable]
    public struct EnemySpawnerStuff
    {

        public GameObject enemySpawnerPos;

        public List<KeyEnemies> enemies;

        public int spawnRadius;

        public bool prespawn;

        public int aggroRadius;

        public float minRadus;

        public float maxRadius;

    }
    [Serializable]
    public struct KeyEnemies
    {

        public Enemy enemy;

        public int Damage;

        public float luck;

        public GameObject weapon;

        public bool dropKey;

    }
 
    public void InitSpawners()
    {
        foreach (var l_spawner in spawners)
        {
            l_spawner.enemySpawnerPos.AddComponent<EnemySpawner>();

            var a = l_spawner.enemySpawnerPos.GetComponent<EnemySpawner>();
            a.AggroRange = l_spawner.aggroRadius;
            a.PreSpawn = l_spawner.prespawn;
            a.maxRange = l_spawner.maxRadius;
            a.minRange = l_spawner.minRadus;
            a.quest = this;
            
            l_spawner.enemySpawnerPos.SetActive(false);

            if ( this is KeyQuest ){
                foreach ( var l_spawnerEnemy in l_spawner.enemies ){
                    l_spawnerEnemy.enemy.dropKey = l_spawnerEnemy.dropKey;
                }
            }
        }

        for ( int i = 0 ; i < spawners.Count ; i++ ){
            var a = spawners[ i ].enemySpawnerPos.GetComponent < EnemySpawner >( );
            a.index = i;
            
        }
    }

    public void ActivateSpawner()
    {
        foreach (var l_spawner in spawners)
        {
            l_spawner.enemySpawnerPos.SetActive(true);
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
      ActivateSpawner();
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

        StaticManager.uiManager.ShowMessage("You have found " + item.gameObject.name, 3, false);
        CollectionReady.SetActive(true);
    }
    public virtual void CollidedWithKey(QuestItem item) {

        StaticManager.uiManager.ShowMessage(item.needToCollMessage, 4, true);
        PickedUpKey = true;
        this.CollidedWithItem(item);
    }

    public virtual void Complete()
    {
        QuestAvalible.SetActive(false);
        CollectionReady.SetActive(false);
        QuestInProgress.SetActive(false);
    }

    // Start is called before the first frame update
    public virtual void Start()
    {
        SwitchState(state.QuestAvailble);
        InitSpawners();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
