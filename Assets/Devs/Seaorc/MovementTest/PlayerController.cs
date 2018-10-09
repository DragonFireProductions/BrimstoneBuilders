using System.Collections;
using System.Collections.Generic;

using Assets.Meyer.TestScripts;
using Assets.Meyer.TestScripts.Player;

using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Assertions;

public class PlayerController : MonoBehaviour
{
    /// <remarks>Set in inspecor</remarks>
    [SerializeField] float WalkSpeed;
    [SerializeField] float RunSpeed;
    [SerializeField] float JumpForce;
    [SerializeField] float Gravity;
    [SerializeField] Transform Cam;

    [SerializeField] float speed = 3;

    CharacterController Controller;
    bool Controlled = true;
    float Y;

    float sneakspeed = 1.5f;
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

    /// <summary>
    /// Calles the move function onece per frame if character is currently controlled
    /// </summary>
    void Update()
    {
        if (Input.GetMouseButtonDown(1)){
                RaycastHit hit;

                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                CharacterUtility.instance.EnableObstacle(this.gameObject.GetComponent<NavMeshAgent>(), true);

                if (Physics.Raycast(ray, out hit))
                {
                    float step = speed;
                    float distance = Vector3.Distance(transform.position, hit.point);

                    Vector3 pos;
                    pos.x = hit.point.x;
                    pos.y = 0.0f;
                    pos.z = hit.point.z;
                    gameObject.GetComponent<NavMeshAgent>().SetDestination(pos);

                }
        //state = PlayerState.navMesh;
        }



        if (Input.anyKey)
        {
            state = PlayerState.move;
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            //Sneak();
            sneak = !isSneaking();

        }
        if (sneak)
            state = PlayerState.sneak;
        else
            state = PlayerState.move;


        switch (state)
        {
            case PlayerState.move:

                Move();
                break;
            case PlayerState.sneak:
                Sneak();
                break;
            default:
                break;
        }

    }

    /// <summary>
    /// Moves character based on current input
    /// </summary>
    void Move()
    {

        float X = Input.GetAxisRaw("Horizontal");
        X *= WalkSpeed;
        float Z = Input.GetAxisRaw("Vertical");
        Z *= WalkSpeed;

        if (X > 0 || Z > 0){
            CharacterUtility.instance.EnableObstacle( this.gameObject.GetComponent<NavMeshAgent>() , false );
        }

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

        Vector3 norm = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));

        var direction = Camera.main.transform.TransformDirection( norm );
        direction.y = 0;


        Controller.Move(Cam.transform.TransformDirection(new Vector3(X, Y, Z)) * Time.deltaTime);
    }

    void Sneak()
    {
        float x = Input.GetAxis("Horizontal");
        x *= sneakspeed;
        float z = Input.GetAxis("Vertical");
        z *= sneakspeed;

        Vector3 norm = new Vector3(x, 0, z);
        Vector3 direction = Camera.main.transform.TransformDirection(norm);
        direction.y = 0;


        Controller.Move(Cam.TransformDirection(direction.normalized * Time.deltaTime));
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
