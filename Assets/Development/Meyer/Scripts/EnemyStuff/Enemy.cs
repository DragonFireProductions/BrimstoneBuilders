using UnityEngine;

namespace Kristal {

    public class Enemy : BaseCharacter {

        private int MaxCoinCount = 15;

        private Vector3 deadPos;

        private int drop;

        public int damage;

        [SerializeField] public GameObject key;

        protected void Awake( ) {
            base.Awake( );
            

        }

        protected void Start( ) {
            StaticManager.RealTime.AllEnemies.Add( this );
            Nav = gameObject.GetComponent < EnemyNav >( );

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
                    item.IncreaseSubClass(0.3f);
                    //drop = Random.Range( 1 , 10 );
                    Debug.Log("Hit if <0");
                    
                    if (key != null)
                    {
                        Debug.Log("Hit dropped loot");
                        StaticManager.drop.Drop_Loot(this);
                    }
                    StaticManager.RealTime.Enemies.Remove(this);
                    Destroy(gameObject);
                }
            }

            
        }

    }

}