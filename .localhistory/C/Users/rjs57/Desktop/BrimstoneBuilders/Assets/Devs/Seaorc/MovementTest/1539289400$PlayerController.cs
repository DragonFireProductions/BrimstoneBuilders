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

    private float end = 0.0f;

    public static Stat stats;
    public static UIInventory inventory = UIInventory.instance;

    private float dex, endu, agil = 0.0f;

    private bool showstats = false;

   public enum PlayerState { move, sneak, navMesh}; PlayerState state;

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
        stats = GetComponent<Stat>();
        //inventory = GetComponent<UIInventory>();
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
        state = PlayerState.navMesh;
        }



        if (Input.anyKey)
        {
           state = PlayerState.move;
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
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
            case PlayerState.navMesh:
                break;
                ;
            default:
                break;
        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            showstats = !Stats();

        }

        if (showstats)
            UIInventory.instance.UpdateStats(stats);
        else
            UIInventory.instance.StatUI.SetActive(false);

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
                Vector3 n = new Vector3(X, 0, Z);
                Vector3 dir = Camera.main.transform.TransformDirection(n);
                dir.y = 0;
                Controller.Move(dir * RunSpeed * Time.deltaTime);
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

        Vector3 norm = new Vector3(X, 0, Z);

        var direction = Camera.main.transform.TransformDirection( norm );
        direction.y = 0;


        Controller.Move(direction.normalized * WalkSpeed * Time.deltaTime);
        if (state == PlayerState.move && X > 0 || X < 0 || Z > 0 || Z < 0)
        {
            endu += 0.005f;
            agil += 0.003f;
            dex += 0.0002f;

            if (endu > 1.0f)
            {
                endu = 0.0f;
                ++stats.Endurance;
            }
            else if (agil > 1.0f)
            {
                agil = 0.0f;
                ++stats.Agility;
                ++WalkSpeed;
                if (WalkSpeed > 7.0f)
                    WalkSpeed = 7.0f;
                Debug.Log("walk speed:" + WalkSpeed);
            }
            else if (dex > 1.0f)
            {
                dex = 0.0f;
                ++stats.Dexterity;
            }
        }
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

    bool Stats()
    {
        return showstats;
    }

    public void SetControlled(bool _control)
    {
        Controlled = _control;
    }

}
