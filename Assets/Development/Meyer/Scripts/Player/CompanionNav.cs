using System.Collections.Generic;

using Assets.Meyer.TestScripts.Player;

using Kristal;

public class CompanionNav : BaseNav {

    protected Companion character;

    public void Start( ) {
        base.Start( );
        SetState          = state.FOLLOW;
        character         = GetComponent < Companion >( );
        character.enemies = new List < Enemy >( );
    }

    private void Update( ) {
        switch ( State ){
            case state.FOLLOW: {
                Agent.destination = Character.Player.transform.position;

                if ( character.isCaught ){
                    SetState = state.ATTACKING;
                }
            }

                break;
            case state.ATTACKING: {
                Agent.SetDestination( character.enemies[ 0 ].transform.position );

                if ( StaticManager.Utility.NavDistanceCheck( Agent ) == DistanceCheck.HAS_REACHED_DESTINATION ){
                    SetState = state.FREEZE;
                    character.AnimationClass.Play( AnimationClass.states.Attacking );

                    character.enemies[ 0 ].AnimationClass.Play( AnimationClass.states.DamageText );
                    character.enemies[ 0 ].damageText.text = StaticManager.DamageCalc.CalcAttack( character.enemies[ 0 ].stats , character.stats ).ToString( );
                    character.enemies[ 0 ].damage          = ( int )StaticManager.DamageCalc.CalcAttack( character.enemies[ 0 ].stats , character.stats );
                    character.enemies[ 0 ].damageText.text = character.enemies[ 0 ].damage.ToString( );
                }
            }

                break;
            case state.FREEZE: {
                if ( character.enemies[ 0 ] == null ){
                    character.enemies.RemoveAt( 0 );
                    SetState = state.ATTACKING;
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