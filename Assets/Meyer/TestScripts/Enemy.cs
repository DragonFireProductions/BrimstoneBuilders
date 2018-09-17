using System.Collections;
using System.Collections.Generic;
using Assets.Meyer.TestScripts;
using Assets.Meyer.TestScripts.Player;
using UnityEngine;

namespace Kristal
{
    public class Enemy : MonoBehaviour
    {
        private float timer;
        private Animator animator;

        [SerializeField] private int health = 100;
        [SerializeField] public float reactionTime = 3.0f;
        [SerializeField] public float playerDistance = 3.0f;

        private bool distanceCheck = false;
        static List<GameObject> enemies;

        // Use this for initialization
        void Awake()
        {
            animator = gameObject.GetComponent<Animator>();
            timer = reactionTime;

            if (enemies == null)
                enemies = new List<GameObject>();

            enemies.Add(gameObject);
        }

        private void OnDisable()
        {
            enemies.Remove(gameObject);
        }

        // Update is called once per frame
        void Update()
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
                    animator.SetBool("Attacking", true);
                }
            }

            else
            {
                animator.SetBool("Attacking", false);
                distanceCheck = false;
                timer = reactionTime;
            }
        }

        public void Damage(int damage)
        {
            health -= damage;
            Debug.Log("Enemy Health: " + health);

            if (health <= 0.0f)
            {
                animator.SetBool("Dying", true);
            }
        }

        public int GetHealth()
        {
            return health;
        }

        void EndAttack()
        {
            timer = 0.0f;
            Character.instance.Damage(7);
        }

        void StartAttack()
        {
            Character.instance.Damage(5);
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
