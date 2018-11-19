﻿using System.Collections.Generic;

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
    //List of enemies to attack
    public List<Enemy> enemiesToAttack;
    public void Start( ) {
        base.Start( );
        enemiesToAttack = new List<Enemy>();
        SetState          = state.IDLE;
        character         = GetComponent < Companion >( );
        randDistance = Random.Range( 1.5f, 1.5f + 2);
        battleDistance = 4;
        des = LineManager.assignIndex( );
        companion = GetComponent<Companion>();
        SetAgreesionState = AggressionStates.PASSIVE;
    }

    //Handles assigning enemies for companion
    public AggressionStates SetAgreesionState
    {
        get { return aggState; }
        set
        {
            enemiesToAttack.Clear();
            if (value == AggressionStates.BERZERK)
            {
                LayerMask mask = LayerMask.NameToLayer("Enemy");
                Collider[] colliders = Physics.OverlapSphere(transform.position, 20);
                foreach (var collider1 in colliders)
                {
                    if (collider1.tag == "Enemy")
                    {
                        enemiesToAttack.Add(collider1.GetComponent<Enemy>());

                    }
                }

                character.enemy = enemiesToAttack[0];
                aggState = AggressionStates.BERZERK;
            }
            else if (value == AggressionStates.DEFEND)
            {
                if (StaticManager.Character.attackers.Count <= 0)
                {
                    aggState = AggressionStates.PASSIVE;
                }
                else
                {
                    character.enemy = StaticManager.Character.attackers[Random.Range(1, StaticManager.Character.attackers.Count)];
                }
                /// StaticManger.Character.attackers .. pick random to attack from list
                // or if there are none then passive
            }
            else if (value == AggressionStates.PASSIVE)
            {
                aggState = AggressionStates.PASSIVE;
            }
            else if (value == AggressionStates.PROVOKED)
            {
                // if character.attackers. count > 0 ... attack random index
                // if there are none, then defend
            }
        }

    }

    // What happens to enemies when it attacks
    protected override void Update( ) {
        switch ( aggState ){



            case AggressionStates.PASSIVE:
            {
                Agent.destination = Character.Player.transform.position + (Vector3.right * des);
            }
                break;
            case AggressionStates.BERZERK:
            {
                enemiesToAttack.RemoveAll(item => item == null);
                if (enemiesToAttack.Count  > 0 && !character.enemy)
                {
                    character.enemy = enemiesToAttack[0];
                }
                if (character.attackers.Count <= 0)
                {
                    SetAgreesionState = AggressionStates.PASSIVE;
                    // if breaks, switch to passive
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
                if (!character.enemy && StaticManager.Character.attackers.Count <= 0)
                {
                   // Debug.Log("now im passive");
                    SetAgreesionState = AggressionStates.PASSIVE;
                }
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