using System.Collections;
using System.Collections.Generic;
using System.Security.Policy;

using UnityEngine;

public class AnimationClass : MonoBehaviour {

    public int damage;
    public enum states { AttackTrigger }
    public enum weaponstates { EnabledTrigger}

    [SerializeField] public Animator animation;
    [SerializeField] public string name;
    
    public void Start( ) {

        animation = gameObject.GetComponent < Animator >( );
    }

    public void Play(states state) {
        animation.SetBool(state.ToString(), true);
    }
    public void Play( weaponstates state ) {
        animation.SetBool(state.ToString(), true);
    }

    
    public void Stop(states state) {
        animation.SetBool(state.ToString(), false);
    }

    public void Stop( weaponstates state ) {
        animation.SetBool(state.ToString(), false);
    }
}
