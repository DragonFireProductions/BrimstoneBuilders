using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.AI;

public class BaseNav : MonoBehaviour {

    public enum state {

        IDLE ,

        ATTACKING ,

        FOLLOW ,

        MOVE ,

        ENEMY_CLICKED ,

        FREEZE

    }

    public NavMeshAgent Agent;

    public float battleDistance = 3;

    public BaseCharacter character;

    public AudioClip clip;

    protected float innerRadius = 8f;

    protected Vector3 newpos;

    protected float outerRadius = 10f;

    [ HideInInspector ] protected state State;

    [ SerializeField ] public float stoppingDistance;

    [ SerializeField ] protected float waittime = 0.5f;

    protected float timer = 0;

    protected float speed;

    protected Vector3 lastPosition;

    protected AudioSource audio;

    protected float dist;

    protected float distance;

    public void RunAway( ) {
        SetState = state.IDLE;
        StartCoroutine( Run( ) );
    }

    IEnumerator Run( ) {
        yield return new WaitForSeconds(4);

        SetState = state.ATTACKING;
    }
    public state SetState {
        get { return State; }
        set
        {
            Agent.isStopped = false;

            if ( value == state.FOLLOW ){
                Agent.stoppingDistance = 8;
            }

            if ( value == state.ATTACKING ){
                character.canvas.SetActive(true);
                Agent.stoppingDistance = 3;
                newpos = StaticManager.Utility.randomInsideDonut(outerRadius, innerRadius, StaticManager.Character.transform.position);
            }

            if ( value == state.MOVE ){
                Agent.stoppingDistance = 2;
                Agent.isStopped        = false;
            }

            if ( value == state.ENEMY_CLICKED ){
                Agent.stoppingDistance = 8;
            }
            if ( value == state.IDLE ){
                 character.canvas.SetActive(false);
                Agent.stoppingDistance = 0;
            }

            if(value == state.FREEZE)
            {
                Agent.isStopped = true;
            }

            State = value;
        }
    }

    // Use this for initialization
    protected virtual void Start( ) {
        Agent.stoppingDistance = stoppingDistance;
        audio = gameObject.AddComponent<AudioSource>();
    }
    void FixedUpdate()
    {
        speed = Mathf.Lerp(speed, (transform.position - lastPosition).magnitude / Time.deltaTime, 0.75f);
        lastPosition = transform.position;
       
    }
    // Update is called once per frame
    protected virtual void Update( ) {
        if ( character.enemy ){
            var a = new Vector3(transform.position.x, 0, transform.position.z);
            var b = new Vector3(character.enemy.transform.position.x, 0, character.enemy.transform.position.z);
            distance = Vector3.Distance(a, b);
        }
       
        //character.AnimationClass.animation.SetFloat("Walk", 3);
        float dist = Agent.velocity.magnitude / Agent.speed;
        audio.playOnAwake = false;
        audio.clip = clip;
        if (dist != 0 && !audio.isPlaying)
        {
            audio.PlayOneShot(audio.clip, 1.0f);
        }
        switch ( State ){
            
            default:

                break;
        }
    }

}