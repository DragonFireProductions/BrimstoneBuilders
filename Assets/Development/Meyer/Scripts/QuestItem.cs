using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[SerializeField]
public class QuestItem : MonoBehaviour{

    public RawImage mapIcon;

    public Sprite icon;

    public Quest quest;

    public string message;

    public bool collected;

    public Light light;

    public UnityEvent events;

    public Quest.Type type;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter(Collider collider ) {
        if ( collider.tag == "Player" ){
            switch (type)
            {
                case Quest.Type.Kill:

                    break;
                case Quest.Type.Escort:

                    break;
                case Quest.Type.Key:
                    collected = true;
                    light.enabled = false;
                    quest.Complete(this, message);
                    this.GetComponent < Collider >( ).enabled = false;
                    break;
            }
        }
    }
}
