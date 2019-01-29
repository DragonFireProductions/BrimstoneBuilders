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

    private void Awake( ) { }

    private void Start( ) {
        base.Start( );
        character      = GetComponent < Enemy >( );
        stats          = GetComponent < Stat >( );
        battleDistance = 3;
    }

    public IEnumerator Nav( ) {
        while ( !Agent.isOnNavMesh ){
            yield return new WaitForEndOfFrame( );
        }

        Agent.destination = Random.insideUnitSphere * wanderDistance + location.transform.position;
        started           = true;
    }

    protected override void Update( ) {
        base.Update( );
        character.AnimationClass.animation.SetFloat( "Walk" , Agent.velocity.magnitude / Agent.speed );
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
            case state.ATTACKING: {

                character.attackers.RemoveAll( item => item               == null );
                StaticManager.RealTime.Companions.RemoveAll( item => item == null );
                StaticManager.RealTime.Enemies.RemoveAll( item => item    == null );
               
                if ( character.attackers.Count > 0 && character.enemy == null ){
                    var enemy = character.attackers[ Random.Range( 0 , character.attackers.Count ) ];
                    character.enemy = enemy;
                    enemy.attackers.Add( character );
                }

                //if no one is attacking current character and still enemies in the scene
                else if ( StaticManager.RealTime.GetCount( character ) && character.enemy == null ){
                    var enemy = StaticManager.RealTime.getnewType( character );
                    character.enemy = enemy;
                    enemy.attackers.Add( character );
                }

                //no enemies are alive
                else if ( character.enemy == null && character is Companion ){
                    StaticManager.RealTime.Attacking = false;
                    SetState                         = state.IDLE;

                    return;
                }

                if ( character.attachedWeapon is GunType ){
                    if ( StaticManager.Utility.NavDistanceCheck( Agent ) == DistanceCheck.HAS_REACHED_DESTINATION || StaticManager.Utility.NavDistanceCheck( Agent ) == DistanceCheck.HAS_NO_PATH ){
                        timer += Time.deltaTime;

                        if ( timer > waittime ){
                            newpos = StaticManager.Utility.randomInsideDonut( outerRadius , innerRadius , character.enemy.transform.position );
                            timer  = 0;
                        }
                    }
                    Vector3 look = new Vector3(character.enemy.transform.position.x, transform.position.y, character.enemy.transform.position.z);
                    transform.LookAt( look );
                    character.attachedWeapon.Use( );
                    Agent.SetDestination( newpos );
                }

                if ( character.attachedWeapon is SwordType ){
                    SetState = state.ATTACKING;
                    Agent.stoppingDistance = 2;
                    Agent.SetDestination( character.enemy.transform.position );
                        Vector3 look = new Vector3(character.enemy.transform.position.x, transform.position.y, character.enemy.transform.position.z);
                        transform.LookAt(look);
                        var distance = Vector3.Distance( transform.position , character.enemy.transform.position );

                    if ( distance < battleDistance ){
                        character.attachedWeapon.Use( );
                    }
                }

                //if current attacker dies and someone is still attacking
            }

                break;
        }
    }

}