using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;

using Kristal;

using TMPro;

using UnityEngine;
using UnityEngine.UI;

public class KeyQuest : Quest
{
    public List < QuestItem > pickedUp;

    [SerializeField]
    public List < QuestItem > list;

 
    public Type type;

    public bool completed;

    public int index;

   
    public enum Type {

        attachToEnemy,

        spawnObject

    }
    
    public override void Accept( ) {
        base.Accept();
        ui.questText.text = list[0].needToCollMessage;
        ui.icon.sprite = list[0].icon;
        ui.labels.Labels[0].labelText.text = list[0].gameObject.name;
        if ( type == Type.spawnObject ){
            list[0].gameObject.SetActive(true);
            
            
        }
        else if ( type == Type.attachToEnemy ){
        }
    }

    public override void CollidedWithItem(QuestItem item ) {
        
       base.CollidedWithItem(item);
        pickedUp.Add(item);

        if ( list[0] != null ){
        list.RemoveAt(0);

        }
        if ( list.Count > 0 ){
            Accept();
        }
        else{
            completed = true;
        }
    }

    public override void InstEnemies( Enemy enemy ) {
        base.InstEnemies( enemy );
        if ( enemy.dropKey ){
            enemy.GetComponent<Enemy>().questItem = list[index];
            index++;
        }
    }

    public override void CollidedWithKey(QuestItem item)
    {
        base.CollidedWithKey(item);
    }



    public void DropLoot(QuestItem item, Enemy enemy) {
        var a = new Vector3(enemy.transform.position.x, StaticManager.Character.transform.position.y + 1, enemy.transform.position.z);
        var temp = Instantiate( item.gameObject );
        temp.transform.position = a;
        temp.SetActive(true);
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }

    public void DeleteAllKeys( ) {
        foreach (var l_collectablese in pickedUp)
        {
            Destroy(l_collectablese.keyItem.gameObject);
        }
        pickedUp.Clear();
    }
    public override void Complete( ) {
       DeleteAllKeys();
        StaticManager.questManager.CompleteQuest(this, "Gate is Now Open");
    }

    public void OnTriggerEnter( Collider collider ) {
        if ( collider.tag == "Player" ){
            if ( completed ){
                DeleteAllKeys();
                var key = Instantiate( this.key );
                key.transform.position = transform.position + (transform.forward * 3);
                key.gameObject.SetActive(true);
                key.quest = this;
                list.Add(key);
                Accept();
            }
            else if ( pickedUp.Count == 0 && ui == null){
                 StaticManager.questManager.currentQuest = this;
                 StaticManager.questManager.QuestConfirmation(this);
            }
            else{
                StaticManager.uiManager.ShowMessage("Please open quest log to see your current objective", 5, false);
            }
             
        }
    }
}
