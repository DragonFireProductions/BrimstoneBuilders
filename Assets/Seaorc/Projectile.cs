using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Kristal;

public class Projectile : MonoBehaviour
{

    [SerializeField] int Damage;
    [SerializeField] float Speed;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(0, 0, Speed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.GetComponent<Enemy>())
        {
            collision.gameObject.GetComponent<Enemy>().Damage(Damage);
        }

        Destroy(this.gameObject);
    }

    public float GetSpeed()
    {
        return Speed;
    }
}
