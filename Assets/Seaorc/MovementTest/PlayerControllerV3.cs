using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerV3 : MonoBehaviour
{
    [SerializeField] float WalkSpeed;
    [SerializeField] float RunSpeed;
    [SerializeField] float JumpForce;
    [SerializeField] float Gravity;
    [SerializeField] Transform Cam;

    CharacterController Controller;
    bool Controlled = true;
    float Y;

    // Use this for initialization
    void Start()
    {
        if(GetComponent<CharacterController>() != null)
        {
            Controller = GetComponent<CharacterController>();
        }
        else
        {
            gameObject.AddComponent<CharacterController>();
            Controller = GetComponent<CharacterController>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Controlled)
        {
            Move();
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

    public void SetControlled(bool _control)
    {
        Controlled = _control;
    }

}
