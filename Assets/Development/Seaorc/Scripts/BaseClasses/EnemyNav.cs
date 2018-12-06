using System.Collections;

using Kristal;

using UnityEngine;

public class EnemyNav : BaseNav {

    /// <remarks>Set in Inspector</remarks>
    [ SerializeField ] private Animator animator;

    [ SerializeField ] public GameObject location;

    [ SerializeField ] private float maintainAttackDistance;

    [ SerializeField ] private float veiwDistance;

    [ SerializeField ] private float wanderDelay;

    [ SerializeField ] private float wanderDistance;

    public bool started;

    private Stat stats;

    private void Awake( ) {
    }

    private void Start( ) {
        base.Start( );
        character      = GetComponent < Enemy >( );
        stats          = GetComponent < Stat >( );
        battleDistance = 3;
        SetState = state.IDLE;
    }

    public IEnumerator Nav( ) {
        while ( !Agent.isOnNavMesh ){
            yield return new WaitForEndOfFrame( );
        }

        Agent.destination = Random.insideUnitSphere * wanderDistance + location.transform.position;
        started           = true;
    }

    private void Update( ) {
        base.Update( );
        timer += Time.deltaTime;
        
            switch ( State ){
                case state.IDLE: {
                    //Debug.Log(stats.Strength);
                    var l_check = StaticManager.Utility.NavDistanceCheck( Agent );

                    if ( wanderDelay <= timer && ( l_check == DistanceCheck.HAS_REACHED_DESTINATION || l_check == DistanceCheck.PATH_INVALID ) || StaticManager.Utility.NavDistanceCheck( Agent ) == DistanceCheck.HAS_NO_PATH ){
                        Agent.destination = Random.insideUnitSphere * wanderDistance + location.transform.position;
                        timer             = 0;
                    }
                }
                    break;
            }
    }

}