using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float Health;
    [SerializeField] Animator Ani;
    [SerializeField] float WalkSpeed;
    [SerializeField] float RunSpeed;
    [SerializeField] float JumpForce;
    [SerializeField] float Gravity;
    [SerializeField] Transform Cam;

    CharacterController Controller;
    float Y;
    //NavMeshAgent Agent;

    private void Awake()
    {
        if (GetComponent<CharacterController>())
            Controller = GetComponent<CharacterController>();
        else
        {
            gameObject.AddComponent<CharacterController>();
            Controller = GetComponent<CharacterController>();
        }

        //if (GetComponent<NavMeshAgent>())
        //    Agent = GetComponent<NavMeshAgent>();
        //else
        //{
        //    gameObject.AddComponent<NavMeshAgent>();
        //    Agent = GetComponent<NavMeshAgent>();
        //}
    }

    public void Damage(float _damage)
    {
        Health -= _damage;

        if (Health <= 0)
            Die();
    }

    void Move()
    {
        float X = Input.GetAxis("Horizontal");
        X *= WalkSpeed;
        float Z = Input.GetAxis("Vertical");
        Z *= WalkSpeed;

        if (Input.GetKey(KeyCode.LeftShift) && RunSpeed > 1)
        {
            X *= RunSpeed;
            Z *= RunSpeed;
        }

        if (Controller.isGrounded)
        {
            if (Input.GetButtonDown("Jump"))
            {
                Y = JumpForce;
            }
            else
                Y = 0;
        }
        else
        {
            Y -= Gravity * Time.deltaTime;
        }


        Controller.Move(Cam.TransformDirection(new Vector3(X, Y, Z) * Time.deltaTime));
    }

    void Die()
    {

    }

    public float GetHealth() { return Health; }
    public void SetHealth(float NewHealth) { Health = NewHealth; }

    public Animator GetAnimator() { return Ani; }
    public void SetAnimator(Animator NewAnimator) { Ani = NewAnimator; }
}
