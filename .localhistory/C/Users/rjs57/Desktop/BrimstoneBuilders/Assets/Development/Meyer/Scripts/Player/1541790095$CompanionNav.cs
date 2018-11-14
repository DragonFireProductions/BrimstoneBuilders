using System.Collections.Generic;

using Assets.Meyer.TestScripts.Player;

using Kristal;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CompanionNav : BaseNav {

    private float randDistance;

    private int des;

    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private Button berzerk;
    [SerializeField] private Button passive;
    [SerializeField] private Button chill;
    [SerializeField] private Button provoked;

    public void Start( ) {
        base.Start( );
        SetState          = state.IDLE;
        character         = GetComponent < Companion >( );
        randDistance = Random.Range( 1.5f, 1.5f + 2);
        battleDistance = 4;
        des = LineManager.assignIndex( );
        
    }

    void awake()
    {
        //text = GetComponent<TextMeshProUGUI>();
        //berzerk = GetComponent<Button>();
        //passive = GetComponent<Button>();
        //chill = GetComponent<Button>();
        //provoked = GetComponent<Button>();
    }

    protected override void Update( ) {

        //if (StaticManager.Character.stats.Health <= 0)
        //{
        //    text.enabled = false;
        //    berzerk.enabled = false;
        //    passive.enabled = false;
        //    chill.enabled = false;
        //    provoked.enabled = false;
        //}
        switch ( State ){
            case state.IDLE: {
                Agent.destination = Character.Player.transform.position + (Vector3.right  * des);
                        }

                break;
            case state.ATTACKING:
                {
                    character.attackers.RemoveAll(item => item == null);
                    StaticManager.RealTime.Companions.RemoveAll(item => item == null);
                    StaticManager.RealTime.Enemies.RemoveAll(item => item == null);

                    //if current attacker dies and someone is still attacking
                    if (character.attackers.Count > 0 && character.enemy == null)
                    {
                        Debug.Log("need to shift my focus");
                        var enemy = character.attackers[Random.Range(0, character.attackers.Count)];
                        character.enemy = enemy;
                        enemy.attackers.Add(character);
                    }
                    //if no one is attacking current character and still enemies in the scene
                    else if (StaticManager.RealTime.GetCount(character) && character.enemy == null)
                    {
                        Debug.Log("there's still enemies that need to be taken out");
                        var enemy = StaticManager.RealTime.getnewType(character);
                        character.enemy = enemy;
                        enemy.attackers.Add(character);
                    }
                    //no enemies are alive
                    else if (character.enemy == null && character is Companion)
                    {
                        StaticManager.RealTime.Attacking = false;
                        SetState = state.IDLE;
                        return;
                    }
                    SetState = state.ATTACKING;
                    Agent.SetDestination(character.enemy.transform.position);
                    transform.LookAt(character.enemy.transform);
                    float distance = Vector3.Distance(transform.position, character.enemy.transform.position);

                    if (distance < battleDistance)
                    {
                        character.AnimationClass.Play(AnimationClass.states.AttackTrigger);
                        character.attachedWeapon.AnimationClass.Play(AnimationClass.weaponstates.EnabledTrigger);
                    }
                }

                break;
            default:

                break;
        }
    }

}