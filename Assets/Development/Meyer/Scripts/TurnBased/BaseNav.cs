using UnityEngine;
using UnityEngine.AI;

public class BaseNav : MonoBehaviour {

    public enum state {

        IDLE ,

        ATTACKING ,

        FOLLOW ,

        MOVE ,

        ENEMY_CLICKED ,

        FREEZE

    }

    public NavMeshAgent Agent;

    public float battleDistance = 3;

    public BaseCharacter character;

    protected float innerRadius = 8f;

    protected Vector3 newpos;

    protected float outerRadius = 10f;

    [ SerializeField ] protected state State;

    [ SerializeField ] public float stoppingDistance;

    [ SerializeField ] protected float waittime = 2;

    protected float timer = 0;

    public state SetState {
        get { return State; }
        set {
            if ( value == state.FOLLOW ){
                Agent.stoppingDistance = stoppingDistance;
            }

            if ( value == state.ATTACKING ){
                Agent.stoppingDistance = stoppingDistance;
                newpos = StaticManager.Utility.randomInsideDonut(outerRadius, innerRadius, StaticManager.Character.transform.position);

            }

            if ( value == state.MOVE ){
                Agent.stoppingDistance = 2;
                Agent.isStopped        = false;
            }

            if ( value == state.ENEMY_CLICKED ){
                Agent.stoppingDistance = stoppingDistance;
            }

            if ( value == state.IDLE ){
                Agent.stoppingDistance = stoppingDistance;
            }

            State = value;
        }
    }

    // Use this for initialization
    protected void Start( ) {
        Agent                  = gameObject.GetComponent < NavMeshAgent >( );
        stoppingDistance       = 3;
        Agent.stoppingDistance = stoppingDistance;
        character              = gameObject.GetComponent < BaseCharacter >( );
    }

    // Update is called once per frame
    protected virtual void Update( ) {
        switch ( State ){
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

                switch ( character.attachedWeapon.stats.weaponType ){
                    case WeaponItem.WeaponType.Gun:

                        if ( StaticManager.Utility.NavDistanceCheck( Agent ) == DistanceCheck.HAS_REACHED_DESTINATION || StaticManager.Utility.NavDistanceCheck(Agent) == DistanceCheck.HAS_NO_PATH){
                            timer += Time.deltaTime;

                            if ( timer > waittime ){
                            newpos = StaticManager.Utility.randomInsideDonut( outerRadius , innerRadius , character.enemy.transform.position );
                                timer = 0;
                            }
                         }

                        transform.LookAt( character.enemy.transform );
                        character.attachedWeapon.Attack( );
                        Agent.SetDestination( newpos );

                        break;
                    case WeaponItem.WeaponType.Sword:
                        SetState = state.ATTACKING;
                        Agent.SetDestination( character.enemy.transform.position );
                        transform.LookAt( character.enemy.transform );
                        var distance = Vector3.Distance( transform.position , character.enemy.transform.position );

                        if ( distance < battleDistance ){
                            character.attachedWeapon.Attack( character.enemy );
                        }

                        break;
                }

                //if current attacker dies and someone is still attacking
            }

                break;
            default:

                break;
        }
    }

}