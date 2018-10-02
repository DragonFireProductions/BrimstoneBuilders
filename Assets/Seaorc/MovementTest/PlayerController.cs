using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float WalkSpeed;
    [SerializeField] float RunSpeed;
    [SerializeField] float JumpForce;
    [SerializeField] float Gravity;
    [SerializeField] Transform Cam;

    CharacterController Controller;
    bool Controlled = true;
    float Y;

    float sneakspeed = 2.0f;
    bool sneak = false;

   public enum PlayerState { move, sneak}; PlayerState state;

    // Use this for initialization
    void Start()
    {
        state = PlayerState.move;
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

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
             sneak = !isSneaking();
            //sneak = true;
        }
        //else if (Input.GetKey(KeyCode.C))
        //{
        //    sneak = false;
        //    //state = PlayerState.move;
        //}

        if (sneak)
            state = PlayerState.sneak;
        else
            state = PlayerState.move;

        

        switch (state)
        {
            case PlayerState.move:
                Debug.Log("move");
                if (Controlled)
                {
                    Move();
                }
                break;
            case PlayerState.sneak:
                Debug.Log("sneaking");
                if (Controlled)
                {
                    Sneak();
                }
                break;
            default:
                break;
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

    void Sneak()
    {
        float x = Input.GetAxis("Horizontal");
        x *= sneakspeed;
        float z = Input.GetAxis("Vertical");
        z *= sneakspeed;

        Controller.Move(Cam.TransformDirection(new Vector3(x, Y, z) * Time.deltaTime));
    }

    bool isSneaking()
    {
        return sneak;
    }

    public void SetControlled(bool _control)
    {
        Controlled = _control;
    }

}
