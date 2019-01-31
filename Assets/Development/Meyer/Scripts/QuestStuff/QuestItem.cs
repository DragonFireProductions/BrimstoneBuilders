using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestItem : MonoBehaviour {

    public Quest quest;

    public Sprite icon;

    public string needToCollMessage;

    public KeyItemContainer keyItem;

    public Type type;
    public enum  Type {

        item,
        key,
        gate,
        unlock

    }
    public virtual void OnTriggerEnter(Collider collider ) {
        if ( collider.tag == "Player" ){
            if ( type == Type.item ){
                quest.CollidedWithItem(this);
                gameObject.SetActive(false);

            }
            else if ( type == Type.key ){
                quest.CollidedWithKey(this);
                gameObject.SetActive(false);
                StaticManager.uiManager.ShowMessage(quest.KeyDropDialog, 10, false);
            }
          else if ( type == Type.gate ){
                if ( quest.PickedUpKey == true ){
                    quest.Complete();
                    gameObject.SetActive(false);
                }
               
            }
            
        }

        else if ( collider.gameObject.GetComponent<EscortNPC>()  )
        {
            if (type == Type.unlock)
            {
                gameObject.SetActive(false);
            }
        }
       
    }
}
