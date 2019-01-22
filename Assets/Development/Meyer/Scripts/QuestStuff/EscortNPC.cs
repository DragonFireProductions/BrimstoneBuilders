using System.Collections;
using System.Collections.Generic;

using Kristal;

using UnityEngine;

public class EscortNPC : BaseCharacter {

    public EscortQuest quest;

    public int runAwayDistance;

    public GameObject retreat_destination;
    public override void Damage( int _damage , BaseItems item ) {
        base.Damage(_damage, item );

        if (stats.health <= 0  ){
            quest.Failed();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    
    // Update is called once per frame
    void Update() {
        attackers.RemoveAll( item => item == null );
        if ( Vector3.Distance(quest._destination.transform.position, gameObject.transform.position) <= runAwayDistance ){
            foreach ( var l_baseCharacter in attackers ){
                l_baseCharacter.GetComponent < EnemyNav >( ).enabled = false;
                l_baseCharacter.agent.SetDestination( retreat_destination.transform.position );

                if ( StaticManager.Utility.NavDistanceCheck(l_baseCharacter.agent) == DistanceCheck.HAS_REACHED_DESTINATION ){
                    Destroy(l_baseCharacter.attachedWeapon.gameObject);
                    Destroy(l_baseCharacter.gameObject);
                    
                }
            }

           
            if (agent.isActiveAndEnabled && StaticManager.Utility.NavDistanceCheck(agent) == DistanceCheck.HAS_REACHED_DESTINATION ){
             quest.Complete();
             quest.Completed = true;
             StaticManager.RealTime.Companions.Add(StaticManager.Character);
             agent.enabled = false;
            }
        }
        
    }


}
