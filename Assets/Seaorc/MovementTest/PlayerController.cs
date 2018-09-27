using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class PlayerController : MonoBehaviour
{
    /// <remarks>Set in inspecor</remarks>
    [SerializeField] float WalkSpeed;
    [SerializeField] float RunSpeed;
    [SerializeField] float JumpForce;
    [SerializeField] float Gravity;
    [SerializeField] Transform Cam;

    CharacterController Controller;
    bool Controlled = true;
    float Y;

    /// <summary>
    /// intilizes variables that are not set in insepector
    /// </summary>
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

        Cam = GameObject.Find("CamHolder").transform;
        Assert.IsNotNull(Cam, "Camholder cannot be found!");
    }

    /// <summary>
    /// Calles the move function onece per frame if character is currently controlled 
    /// </summary>
    void Update()
    {
        if (Controlled)
        {
            Move();
        }
    }

    /// <summary>
    /// Moves character based on current input
    /// </summary>
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

    /// <summary>
    /// Sets character controller
    /// </summary>
    /// <param name="_control"></param>
    public void SetControlled(bool _control)
    {
        Controlled = _control;
    }

}
