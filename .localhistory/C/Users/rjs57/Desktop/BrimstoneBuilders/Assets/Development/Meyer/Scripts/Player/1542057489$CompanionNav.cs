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

    public enum AggressionStates { BERZERK, PASSIVE, DEFEND, PROVOKED }

    public AggressionStates aggState;
    public companionBehaviors behaviors;
    public void Start( ) {
        base.Start( );
        SetState          = state.IDLE;
        character         = GetComponent < Companion >( );
        randDistance = Random.Range( 1.5f, 1.5f + 2);
        battleDistance = 4;
        des = LineManager.assignIndex( );
        companion = GetComponent<Companion>();
        SetAgreesionState = AggressionStates.PASSIVE;
    }

    public AggressionStates SetAgreesionState
    {
        get { return aggState; }
        set
        {
            character.attackers.Clear();
            if (value == AggressionStates.BERZERK)
            {
                LayerMask mask = LayerMask.NameToLayer("Enemy");
                Collider[] colliders = Physics.OverlapSphere(transform.position, 20, mask);
                foreach (var collider1 in colliders)
                {
                    character.attackers.Add(collider1.GetComponent<Enemy>());
                }

                character.enemy = character.attackers[0];
                aggState = AggressionStates.BERZERK;
            }
            else if (value == AggressionStates.DEFEND)
            {

            }
            else if (value == AggressionStates.PASSIVE)
            {
                aggState = AggressionStates.PASSIVE;
            }
            else if (value == AggressionStates.PROVOKED)
            {

            }
        }

    }
    protected override void Update( ) {
        switch ( aggState ){



            case AggressionStates.PASSIVE:
            {
                Agent.destination = Character.Player.transform.position + (Vector3.right * des);
            }
                break;
            case AggressionStates.BERZERK:
            {
                if (character.attackers.Count <= 0)
                {
                    SetAgreesionState = AggressionStates.BERZERK;
                }
                    Agent.SetDestination(character.enemy.transform.position);
                    float dist = Vector3.Distance(transform.position, character.enemy.transform.position);
                    transform.LookAt(character.enemy.transform.position);

                    if (dist < 80)
                    {
                        character.AnimationClass.Play(AnimationClass.states.AttackTrigger);
                        character.attachedWeapon.AnimationClass.Play(AnimationClass.weaponstates.EnabledTrigger);
                    }

                }
                break;
            case AggressionStates.DEFEND:
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
            case AggressionStates.PROVOKED:
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