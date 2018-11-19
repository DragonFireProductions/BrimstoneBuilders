﻿using System.Collections.Generic;
using System.Net;
using Assets.Meyer.TestScripts.Player;

using Kristal;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CompanionNav : BaseNav {

    private float randDistance;
    

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
        companion = GetComponent<Companion>();
        SetAgreesionState = AggressionStates.PASSIVE;
    }

    //Handles assigning enemies for companion
    public AggressionStates SetAgreesionState
    {
        get { return aggState; }
        set {
            Agent.isStopped = false;
            if (value == AggressionStates.BERZERK)
            {
                aggState = AggressionStates.BERZERK;
            }
            else if (value == AggressionStates.DEFEND)
            {

                aggState = AggressionStates.DEFEND;

                /// StaticManger.Character.attackers .. pick random to attack from list
                // or if there are none then passive
            }
            else if (value == AggressionStates.PASSIVE)
            {
                aggState = AggressionStates.PASSIVE;
            }
            else if (value == AggressionStates.PROVOKED)
            {
                //LayerMask mask = LayerMask.NameToLayer("Enemy");
                //Collider[] colliders = Physics.OverlapSphere(transform.position, 5.0f);
                if (character.attackers.Count > 0)
                {
                character.enemy = character.attackers[0];

                }
                aggState = AggressionStates.PROVOKED;


                // if character.attackers. count > 0 ... attack random index
                // if there are none, then defend
            }
        }

    }

    // What happens to enemies when it attacks
    protected override void Update( ) {
        character.attackers.RemoveAll( item => item == null );
        enemiesToAttack.RemoveAll( item => item == null );
        switch ( aggState ){



            case AggressionStates.PASSIVE:
            {
                Debug.Log("now in the passive state");
                Agent.destination = Character.Player.transform.position;
            }
                break;
            case AggressionStates.BERZERK:
            {
               
                Debug.Log("now in berzerk state");
                enemiesToAttack = StaticManager.RealTime.AllEnemies;
                enemiesToAttack.RemoveAll(item => item == null);

                if ( enemiesToAttack.Count > 0 && !character.enemy ){
                    character.enemy = enemiesToAttack[ 0 ];

                    if ( character.attachedWeapon is GunType ){
                        if ( Vector3.Distance( StaticManager.Character.transform.position , gameObject.transform.position ) > 8 ){
                            Agent.isStopped = false;
                            Agent.SetDestination( StaticManager.Character.transform.position );
                        }
                        else{
                            Agent.isStopped = true;
                        }

                        transform.LookAt( character.enemy.transform );
                        character.attachedWeapon.Attack( );
                    }
                    else{
                        if ( character.enemy ){
                            Agent.SetDestination( character.enemy.transform.position );

                        }

                        if ( enemiesToAttack.Count == 0 ){
                            Agent.SetDestination( StaticManager.Character.transform.position );

                            return;
                        }

                        float dist = Vector3.Distance( transform.position , character.enemy.transform.position );

                        if ( dist < battleDistance ){
                            character.AnimationClass.Play( AnimationClass.states.AttackTrigger );
                            character.attachedWeapon.AnimationClass.Play( AnimationClass.weaponstates.EnabledTrigger );
                            transform.LookAt( character.enemy.transform.position );

                        }

                    }
                }
                else{
                    Agent.SetDestination( StaticManager.Character.transform.position );
                }
            }
                   
                break;
            case AggressionStates.DEFEND:
            {
                Debug.Log("now in the defend state");
                StaticManager.Character.attackers.RemoveAll( item => item == null );
                if (StaticManager.Character.attackers.Count > 0)
                {
                    character.enemy = StaticManager.Character.attackers[0];

                    if ( character.attachedWeapon is GunType ){
                        if ( Vector3.Distance(StaticManager.Character.transform.position, gameObject.transform.position) > 8 ){
                            Agent.isStopped = false;
                            Agent.SetDestination( StaticManager.Character.transform.position );
                        }
                        else{
                            Agent.isStopped = true;
                        }
                        transform.LookAt(character.enemy.transform);
                        character.attachedWeapon.Attack();
                    }
                    else{
                            Agent.SetDestination(character.enemy.transform.position);
                            transform.LookAt(character.enemy.transform.position);
                            float distance = Vector3.Distance(transform.position, character.enemy.transform.position);
                            if (distance < 3)
                            {
                                character.AnimationClass.Play(AnimationClass.states.AttackTrigger);
                                character.attachedWeapon.AnimationClass.Play(AnimationClass.weaponstates.EnabledTrigger);
                            }
                        }
                  
                    //SetAgreesionState = AggressionStates.PASSIVE;
                }
                else if (StaticManager.Character.attackers.Count == 0)
                {
                    Agent.destination = Character.Player.transform.position;
                }

            }
                break;
            case AggressionStates.PROVOKED:
            {
                    Debug.Log("now in the provoked state");
                character.attackers.RemoveAll(item => item == null);
                    if (character.attackers.Count > 0)
                    {
                        character.enemy = character.attackers[0];

                        if (character.attachedWeapon is GunType)
                        {
                            if (Vector3.Distance(StaticManager.Character.transform.position, gameObject.transform.position) > 8)
                            {
                                Agent.isStopped = false;
                                Agent.SetDestination(StaticManager.Character.transform.position);
                            }
                            else
                            {
                                Agent.isStopped = true;
                            }
                            transform.LookAt(character.enemy.transform);
                            character.attachedWeapon.Attack();
                        }
                        else{
                            Agent.SetDestination( character.enemy.transform.position );
                            transform.LookAt( character.enemy.transform.position );
                            character.AnimationClass.Play( AnimationClass.states.AttackTrigger );
                            character.attachedWeapon.AnimationClass.Play( AnimationClass.weaponstates.EnabledTrigger );
                        }
                    }
                    else
                    {

                        Agent.destination = Character.Player.transform.position;
                    }

                }
                break;
            default:

                break;
        }
    }

}