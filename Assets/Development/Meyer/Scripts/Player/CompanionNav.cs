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

    private void Update( )
    {
        if (character.enemies.Count == 0)
        {
            SetState = state.FOLLOW;
        }
        switch ( State ){
            case state.FOLLOW: {
                if(Agent.isOnNavMesh)
                {
                     Agent.destination = Character.Player.transform.position;
                }
               

                if ( character.enemies.Count > 0 ){
                    SetState = state.ATTACKING;
                }
                //if the main player has enemys and this character is not attacking
                else if ( StaticManager.Character.enemies.Count > 0 ){
                    //main players enemys become this characters enemy
                    character.enemies.Add(StaticManager.Character.enemies[0]);
                    character.enemies[ 0 ].enemies.Remove( character );
                    character.enemies[0].enemies.Insert(0, character);
                }
            }

                break;
            case state.ATTACKING: {
                    //Removes the current enemy that this character is attacking from attack list if null

                    if (character.enemies[0] == null)
                    {
                        character.enemies.RemoveAt(0);
                    }

                    //if the enemy is not attacking and has enemies to attack
                    if (character.enemies.Count > 0 && !character.AnimationClass.animation.GetBool("Attacking"))
                    {
                        //set the destination to the first enemy in attack list
                        Agent.SetDestination(character.enemies[0].transform.position);

                        //if it reached the first enemy in list, then attack
                        if (StaticManager.Utility.NavDistanceCheck(Agent) == DistanceCheck.HAS_REACHED_DESTINATION)
                        {
                            character.AnimationClass.Play(AnimationClass.states.Attacking);
                        }
                    }
                    //if this character has enemys and is attacking
                    else if (character.enemies.Count > 0)
                    {
                        //look at enemy
                        transform.LookAt(character.enemies[0].transform.position);
                    }
                
                SetState = state.ATTACKING;
            }

                break;
            default:

                break;
        }
    }

}