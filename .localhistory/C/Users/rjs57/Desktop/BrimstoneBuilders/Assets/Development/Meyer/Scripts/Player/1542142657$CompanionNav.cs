using System.Collections.Generic;
using System.Net;
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
                enemiesToAttack = StaticManager.RealTime.AllEnemies;
                character.enemy = enemiesToAttack[0];
                aggState = AggressionStates.BERZERK;

            }
            else if (value == AggressionStates.DEFEND)
            {
                enemiesToAttack.Clear();
                LayerMask mask = LayerMask.NameToLayer("Enemy");
                Collider[] colliders = Physics.OverlapSphere(transform.position, 10);
                foreach (var collider in colliders)
                {
                    if (collider.tag == "Enemy")
                    {
                        enemiesToAttack.Add(collider.GetComponent<Enemy>());
                    }
                }

                character.enemy = enemiesToAttack[0];
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
                LayerMask mask = LayerMask.NameToLayer("Enemy");
                Collider[] colliders = Physics.OverlapSphere(transform.position, 5.0f);
                foreach (var enemy in colliders)
                {
                    //Debug.Log(enemy.tag);
                    if (enemy.tag == "Enemy")
                    {
                        enemiesToAttack.Add(enemy.GetComponent<Enemy>());
                    }
                }
                aggState = AggressionStates.PROVOKED;
                character.enemy = enemiesToAttack[0];


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
                Debug.Log("now in the passive state");
                Agent.destination = Character.Player.transform.position + (Vector3.right * des);
            }
                break;
            case AggressionStates.BERZERK:
            {
                Debug.Log("now in berzerk state");
                    enemiesToAttack.RemoveAll(item => item == null);
                if (enemiesToAttack.Count  > 0 && !character.enemy)
                {
                    character.enemy = enemiesToAttack[0];
                    Agent.SetDestination(character.enemy.transform.position);
                }

                if (enemiesToAttack.Count == 0)
                {
                    Agent.SetDestination(StaticManager.Character.transform.position);
                    return;
                }
                float dist = Vector3.Distance(transform.position, character.enemy.transform.position);
                transform.LookAt(character.enemy.transform.position);
                if (dist < battleDistance)
                {
                    character.AnimationClass.Play(AnimationClass.states.AttackTrigger);
                    character.attachedWeapon.AnimationClass.Play(AnimationClass.weaponstates.EnabledTrigger);
                }

                }
                break;
            case AggressionStates.DEFEND:
            {
                Debug.Log("now in the defend state");
                    enemiesToAttack.RemoveAll(item => item == null);
                if (enemiesToAttack.Count > 0)
                {
                    character.enemy = enemiesToAttack[0];
                    Agent.SetDestination(character.enemy.transform.position);
                    transform.LookAt(character.enemy.transform.position);
                    float distance = Vector3.Distance(transform.position, character.enemy.transform.position);
                    if (distance < 3)
                    {
                        character.AnimationClass.Play(AnimationClass.states.AttackTrigger);
                        character.attachedWeapon.AnimationClass.Play(AnimationClass.weaponstates.EnabledTrigger);
                    }
                    //SetAgreesionState = AggressionStates.PASSIVE;
                }
                else if (character.attackers.Count == 0)
                {
                    Agent.destination = Character.Player.transform.position + (Vector3.right * des);
                        //SetAgreesionState = AggressionStates.PASSIVE;
                }

            }
                break;
            case AggressionStates.PROVOKED:
            {
                    Debug.Log("now in the provoked state");
                    enemiesToAttack.RemoveAll(item => item == null);
                    if (enemiesToAttack.Count > 0)
                    {
                        character.enemy = enemiesToAttack[0];
                        Agent.SetDestination(character.enemy.transform.position);
                        transform.LookAt(character.enemy.transform.position);
                        character.AnimationClass.Play(AnimationClass.states.AttackTrigger);
                        character.attachedWeapon.AnimationClass.Play(AnimationClass.weaponstates.EnabledTrigger);
                    }
                    else
                    {
                        LayerMask mask = LayerMask.NameToLayer("Enemy");
                        Collider[] colliders = Physics.OverlapSphere(transform.position, 5.0f);
                        foreach (var enemy in colliders)
                        {
                            //Debug.Log(enemy.tag);
                            if (enemy.tag == "Enemy")
                            {
                                enemiesToAttack.Add(enemy.GetComponent<Enemy>());
                            }
                        }
                        Agent.destination = Character.Player.transform.position + (Vector3.right * des);
                    }

                }
                break;
            default:

                break;
        }
    }

}