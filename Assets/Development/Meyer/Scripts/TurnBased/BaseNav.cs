using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BaseNav : MonoBehaviour {

	public NavMeshAgent Agent;

	[ SerializeField ] public float stoppingDistance = 0;

	[SerializeField] protected state State;

	public BaseCharacter character;

	public float battleDistance = 3;
	// Use this for initialization
	protected void Start () {
		Agent = gameObject.GetComponent < NavMeshAgent >( );
		stoppingDistance = 3;
		Agent.stoppingDistance = stoppingDistance;
		character = gameObject.GetComponent < BaseCharacter >( );
	}

	// Update is called once per frame
	protected virtual void Update () {
        switch (State)
        {
            case state.ATTACKING:
                {
			        character.attackers.RemoveAll(item => item == null );
					StaticManager.RealTime.Companions.RemoveAll(item => item == null  );
	                StaticManager.RealTime.Enemies.RemoveAll( item => item == null );

                    //if current attacker dies and someone is still attacking
                    if (character.attackers.Count > 0 && character.enemy == null)
                    {
                        var enemy = character.attackers[Random.Range(0, character.attackers.Count)];
                        character.enemy = enemy;
                        enemy.attackers.Add(character);
                    }
                    //if no one is attacking current character and still enemies in the scene
                    else if (StaticManager.RealTime.GetCount(character) && character.enemy == null){
	                    var enemy = StaticManager.RealTime.getnewType( character );
                        character.enemy = enemy;
                        enemy.attackers.Add(character);
                    }
                    //no enemies are alive
                    else if (character.enemy == null && character is Companion){
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
    
    public enum state { IDLE, ATTACKING, FOLLOW, MOVE, ENEMY_CLICKED, FREEZE }
    public state SetState
    {
        get { return State; }
	    set {
		    if ( value == state.FOLLOW ){
			    Agent.stoppingDistance = stoppingDistance;
		    }

		    if ( value == state.ATTACKING ){
			    Agent.stoppingDistance = stoppingDistance;
		    }

		    if ( value == state.MOVE ){
			    Agent.stoppingDistance = 2;
		    }

		    if ( value == state.ENEMY_CLICKED ){
			    Agent.stoppingDistance = stoppingDistance;
		    }

		    if ( value == state.IDLE ){
			    Agent.stoppingDistance = stoppingDistance;
		    }


		    State = value;
	    }
    }

}
