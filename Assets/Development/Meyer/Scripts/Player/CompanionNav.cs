using System.Collections.Generic;

using Assets.Meyer.TestScripts.Player;

using Kristal;

using UnityEngine;

public class CompanionNav : BaseNav
{

    private float randDistance;

    public Companion companion;

    private int currenemy;

    public enum AggressionStates
    {

        BERZERK,

        PASSIVE,

        DEFEND,

        PROVOKED

    }

    [HideInInspector] public AggressionStates aggState;

     public companionBehaviors behaviors;

    //List of enemies to attack
    [HideInInspector] public List<Enemy> enemiesToAttack;

    protected override void Start()
    {
        base.Start();
        enemiesToAttack = new List<Enemy>();
        randDistance = Random.Range(1.5f, 1.5f + 2);
        enabled = false;
        battleDistance = 4;
    }

    //Handles assigning enemies for companion
    public AggressionStates SetAgreesionState
    {
        get { return aggState; }
        set
        {
            Agent.isStopped = false;
              Agent.stoppingDistance = 8;
            if (value == AggressionStates.BERZERK){
              
                aggState = AggressionStates.BERZERK;
            }
            else if (value == AggressionStates.DEFEND)
            {
                aggState = AggressionStates.DEFEND;
                
            }
            else if (value == AggressionStates.PASSIVE)
            {
                aggState = AggressionStates.PASSIVE;
            }
            else if (value == AggressionStates.PROVOKED)
            {
                if (character.attackers.Count > 0)
                {
                    character.enemy = character.attackers[0];
                }

                aggState = AggressionStates.PROVOKED;
                
            }
        }
    }

    private void FixedUpdate()
    {
        speed = Mathf.Lerp(speed, (transform.position - lastPosition).magnitude / Time.deltaTime, 0.75f);
        lastPosition = transform.position;
    }

    // What happens to enemies when it attacks
    protected override void Update()
    {
        base.Update();
        character.AnimationClass.animation.SetFloat("Walk", Agent.velocity.magnitude / Agent.speed);
        Debug.Log("Enabled");
        character.attackers.RemoveAll(item => item == null);
        enemiesToAttack.RemoveAll(item => item == null);


        stoppingDistance = 4;
        switch (aggState)
        {
            case AggressionStates.PASSIVE:
                {
                    Agent.destination = StaticManager.Character.transform.position;
                }

                break;
            case AggressionStates.BERZERK:
                {
                    enemiesToAttack = StaticManager.RealTime.AllEnemies;
                    enemiesToAttack.RemoveAll(item => item == null);

                    if (enemiesToAttack.Count > 0)
                    {
                        if (!character.enemy)
                        {
                            character.enemy = enemiesToAttack[0];
                        }

                        if (character.attachedWeapon is GunType)
                        {
                            if (StaticManager.Utility.NavDistanceCheck(Agent) == DistanceCheck.HAS_REACHED_DESTINATION)
                            {
                                timer += Time.deltaTime;

                                if (timer > waittime)
                                {
                                    newpos = StaticManager.Utility.randomInsideDonut(outerRadius, innerRadius, character.enemy.transform.position);
                                    timer = 0;
                                }
                            }

                            Vector3 look = new Vector3(character.enemy.transform.position.x, transform.position.y, character.enemy.transform.position.z);
                            transform.LookAt(look);
                            character.attachedWeapon.Use();
                            Agent.SetDestination(newpos);
                        }
                        else
                        {
                                Agent.SetDestination(character.enemy.transform.position);

                            if (enemiesToAttack.Count == 0)
                            {
                                Agent.SetDestination(StaticManager.Character.transform.position);

                                return;
                            }

                            if (distance < battleDistance)
                            {
                                character.AnimationClass.Play(AnimationClass.states.Attack);
                                Vector3 look = new Vector3(character.enemy.transform.position.x, transform.position.y, character.enemy.transform.position.z);
                                transform.LookAt(look);
                            }
                        }
                    }
                    else
                    {
                        Agent.SetDestination(StaticManager.Character.transform.position);
                    }
                }

                break;
            case AggressionStates.DEFEND:
                {
                    StaticManager.Character.attackers.RemoveAll(item => item == null);

                    if (StaticManager.Character.attackers.Count > 0)
                    {
                        character.enemy = StaticManager.Character.attackers[0];

                        if (character.attachedWeapon is GunType)
                        {
                            if (StaticManager.Utility.NavDistanceCheck(Agent) == DistanceCheck.HAS_REACHED_DESTINATION)
                            {
                                timer += Time.deltaTime;

                                if (timer > waittime)
                                {
                                    newpos = StaticManager.Utility.randomInsideDonut(outerRadius, innerRadius, character.enemy.transform.position);
                                    timer = 0;
                                }
                            }

                            Vector3 look = new Vector3(character.enemy.transform.position.x, transform.position.y, character.enemy.transform.position.z);
                            transform.LookAt(look);
                            character.attachedWeapon.Use();
                            Agent.SetDestination(newpos);
                        }
                        else
                        {
                                Agent.SetDestination(character.enemy.transform.position);
                            Vector3 look = new Vector3(character.enemy.transform.position.x, transform.position.y, character.enemy.transform.position.z);
                            transform.LookAt(look);

                            if (distance < battleDistance)
                            {
                                character.AnimationClass.Play(AnimationClass.states.Attack);
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
                    character.attackers.RemoveAll(item => item == null);

                    if (character.attackers.Count > 0)
                    {
                        character.enemy = character.attackers[0];

                        if (character.attachedWeapon is GunType)
                        {
                            if (StaticManager.Utility.NavDistanceCheck(Agent) == DistanceCheck.HAS_REACHED_DESTINATION)
                            {
                                timer += Time.deltaTime;

                                if (timer > waittime)
                                {
                                    newpos = StaticManager.Utility.randomInsideDonut(outerRadius, innerRadius, character.enemy.transform.position);
                                    timer = 0;
                                }
                            }

                            transform.LookAt(character.enemy.transform);
                            character.attachedWeapon.Use();
                            Agent.SetDestination(newpos);
                        }
                        else
                        {
                                Agent.SetDestination(character.enemy.transform.position);

                            Vector3 look = new Vector3(character.enemy.transform.position.x, transform.position.y, character.enemy.transform.position.z);
                            transform.LookAt(look);

                            if ( distance < battleDistance ){
                                character.AnimationClass.Play(AnimationClass.states.Attack);
                            }
                            
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