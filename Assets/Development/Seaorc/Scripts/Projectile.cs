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
        
        
        Destroy(this.gameObject);
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
