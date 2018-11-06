using System.Collections.Generic;

using Kristal;

using UnityEngine;

public class EnemyNav : BaseNav {

    /// <remarks>Set in Inspector</remarks>
    [ SerializeField ] private Animator animator;
    

    [ SerializeField ] public GameObject location;

    [ SerializeField ] private float maintainAttackDistance;

    private float timer;

    [ SerializeField ] private float veiwDistance;

    [ SerializeField ] private float wanderDelay;

    [ SerializeField ] private float wanderDistance;


    private Stat stats;
    private void Awake( ) {
        timer = Time.deltaTime;
    }

    private void Start( ) {
        base.Start( );
        character         = GetComponent < Enemy >( );
        Agent.destination = Random.insideUnitSphere * wanderDistance + location.transform.position;
        stats = GetComponent<Stat>();
        battleDistance = 3;

    }
    
    private void Update( ) {
        base.Update();
        timer += Time.deltaTime;


        switch ( State ){
            case state.IDLE: {
                //Debug.Log(stats.Strength);
                    character.threat_signal.enabled = stats.Strength > 10;

                    var l_check = StaticManager.Utility.NavDistanceCheck( Agent );

                if ( wanderDelay <= timer && ( l_check == DistanceCheck.HAS_REACHED_DESTINATION || l_check == DistanceCheck.PATH_INVALID ) || StaticManager.Utility.NavDistanceCheck( Agent ) == DistanceCheck.HAS_NO_PATH ){
                    Agent.destination = Random.insideUnitSphere * wanderDistance + location.transform.position;
                    timer             = 0;
                }
            }

                break;
            case state.FOLLOW: {
                Agent.destination = character.leader.transform.position;
            }

                break;
        }
    }

}