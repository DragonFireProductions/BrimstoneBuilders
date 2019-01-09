using UnityEngine;

public class AnimationClass : MonoBehaviour {

    public enum states {

        AttackTrigger ,

        Idle ,

        Attack ,

        Walk

    }

    public enum WeaponType {

        Bow ,

        Sword

    }

    public enum weaponstates {

        EnabledTrigger

    }

    [ SerializeField ] public Animator animation;

    public void SwitchWeapon( WeaponObject switchTo ) {
        if ( switchTo.type == SubClasses.Types.MELEE ){
            animation.runtimeAnimatorController = ( RuntimeAnimatorController )Resources.Load( "Animations/SwordCharacter" , typeof( RuntimeAnimatorController ) );
        }
       
        if ( switchTo.type == SubClasses.Types.RANGE ){
            animation.runtimeAnimatorController = ( RuntimeAnimatorController )Resources.Load( "Animations/BowCharacter" , typeof( RuntimeAnimatorController ) );
        }
    }

    public void Start( ) {
        animation = gameObject.GetComponent < Animator >( );
    }

    public void Play( states state ) {
        animation.SetBool( state.ToString( ) , true );
    }

    public void Play( weaponstates state ) {
        animation.SetBool( state.ToString( ) , true );
    }

    public void Stop( states state ) {
        animation.SetBool( state.ToString( ) , false );
    }

    public void Stop( weaponstates state ) {
        animation.SetBool( state.ToString( ) , false );
    }

}