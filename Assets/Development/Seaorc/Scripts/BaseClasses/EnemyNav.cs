using UnityEngine;

public class EnemyNav : BaseNav {

    /// <remarks>Set in Inspector</remarks>
    [ SerializeField ] private Animator animator;

    [ SerializeField ] private GameObject location;

    [ SerializeField ] private float maintainAttackDistance;

    private float timer;

    [ SerializeField ] private float veiwDistance;

    [ SerializeField ] private float wanderDelay;

    [ SerializeField ] private float wanderDistance;

    private void Awake( ) {
        timer = Time.deltaTime;
    }

    private void Start( ) {
        base.Start( );
        Agent.destination = Random.insideUnitSphere * wanderDistance + location.transform.position;
    }

    private void Update( ) {
        timer += Time.deltaTime;

        switch ( State ){
            case state.IDLE: {
                var l_check = StaticManager.Utility.NavDistanceCheck( Agent );

                if ( wanderDelay <= timer && ( l_check == DistanceCheck.HAS_REACHED_DESTINATION || l_check == DistanceCheck.PATH_INVALID ) || StaticManager.Utility.NavDistanceCheck( Agent ) == DistanceCheck.HAS_NO_PATH ){
                    Agent.destination = Random.insideUnitSphere * wanderDistance + location.transform.position;
                    timer             = 0;
                }
            }

                break;
            default:

                break;
        }
    }

}