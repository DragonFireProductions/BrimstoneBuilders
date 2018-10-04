﻿using System.Collections;
using System.Collections.Generic;
using Assets.Meyer.TestScripts;
using Assets.Meyer.TestScripts.Player;
using UnityEngine;
using UnityEngine.UI;

namespace Kristal
{
    public class Enemy : MonoBehaviour
    {
        private float timer;
        private Animator animator;

        [SerializeField]
        private int maxHealth = 100;
        [SerializeField]
        private int maxStamina;
        [SerializeField]
        private int strength;
        [SerializeField]
        private int Speed;
        [SerializeField]
        private int health = 100;
        [SerializeField]
        private int stamina;

        [ SerializeField ] private GameObject groupLeader;

        [SerializeField] public float reactionTime = 3.0f;
        [SerializeField] public float playerDistance = 3.0f;

        private bool distanceCheck = false;
        static List<GameObject> enemies;

        [SerializeField] private GameObject attachedWeapon;


        private bool attacking = false;
        private float time = 0;

        private Animation animation;

        private Vector3 startPosition;

        private Quaternion startRotation;

        // Use this for initialization
        void Awake()
        {
            animator = gameObject.GetComponent<Animator>();
            timer = reactionTime;

            if (enemies == null)
                enemies = new List<GameObject>();
            
            attachedWeapon = transform.Find( "EnemySword" ).gameObject;
            animation = attachedWeapon.GetComponent<Animation>();
        }

        private void Start( ) {

        }

        private void OnDisable()
        {
            enemies.Remove(gameObject);
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

        public void Damage(int _damage)
        {
            health -= _damage;
            //Debug.Log("Enemy Health: " + health);
           GameObject.Find("EnemyHealth").GetComponent<Text>().text = "Enemy Health: " + health;


            if (health <= 0.0f)
            {
                animator.SetBool("Dying", true);
            }

        }

        public int GetMaxHealth()
        {
            return maxHealth;
        }

        public int GetHealth()
        {
            return health;
        }

        public int GetMaxStamina()
        {
            return maxStamina;
        }

        public int GetStamina()
        {
            return stamina;
        }

        public int GetStrength()
        {
            return strength;
        }

        public int GetSpeed()
        {
            return Speed;
        }

        public GameObject Leader {
            get { return groupLeader; }
            set { groupLeader = value; }
        }
        

        void EndDeath()
        {
            Destroy(this.gameObject);
        }

        public List<GameObject> GetEnemies()
        {
            return enemies;
        }
        

    }
}