using System.Collections;
using System.Collections.Generic;
using Assets.Meyer.TestScripts;
using Assets.Meyer.TestScripts.Player;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

namespace Kristal
{
    public class Enemy : BaseCharacter
    {
        private float timer;
        

        [SerializeField] public float reactionTime = 3.0f;
        [SerializeField] public float playerDistance = 3.0f;

        private bool distanceCheck = false;

        [SerializeField] private GameObject attachedWeapon;


        private bool attacking = false;

        private float time = 0;

        private Vector3 startPosition;

        private Quaternion startRotation;

        // Use this for initialization
        protected void Awake( ) {
            base.Awake( );
            timer = reactionTime;
        }

        protected void Start( ) {
            this.material.color = BaseColor;
            Nav = gameObject.GetComponent<EnemyNav>();
        }
        

        public override void Damage(BaseCharacter attacker)
        {
          
        }
        // Update is called once per frame
        void Update()
        {
            

        }
        

        public void Remove( BaseCharacter chara ) { }

        
        
        

    }
}
