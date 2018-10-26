using UnityEngine;

namespace Kristal {

    public class Enemy : BaseCharacter {

        [ SerializeField ] private GameObject attachedWeapon;

        // Use this for initialization
        protected void Awake( ) {
            base.Awake( );
        }

        protected void Start( ) {
            material.color = BaseColor;
            Nav            = gameObject.GetComponent < EnemyNav >( );
        }

        public override void Damage( BaseCharacter attacker ) { }

        public void Remove( BaseCharacter chara ) { }

    }

}