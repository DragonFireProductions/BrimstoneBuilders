using System.Collections;
using System.Collections.Generic;
using System.Security.Policy;

using UnityEngine;

public class AnimationClass : MonoBehaviour {

    public int damage;
    public enum states { AttackTrigger, Idle, Attack, Walk }

    public enum WeaponType { Bow, Sword}
    public enum weaponstates { EnabledTrigger}

    [SerializeField] public Animator animation;
    [SerializeField] public string name;

    [ SerializeField ] public Animator swordAnimator;

    [ SerializeField ] public Animator bowAnimator;

    public void SwitchWeapon(WeaponObject switchTo ) {
        if ( switchTo.type == SubClasses.Types.MELE ){
            animation.runtimeAnimatorController = ( RuntimeAnimatorController )Resources.Load( "SwordCharacter" , typeof( RuntimeAnimatorController ) );
        }
        if (switchTo.type == SubClasses.Types.RANGE){
            animation.runtimeAnimatorController = ( RuntimeAnimatorController )Resources.Load( "BowCharacter" , typeof( RuntimeAnimatorController ) );
        }
    }

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
