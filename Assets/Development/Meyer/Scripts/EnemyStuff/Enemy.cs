using System;
using System.Collections;
using System.Collections.Generic;

using TMPro;

using UnityEngine;
using Random = UnityEngine.Random;

namespace Kristal
{

    public class Enemy : BaseCharacter
    {

        private int MaxCoinCount = 15;
        private Vector3 deadPos = new Vector3();

        private int drop;

        public int damage;

        protected void Awake()
        {
            base.Awake();
          

        }


        protected void Start()
        {

            StaticManager.RealTime.AllEnemies.Add(this);
            Nav = gameObject.GetComponent<EnemyNav>();

            MaxCoinCount = UnityEngine.Random.Range(1, MaxCoinCount);
            attachedWeapon.Damage = damage;
        }

        public void Remove(BaseCharacter chara) { }
        
        public override void Damage(int _damage, BaseItems item) {
            
            item.IncreaseSubClass(0.053f);
            if (stats.Health > 0)
            {
                base.Damage(_damage, item);
                var blood = StaticManager.particleManager.Play(ParticleManager.states.Blood, transform.position);
                blood.transform.SetParent(gameObject.transform);
            }
            else
            {   
                item.IncreaseSubClass(0.3f);
                drop = Random.Range(1, 10);
                if (drop > 5)
                {
                    for (int i = 0; i < MaxCoinCount; i++)
                    {
                        deadPos = UnityEngine.Random.insideUnitSphere * 2.5f + this.gameObject.transform.position;
                        deadPos.y = StaticManager.Character.gameObject.transform.position.y;
                        var newCoin = Instantiate(Resources.Load<GameObject>("Coin"));
                        newCoin.gameObject.transform.position = deadPos;
                    }
                }
                else
                {
                    deadPos = UnityEngine.Random.insideUnitSphere * 2.5f + this.gameObject.transform.position;
                    deadPos.y = StaticManager.Character.gameObject.transform.position.y;
                    var newsword = Instantiate(attachedWeapon);
                    newsword.tag = "PickUp";

                    newsword.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
                    newsword.gameObject.transform.position = deadPos;
                }
                StaticManager.RealTime.Enemies.Remove(this);
                Destroy(gameObject);
            }
        }
        
    }

}