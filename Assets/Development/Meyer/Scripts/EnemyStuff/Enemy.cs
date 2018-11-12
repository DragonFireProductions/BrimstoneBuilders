using System;
using System.Collections;
using System.Collections.Generic;

using TMPro;

using UnityEngine;

namespace Kristal
{

    public class Enemy : BaseCharacter
    {
        public Companion ChosenCompanion;

        private int MaxCoinCount = 15;
        private Vector3 deadPos = new Vector3();

        protected void Awake()
        {
            base.Awake();
        }


        protected void Start()
        {
            material.color = BaseColor;
            Nav = gameObject.GetComponent<EnemyNav>();
            threat_signal = gameObject.transform.Find("Canvas/ThreatSignal").GetComponent<SpriteRenderer>();

            MaxCoinCount = UnityEngine.Random.Range(1, MaxCoinCount);

        }

        public void Remove(BaseCharacter chara) { }

        public void Damage(int _damage, BaseCharacter attacker)
        {

            if (stats.Health > 0)
            {
                InstatiateFloatingText.InstantiateFloatingText(_damage.ToString(), this, Color.red);
                stats.Health -= _damage;
            }
            else
            {
                //
                for (int i = 0; i < MaxCoinCount; i++)
                {
                    deadPos = UnityEngine.Random.insideUnitSphere * 2 + this.gameObject.transform.position;
                    deadPos.y = StaticManager.Character.gameObject.transform.position.y;
                    var newCoin = Instantiate(Resources.Load<GameObject>("Coin"));
                    newCoin.gameObject.transform.position = deadPos;
                }
                //
                StaticManager.RealTime.Enemies.Remove(this);
                Destroy(gameObject);
            }

            damage -= _damage;
        }
        //runs when enemy's animation is half way through
        public override void Attack(BaseCharacter chara)
        {
            //gets the damage value
            float l_damage = StaticManager.DamageCalc.CalcAttack(stats, chara.stats);
            //adds the value to the total damage
            damage += (int)l_damage;
            //sets the text value to the damage done
            //damageText.text = damage.ToString();

            Damage((int)l_damage, chara);
        }
    }

}