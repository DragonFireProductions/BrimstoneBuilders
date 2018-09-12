using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] Animator Ani;
    [SerializeField] float WalkSpeed;
    [SerializeField] float RunSpeed;
    [SerializeField] float JumpForce;
    [SerializeField] float Gravity;
    [SerializeField] Transform Cam;

    CharacterController Controller;
    float Y;

    private void Awake()
    {
        if (GetComponent<CharacterController>())
            Controller = GetComponent<CharacterController>();
        else
        {
            gameObject.AddComponent<CharacterController>();
            Controller = GetComponent<CharacterController>();
        }
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
    

    public Animator GetAnimator() { return Ani; }
    public void SetAnimator(Animator NewAnimator) { Ani = NewAnimator; }
}
