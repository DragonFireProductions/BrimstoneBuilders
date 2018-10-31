using System.Collections.Generic;

using Kristal;

using UnityEngine;

public class EnemyNav : BaseNav {

    /// <remarks>Set in Inspector</remarks>
    [ SerializeField ] private Animator animator;

    private Enemy character;

    [ SerializeField ] public GameObject location;

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
        character         = GetComponent < Enemy >( );
        Agent.destination = Random.insideUnitSphere * wanderDistance + location.transform.position;
        character.enemies = new List < Companion >( );
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

                if ( Vector3.Distance( StaticManager.Character.transform.position , transform.position ) < veiwDistance ){
                    character.ChooseEnemy( );
                }
            }

                break;
            case state.FOLLOW: {
                Agent.destination = character.leader.transform.position;
            }

                break;
            
            case state.ATTACKING: {
                //Removes the current enemy that this character is attacking from attack list if null
                if ( character.enemies[0] == null ){
                   character.enemies.RemoveAt(0);
                }
                //if the enemy is not attacking and has enemies to attack
                if ( character.enemies.Count > 0 && !character.AnimationClass.animation.GetBool( "Attacking" ) ){
                    //set the destination to the first enemy in attack list
                    Agent.SetDestination(character.enemies[0].transform.position);
                        //if it reached the first enemy in list, then attack
                        if (StaticManager.Utility.NavDistanceCheck(Agent) == DistanceCheck.HAS_REACHED_DESTINATION)
                        {
                            character.AnimationClass.Play( AnimationClass.states.Attacking);
                        }
                }
                //if this character has no enemies then choose a new companion to attack
                else if ( character.enemies.Count == 0 ){
                    character.ChooseEnemy();
                }
                // else if it is attacking and has enemies then look at enemies
                else if (character.enemies.Count > 0){
                    transform.LookAt( character.enemies[ 0 ].transform.position );
                }
            }

                break;

            default:

                break;
        }
    }

}