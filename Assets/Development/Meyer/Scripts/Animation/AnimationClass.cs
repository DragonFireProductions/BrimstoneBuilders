using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationClass : MonoBehaviour
{
    
    public enum states { Attacking = 0, Idle, Dying, AttackText, Selected }

    [SerializeField] public Animator animation;
    [SerializeField] public string name;
    
    public void Start( ) {
        animation = gameObject.GetComponent < BaseCharacter >( ).animator;
    }

    public void Play(states state) {
        animation.SetBool(state.ToString(), true);
    }
    
    public void Stop(states state)
    {
        animation.SetBool(state.ToString(), false);
    }
}
