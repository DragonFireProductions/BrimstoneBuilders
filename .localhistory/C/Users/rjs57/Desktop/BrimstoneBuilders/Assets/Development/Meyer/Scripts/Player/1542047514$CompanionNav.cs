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

            //case state.ATTACKING:
            //    {
            //        character.attackers.RemoveAll(item => item == null);
            //        StaticManager.RealTime.Companions.RemoveAll(item => item == null);
            //        StaticManager.RealTime.Enemies.RemoveAll(item => item == null);

            //        //if current attacker dies and someone is still attacking
            //        if (character.attackers.Count > 0 && character.enemy == null)
            //        {
            //            Debug.Log("need to shift my focus");
            //            var enemy = character.attackers[Random.Range(0, character.attackers.Count)];
            //            character.enemy = enemy;
            //            enemy.attackers.Add(character);
            //        }
            //        //if no one is attacking current character and still enemies in the scene
            //        else if (StaticManager.RealTime.GetCount(character) && character.enemy == null)
            //        {
            //            Debug.Log("there's still enemies that need to be taken out");
            //            var enemy = StaticManager.RealTime.getnewType(character);
            //            character.enemy = enemy;
            //            enemy.attackers.Add(character);
            //        }
            //        //no enemies are alive
            //        else if (character.enemy == null && character is Companion)
            //        {
            //            StaticManager.RealTime.Attacking = false;
            //            SetState = state.IDLE;
            //            return;
            //        }
            //        SetState = state.ATTACKING;
            //        Agent.SetDestination(character.enemy.transform.position);
            //        transform.LookAt(character.enemy.transform);
            //        float distance = Vector3.Distance(transform.position, character.enemy.transform.position);

            //        if (distance < battleDistance)
            //        {
            //            character.AnimationClass.Play(AnimationClass.states.AttackTrigger);
            //            character.attachedWeapon.AnimationClass.Play(AnimationClass.weaponstates.EnabledTrigger);
            //        }
            //    }
            //    break;
            case Companion.AggressionStates.PASSIVE:
            {
                Agent.destination = Character.Player.transform.position + (Vector3.right * des);
            }
                break;
            case Companion.AggressionStates.BERZERK:
            {
                    Debug.Log("in berzerk state now");
                for (int i = 0; i < character.attackers.Count; ++i)
                {
                    Agent.SetDestination(character.attackers[i].transform.position);
                }
            }
                break;
            case Companion.AggressionStates.DEFEND:
            {
                var enemy = character.attackers[Random.Range(0, character.attackers.Count)];
                    character.enemy = enemy;
                    enemy.attackers.Add(character);
                }
                break;
            case Companion.AggressionStates.PROVOKED:
            {
                var enemy = character.attackers[Random.Range(0, character.attackers.Count)];
                    character.enemy = enemy;
                    enemy.attackers.Add(character);
                }
                break;
            default:

                break;
        }
    }

}