﻿using System.Collections;
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

        private Animation animation;

        private Vector3 startPosition;

        private Quaternion startRotation;
        
        public EnemyNav Nav;

        // Use this for initialization
        void Awake() {

            timer = reactionTime;

            if (characterObjs == null)
                characterObjs = new List<GameObject>();
            
            //attachedWeapon = transform.Find( "EnemySword" ).gameObject;
            //animation = attachedWeapon.GetComponent<Animation>();
        }

        private void Start( ) {
            stats = gameObject.GetComponent<Stat>();
            obj = gameObject;
            Nav = gameObject.GetComponent<EnemyNav>();
        }

        private void OnDisable()
        {
            characterObjs.Remove(gameObject);
        }
        public void Damage(Companion attacker)
        {
            UIInventory.instance.ShowNotification(attacker.name + " has chosen to attack " + this.gameObject.name, 5);
            UIInventory.instance.AppendNotification("\n Damage = " + DamageCalc.Instance.CalcAttack(attacker.stats, this.stats));
            UIInventory.instance.AppendNotification("\n " + this.stats.Name + " health was " + this.stats.Health);
            this.stats.Health -= DamageCalc.Instance.CalcAttack(attacker.stats, this.stats);
            Debug.Log("Enemycount: " + TurnBasedController.instance.Enemies.Count + "           Damage-Enemy- line: 64");

            if ( this.stats.Health <= 0 ){
                UIInventory.instance.AppendNotification("\n Enemy is now Dead");
                Leader.Remove(this);
            }
            else{
                UIInventory.instance.AppendNotification("\n" + this.stats.name + " health is now " + this.stats.Health);

            }

        }
        // Update is called once per frame
        void Update()
        {
            if (Character.player)
            {
                float distance = Vector3.Distance(Character.player.transform.position, transform.position);

                if (distance < playerDistance)
                {

                    if (!distanceCheck)
                    {
                        float step = 0.5f * Time.deltaTime;

                        Vector3 dir = Vector3.RotateTowards(transform.forward, Character.player.transform.forward, step,
                            0.0f);

                        transform.rotation = Quaternion.LookRotation(dir);

                        //warn player

                        distanceCheck = true;
                    }
                    else
                    {
                        timer -= Time.deltaTime;
                    }

                    if (timer <= 0.0f)
                    {
                        attacking = true;
                    }
                }

                else
                {
                    attacking = false;
                    distanceCheck = false;
                    timer = reactionTime;
                }
            }

            if (attacking == true)
            {
                timer += Time.deltaTime;
            }

            if (timer >= 0.4f)
            {
                attacking = false;
            }

        }

        public override void Remove( BaseCharacter chara ) { }

        public EnemyLeader Leader {
            get { return ( EnemyLeader )leader; }
            set { leader = value; }
        }
        

        void EndDeath()
        {
            Destroy(this.gameObject);
        }
        

    }
}
