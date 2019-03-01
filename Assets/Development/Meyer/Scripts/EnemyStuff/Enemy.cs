using UnityEngine;

namespace Kristal {

    public class Enemy : BaseCharacter {

        private int MaxCoinCount = 15;

        private Vector3 deadPos;

        private int drop;

        public int damage;

        public bool dropKey = false;

        public QuestItem questItem;

        public Quest quest;

        public bool DropWeapon;

        public GameObject objectToDrop;

        public float health;

        public GameObject DestroyEffect;


        protected void Awake( ) {
            base.Awake( );
        }

        protected void Start( ) {
            StaticManager.RealTime.AllEnemies.Add( this );
            Nav = gameObject.GetComponent < EnemyNav >( );

            base.stats.health = health;
            base.stats.maxHealth = health;
            MaxCoinCount          = Random.Range( 1 , MaxCoinCount );
            attachedWeapon.Damage = damage;
            StaticManager.map.Enemies.Add(icon);
            StaticManager.map.All.Add(icon);
        }

        public void Remove( BaseCharacter chara ) { }

        public override void Damage( int _damage , BaseItems item ) {
            item.IncreaseSubClass( 0.053f );

            if ( stats.Health > 0 ){
                base.Damage( _damage , item );
                var blood = StaticManager.particleManager.Play( ParticleManager.states.Blood , transform.position );
                blood.transform.SetParent(gameObject.transform);
                if (stats.health <= 0)
                {
                    if ( quest ){
                         quest.EnemyDied(this);
                    }
                   
                    item.IncreaseSubClass(0.3f);
                    Debug.Log("Hit if <0");
                    
                        StaticManager.drop.Drop_Loot(this);
                    StaticManager.RealTime.Enemies.Remove(this);
                    Instantiate(DestroyEffect, transform.position, transform.rotation);
                    Destroy(gameObject);
                }
            }
        }

    }

}