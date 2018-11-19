using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Kristal;

public class Projectile : MonoBehaviour
{
    /// <remarks> assign in inspector </remarks>
    [SerializeField] int Damage;
    [SerializeField] float Speed;

    /// <summary>
    /// Moves the projectile in its forwoard derection
    /// </summary>
    void Update()
    {
        transform.Translate(0, 0, Speed * Time.deltaTime);
    }

    /// <summary>
    /// Detects when the projectile hits an _enemy
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        if ( other.tag == "Enemy" || other.tag == "Companion" || other.tag == "Player" ){
            other.GetComponent<BaseCharacter>().Damage(Damage);
            StartCoroutine( stopBullet( ) );
        }
        
    }

    IEnumerator stopBullet( ) {
        yield return new WaitForSeconds(1);
        gameObject.SetActive(false);
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
