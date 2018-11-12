using System.Collections.Generic;

using Assets.Meyer.TestScripts.Player;

using Kristal;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CompanionNav : BaseNav {

    private float randDistance;

    private int des;

    public Companion companion;
    private int currenemy;


    public void Start( ) {
        base.Start( );
        SetState          = state.IDLE;
        character         = GetComponent < Companion >( );
        randDistance = Random.Range( 1.5f, 1.5f + 2);
        battleDistance = 4;
        des = LineManager.assignIndex( );
        companion = GetComponent<Companion>();
    }

    protected override void Update( ) {
        switch ( companion.aggState ){



            case Companion.AggressionStates.PASSIVE:
            {
                Agent.destination = Character.Player.transform.position + (Vector3.right * des);
            }
                break;
            case Companion.AggressionStates.BERZERK:
            {
                    //    Debug.Log("in berzerk state now");
                currenemy = character.attackers.Count;
                Debug.Log(character.attackers.Count);
                    Agent.SetDestination(character.attackers[currenemy].transform.position);
                    float dist = Vector3.Distance(transform.position, character.attackers[currenemy].transform.position);
                    if (dist < battleDistance)
                    {
                        character.AnimationClass.Play(AnimationClass.states.AttackTrigger);
                        character.attachedWeapon.AnimationClass.Play(AnimationClass.weaponstates.EnabledTrigger);
                    }

                }
                break;
            case Companion.AggressionStates.DEFEND:
            {
                //Agent.destination = Character.Player.transform.position + (Vector3.right * des);
                //float dist = Vector3.Distance(transform.position, character.attackers[Random.Range(0, )].transform.position);

                //    if (dist < battleDistance)
                //    {
                //        transform.LookAt(character.attackers[currenemy].transform.position, Vector3.up);
                //    character.AnimationClass.Play(AnimationClass.states.AttackTrigger);
                //    character.attachedWeapon.AnimationClass.Play(AnimationClass.weaponstates.EnabledTrigger);
                //    }

                }
                break;
            case Companion.AggressionStates.PROVOKED:
            {
                //var enemy = character.attackers[Random.Range(0, character.attackers.Count)];
                //    character.enemy = enemy;
                //    enemy.attackers.Add(character);
            }
                break;
            default:

                break;
        }
    }

}