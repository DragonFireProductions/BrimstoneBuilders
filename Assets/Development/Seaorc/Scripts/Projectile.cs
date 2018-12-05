using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Kristal;

public class Projectile : MonoBehaviour
{
    /// <remarks> assign in inspector </remarks>
    [SerializeField] float Speed;
    private int maxDOTduration = 5;
    private float Timer = 0;
    private float IntervalTimer = 0;

    public bool doesDOT;

    protected int DOT_interval; //Time between DOT damage.

    public int hits = 3;

    public float interval = 0.5f;

    public GunType weapon;

    public ParticleSystem hiteffect;

    public int TimeToPlay;

    public int y_pos;
    
    /// <summary>
    /// Moves the projectile in its forward direction
    /// </summary>
    public void Update()
    {
        transform.Translate(0, 0, Speed * Time.deltaTime);
    }
    
    /// <summary>
    /// Detects when the projectile hits an _enemy
    /// </summary>
    /// <param name="other"></param>
    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy" || other.tag == "Companion" || other.tag == "Player")
        {
            if(weapon.KnockBack)
            {
                var coliders = Physics.OverlapSphere(this.transform.position, 5);
                foreach (var VARIABLE in coliders)
                {
                    if(VARIABLE.tag == "Enemy" || VARIABLE.tag == "Companion" ) {
                        VARIABLE.GetComponent<BaseCharacter>().KnockBack(weapon.KnockBackAmount);
                    }
                   
                }
            }


            //Sets the y position and plays the particle effect, y gets set in the item editor.
            var aboveHead = other.transform.position;
            aboveHead.y += y_pos;
            var effect = Instantiate( hiteffect );
            effect.transform.position = aboveHead;
            effect.transform.SetParent(other.transform);
            effect.Play();
            StaticManager.particleManager.Play(ParticleManager.states.Blood, other.transform.position, other.gameObject.transform);
            
            if (doesDOT)
            {
                //Deals damage to the target and applies the DOT effect.
                
                other.GetComponent<BaseCharacter>().Damage((int)weapon.Damage, weapon);
                other.GetComponent<BaseCharacter>().DOT((int)weapon.Damage / 2, interval, hits, weapon); //Apply the dot damage.
            }

            else
            {
                //Deals the base damage only.
                other.GetComponent<BaseCharacter>().Damage((int)weapon.Damage, weapon);
            }
            if (weapon.RunAwayOnUse && other.tag != "Player")
            {
                other.GetComponent<BaseNav>().RunAway();
            }
        }
        else{
            InstatiateFloatingText.InstantiateFloatingText( "MISS" , weapon.AttachedCharacter , Color.grey, new Vector3(0.5f, 0.5f, 0.5f) );
        }

        gameObject.SetActive(false);


    }

    
    public void OnCollisionEnter( Collision collision ) {

         InstatiateFloatingText.InstantiateFloatingText( "MISS" , weapon.AttachedCharacter , Color.grey, new Vector3(0.5f, 0.5f, 0.5f) );
        gameObject.SetActive(false);
    }

    IEnumerator stopBullet(int i)
    {
        yield return new WaitForSeconds(i);
        //gameObject.SetActive(false);
    }

    /// <summary>
    /// Returns the projectiles speed
    /// </summary>
    /// <returns></returns>
    public float GetSpeed()
    {
        return Speed;
    }
    

    
}
