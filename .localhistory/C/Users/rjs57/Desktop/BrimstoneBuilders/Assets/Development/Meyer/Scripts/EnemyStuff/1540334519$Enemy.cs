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

        private Animation animation;

        private Vector3 startPosition;

        private Quaternion startRotation;

        public LootableObject Loot;

        // Use this for initialization
        void Awake( ) {
            base.Awake( );
            timer = reactionTime;
        }

        protected void Start( ) {
            this.material.color = BaseColor;
            Nav = gameObject.GetComponent<EnemyNav>();

            if (gameObject.GetComponent<LootableObject>())
                Loot = gameObject.GetComponent<LootableObject>();
            
        }
        

        public override void Damage(BaseCharacter attacker)
        {
          StaticManager.uiInventory.ShowNotification("   " + gameObject.name + " : ", 5);
          StaticManager.uiInventory.AppendNotification("\n Damage = " + StaticManager.DamageCalc.CalcAttack(attacker.stats, this.stats));
          StaticManager.uiInventory.AppendNotification("\n Health was " + this.stats.Health);
            float damage = StaticManager.DamageCalc.CalcAttack(attacker.stats, this.stats);
            
            this.stats.Health -= damage;
            Debug.Log("Enemycount: " + TurnBasedController.instance._enemy.characters.Count + "           Damage-Enemy- line: 64");
         
            base.DamageDone((int)damage, this);

            if ( this.stats.Health <= 0 ){
              StaticManager.uiInventory.AppendNotification("\n Enemy is now Dead");
                Leader.Remove(this);
            }
            else{
              StaticManager.uiInventory.AppendNotification("\n health is now " + this.stats.Health);

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

        public void Remove( BaseCharacter chara ) { }

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
