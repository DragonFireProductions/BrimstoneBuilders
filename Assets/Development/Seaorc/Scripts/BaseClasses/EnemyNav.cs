using System.Collections.Generic;

using Assets.Meyer.TestScripts.Player;

using JetBrains.Annotations;

using Kristal;

using TMPro;

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

    private Enemy character;
    private void Awake( ) {
        timer = Time.deltaTime;
    }

    private void Start( ) {
        base.Start( );
        character = GetComponent < Enemy >( );
        Agent.destination = Random.insideUnitSphere * wanderDistance + location.transform.position;
        character.enemies = new List < Companion >();
    }

    public void ChooseCompanion( ) {
        int range = Random.Range( 0 , StaticManager.RealTime.Companions.Count - 1 );

        Companion chosenCompanion = StaticManager.RealTime.Companions[ range ];

        chosenCompanion.enemies.Add(character);

        chosenCompanion.isCaught = true;

       character.enemies.Add(chosenCompanion);

        SetState = state.ATTACKING;
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

                if ( Vector3.Distance(StaticManager.Character.transform.position, transform.position) < veiwDistance ){
                    StaticManager.RealTime.AddEnemy(GetComponent<Enemy>());
                    ChooseCompanion( );
                }
            }

                break;
            case state.FOLLOW: {
                Agent.destination = character.leader.transform.position;
            }
                break;
            case state.ATTACKING: {
                Agent.SetDestination( character.enemies[0].transform.position );

                if ( StaticManager.Utility.NavDistanceCheck(Agent) == DistanceCheck.HAS_REACHED_DESTINATION ){
                    SetState = state.FREEZE;

                    character.AnimationClass.Play(AnimationClass.states.Attacking);
                    character.enemies[0].AnimationClass.Play(AnimationClass.states.DamageText);
                    character.enemies[0].damageText.text = StaticManager.DamageCalc.CalcAttack(character.enemies[0].stats, character.stats).ToString();
                    character.enemies[0].damage = (int)StaticManager.DamageCalc.CalcAttack(character.enemies[0].stats, character.stats);
                    character.enemies[0].damageText.text = character.enemies[0].damage.ToString();
                    }

                }
                break;
            case state.FREEZE: {
                if ( character.enemies[ 0 ] == null ){
                    character.enemies.RemoveAt( 0 );
                    ChooseCompanion( );
                }
                else{
                    transform.LookAt( character.enemies[ 0 ].transform.position );

                }
            }

                break;

            default:

                break;
        }
    }

}