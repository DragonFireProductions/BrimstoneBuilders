using System.Collections;
using System.Collections.Generic;
using Assets.Meyer.TestScripts;
using Assets.Meyer.TestScripts.Player;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

namespace Kristal
{
    public class Enemy : MonoBehaviour
    {
        private float timer;
        

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


        [SerializeField] float health = 100;

        public EnemyLeader leader;

        public GameObject obj;

        public EnemyNav Nav;

        public Stat Stats;



        // Use this for initialization
        void Awake()
        {
            timer = reactionTime;

            if (enemies == null)
                enemies = new List<GameObject>();
            
            //attachedWeapon = transform.Find( "EnemySword" ).gameObject;
            //animation = attachedWeapon.GetComponent<Animation>();
        }

        private void Start( ) {
            Stats = gameObject.GetComponent<Stat>();
            obj = gameObject;
            Nav = gameObject.GetComponent<EnemyNav>();
        }

        private void OnDisable()
        {
            enemies.Remove(gameObject);
        }
        public void Damage(Companion attacker)
        {
            UIInventory.instance.ShowNotification(attacker.name + " has chosen to attack " + this.gameObject.name, 5);
            UIInventory.instance.AppendNotification("\n Damage = " + DamageCalc.Instance.CalcAttack(attacker.Stats, this.Stats));
            UIInventory.instance.AppendNotification("\n " + this.Stats.Name + " health was " + this.Stats.Health);
            this.Stats.Health -= DamageCalc.Instance.CalcAttack(attacker.Stats, this.Stats);

            if ( this.Stats.Health <= 0 ){
                UIInventory.instance.AppendNotification("\n Enemy is now Dead");
                leader.Remove(this);
            }
            else{
                UIInventory.instance.AppendNotification("\n" + this.Stats.name + " health is now " + this.Stats.Health);

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
        
        public EnemyLeader Leader {
            get { return leader; }
            set { leader = value; }
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
