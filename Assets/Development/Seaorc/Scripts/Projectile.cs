using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Kristal;

public class Projectile : MonoBehaviour
{
    /// <remarks> assign in inspector </remarks>
    [SerializeField] protected int Damage;
    [SerializeField] float Speed;
    private int maxDOTduration = 5;
    private float Timer = 0;
    private float IntervalTimer = 0;

    public bool doesDOT;

    protected int DOT_interval; //Time between DOT damage.

    public int hits = 3;

    public float interval = 0.5f;

    void OnEnable( ) {
        StartCoroutine(stopBullet(5));

    }
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
    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy" || other.tag == "Companion" || other.tag == "Player")
        {
            other.GetComponent<BaseCharacter>().Damage(Damage);
            StartCoroutine(stopBullet(1));
        }

    }

    IEnumerator stopBullet(int i)
    {
        yield return new WaitForSeconds(i);
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
